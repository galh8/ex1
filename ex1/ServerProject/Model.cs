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
    class Model : IModel
    {
        private Dictionary<string, string> mazeSolutions;
        private Dictionary<string, Maze> mazesDictionary;
        private Dictionary<string, Game> gamesLobby;
        private Dictionary<TcpClient,Game> gamesBeingPlayed;
        DFSMazeGenerator mazeGenerator;
        BFS<Position> bfsSolver;
        DFS<Position> dfsSolver;


        public Model()
        {
            mazeGenerator = new DFSMazeGenerator();
            mazeSolutions = new Dictionary<string, string>();
            mazesDictionary = new Dictionary<string, Maze>();
            gamesLobby = new Dictionary<string, Game>();
            gamesBeingPlayed = new Dictionary<string, Game>(); 
            bfsSolver = new BFS<Position>();
            dfsSolver = new DFS<Position>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {

            Maze maze = mazeGenerator.Generate(rows, cols);
            mazesDictionary.Add(name, maze);
            return maze;
        }

        public Game startGame(string name, int rows, int cols, System.Net.Sockets.TcpClient firstPlayer)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            var newGame = new Game(firstPlayer,maze); //publisher

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

            //notify that another player joined the game.
            currentGAME.joinAnotherPlayer(otherPlayer);

            return currentGAME.PlayedMaze().ToJSON();
        }

        public string play(string direction, TcpClient otherPlayer)
        {
            Game currentGAME = gamesBeingPlayed[otherPlayer];

            /*pop out the game out of the lobby and moves it 
            to another dictonery of a being played games */
            //gamesLobby.Remove(direction);
           // gamesBeingPlayed.Add(direction, currentGAME);

            //notify that another player joined the game.
            currentGAME.joinAnotherPlayer(otherPlayer);

            return currentGAME.PlayedMaze().ToJSON();
        }


        public string getListOfGames()
        {
            List<string> gamesList = new List<string>(this.gamesLobby.Keys);

            return JsonConvert.SerializeObject(gamesList);
        }


        public string solve(string name, int algo)
        {
            //checks if the solution is already exists.
            if (mazeSolutions.ContainsKey(name))
            {
                return mazeSolutions[name];
            }
            //if there is no existing solution - solving it. 
            else
            {
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
                }
                else
                {
                    Solution<Position> dfsSolution = new Solution<Position>();
                    dfsSolution = dfsSolver.search(searchableMaze);
                    //Json                   
                    solveObj["Solution"] = calculateSolution(dfsSolution.PathToGoal());
                    solveObj["NodesEvaluated"] = dfsSolver.getNumberOfNodesEvaluated().ToString();
                }
                //adding the solution to the dictionary.
                mazeSolutions.Add(name, solveObj.ToString());
                return solveObj.ToString();
            }

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
                else if(position.getInstance().Row != position.CameFrom.getInstance().Row)
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


