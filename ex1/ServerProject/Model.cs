using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace ServerProject
{
    public class Model : IModel
    {
        private Dictionary<string, string> mazeBFSSolutions;
        private Dictionary<string, string> mazeDFSSolutions;
        private Dictionary<string, Maze> mazesDictionary;
        private Dictionary<string, Game> gamesLobby;
        private Dictionary<TcpClient, Game> gamesBeingPlayed;
        private HashSet<string> mazesAndGamesNames;
        DFSMazeGenerator mazeGenerator;
        BFS<Position> bfsSolver;
        DFS<Position> dfsSolver;



        public Model()
        {
            mazeGenerator = new DFSMazeGenerator();
            mazeBFSSolutions = new Dictionary<string, string>();
            mazeDFSSolutions = new Dictionary<string, string>();
            mazesDictionary = new Dictionary<string, Maze>();
            gamesLobby = new Dictionary<string, Game>();
            gamesBeingPlayed = new Dictionary<TcpClient, Game>();
            bfsSolver = new BFS<Position>();
            dfsSolver = new DFS<Position>();
            //hash set that resposible to aware of two games and mazes with the same name.
            mazesAndGamesNames = new HashSet<string>();
        }


        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            mazesDictionary.Add(name, maze);
            mazesAndGamesNames.Add(name);
            return maze;
        }
        

        public Game startGame(string name, int rows, int cols, System.Net.Sockets.TcpClient firstPlayer)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            mazesAndGamesNames.Add(name);
            var newGame = new Game(firstPlayer, maze); //publisher

            //adding the new maze to the maze dictionary.
            mazesDictionary.Add(name, maze);

            //adding the game to the lobby till someone asks to join
            gamesLobby.Add(name, newGame);

            //subscriber
            //newGame.gameNeedsToStart += startGame.OngameNeedsToStart;

            //newGame.PlayerJoind(client);

            return newGame;
        }

        public string join(string gameName, System.Net.Sockets.TcpClient otherPlayer)
        {
            Game currentGAME = gamesLobby[gameName];

            /*pop out the game out of the lobby and moves it 
            to another dictonery of a being played games */
            gamesLobby.Remove(gameName);
            gamesBeingPlayed.Add(otherPlayer, currentGAME);
            gamesBeingPlayed.Add(currentGAME.getCreatorPlayer(), currentGAME);

            //notify that another player joined the game.
            currentGAME.joinAnotherPlayer(otherPlayer);

            return currentGAME.PlayedMaze().ToJSON();
        }

        public string play(string direction, TcpClient otherPlayer)
        {
            Game currentGAME = gamesBeingPlayed[otherPlayer];
            TcpClient playerToNotify = currentGAME.getOtherPlayer(otherPlayer);


            /*pop out the game out of the lobby and moves it 
            to another dictonery of a being played games */
            //gamesLobby.Remove(direction);
            // gamesBeingPlayed.Add(direction, currentGAME);

            //notify that another player joined the game.
            //currentGAME.joinAnotherPlayer(otherPlayer);

            return currentGAME.PlayedMaze().ToJSON();
        }

        public void close(TcpClient firstPlayer, TcpClient secondPlayer)
        {
            Game currentGame = gamesBeingPlayed[firstPlayer];
            //removing the maze of the game from the maze dictionery.
            mazesDictionary.Remove(currentGame.Name);
            mazesAndGamesNames.Remove(currentGame.Name);
            //removing both of the players from the list.
            gamesBeingPlayed.Remove(firstPlayer);
            gamesBeingPlayed.Remove(secondPlayer);
            
        }

        public TcpClient getOtherPlayerClient(TcpClient client)
        {
            Game currentGAME = gamesBeingPlayed[client];
            TcpClient otherPlayer = currentGAME.getOtherPlayer(client);
            return otherPlayer;

        }
        public string getClientGameName(TcpClient client)
        {
            Game currentGAME = gamesBeingPlayed[client];
            return currentGAME.Name;

        }

        public string[] getListOfGames()
        {
            return gamesLobby.Keys.ToArray();
        }


        public string solve(string name, int algo)
        {
            if (algo == 0)
            {
                //checks if the  BFS solution is already exists.
                if (mazeBFSSolutions.ContainsKey(name))
                {
                    return mazeBFSSolutions[name];
                }

            }
            else if (mazeDFSSolutions.ContainsKey(name))
            //checks if the DFS solution is already exists.
            {
                return mazeDFSSolutions[name];
            }


            //if there is no existing solution - solving it. 
            Maze maze = mazesDictionary[name];
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            JObject solveObj = new JObject();
            solveObj["Name"] = name;

            if (algo == 0)
            {
                Solution<Position> bfsSolution = new Solution<Position>();
                bfsSolution = bfsSolver.search(searchableMaze);
                //Json
                solveObj["Solution"] = calculateSolution(bfsSolution.PathToGoal());
                solveObj["NodesEvaluated"] = bfsSolver.getNumberOfNodesEvaluated().ToString();
                mazeBFSSolutions.Add(name, solveObj.ToString());
            }
            else
            {
                Solution<Position> dfsSolution = new Solution<Position>();
                dfsSolution = dfsSolver.search(searchableMaze);
                //Json                   
                solveObj["Solution"] = calculateSolution(dfsSolution.PathToGoal());
                solveObj["NodesEvaluated"] = dfsSolver.getNumberOfNodesEvaluated().ToString();
                mazeDFSSolutions.Add(name, solveObj.ToString());
            }
            //adding the solution to the dictionary.
           
            return solveObj.ToString();
        }

        public bool isNameAlreadyExists(string name)
        {
            if (mazesAndGamesNames.Contains(name))
            {
                return true;
            }

            return false;
        }


        private string calculateSolution(List<State<Position>> pathToGoal)
        {
            StringBuilder pathToReturn = new StringBuilder();

            List<Position> newList = new List<Position>();

            foreach (State<Position> position in pathToGoal)
            {
                //if we came from the same row

                if (position.getInstance().Row == position.CameFrom.getInstance().Row)
                {
                    if (position.getInstance().Col > position.CameFrom.getInstance().Col)
                    {
                        pathToReturn.Append("1");
                    }
                    else
                    {
                        pathToReturn.Append("0");
                    }
                }
                //if we came from the same col
                else if (position.getInstance().Row != position.CameFrom.getInstance().Row)
                {
                    if (position.getInstance().Row > position.CameFrom.getInstance().Row)
                    {
                        pathToReturn.Append("3");
                    }
                    else
                    {
                        pathToReturn.Append("2");
                    }
                }

            }

            //returning it back reversed
            char[] arrayToReverse = pathToReturn.ToString().ToCharArray();
            Array.Reverse(arrayToReverse);
            return new string(arrayToReverse);
        }
    }
}



