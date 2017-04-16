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


namespace ServerProject
{
    class Model : IModel
    {
        private Dictionary<string, string> mazeSolutions;
        private Dictionary<string, Maze> mazesDictionary;
        private Dictionary<string, Maze> gamesLobby;
        DFSMazeGenerator mazeGenerator;
        BFS<Position> bfsSolver;
        DFS<Position> dfsSolver;

        public Model()
        {
            mazeGenerator = new DFSMazeGenerator();
            mazeSolutions = new Dictionary<string, string>();
            mazesDictionary = new Dictionary<string, Maze>();
            gamesLobby = new Dictionary<string, Maze>();
            bfsSolver = new BFS<Position>();
            dfsSolver = new DFS<Position>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            
            Maze maze = mazeGenerator.Generate(rows, cols);
            mazesDictionary.Add(name, maze);
            return maze;
        }

        public string startGame(string name, int rows, int cols)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            StringBuilder gameName = new StringBuilder();
            gameName.Append("name"); gameName.Append("game");
            mazesDictionary.Add(gameName.ToString(), maze);
            //adding the game to the lobby till someone asks to join
            gamesLobby.Add(name, maze);

            return "blalbla";
        }
        
        public string getListOfGames()
        {
            List<string> gamesList = new List<string>(this.gamesLobby.Keys);

            return JsonConvert.SerializeObject(gamesList);
        }
        

        public string solve(string name,string algo)
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

                if (String.Compare(algo,"bfs") == 0)
                {
                    Solution<Position> bfsSolution = bfsSolver.search(searchableMaze);
                    //Json
                    solveObj["Solution"] = calculateSolution(bfsSolution);
                    solveObj["NodesEvaluated"] = bfsSolver.getNumberOfNodesEvaluated().ToString();
                }
                else
                {
                    Solution<Position> dfsSolution = dfsSolver.search(searchableMaze);
                    //Json                   
                    solveObj["Solution"] = calculateSolution(dfsSolution);
                    solveObj["NodesEvaluated"] = dfsSolver.getNumberOfNodesEvaluated().ToString();
                }
                //adding the solution to the dictionary.
                mazeSolutions.Add(name, solveObj.ToString());
                return solveObj.ToString();
            }

        }

        private string calculateSolution(Solution<Position> solution)
        {
            StringBuilder pathToReturn = new StringBuilder();
            foreach (State<Position> position in solution.PathToGoal)
            {
                //if we came from the same row
                if (position.stateInstance.Row == position.CameFrom.stateInstance.Row)
                {
                    if (position.stateInstance.Col > position.CameFrom.stateInstance.Col)
                    {
                        pathToReturn.Append("1");
                    }
                    else
                    {
                        pathToReturn.Append("0");
                    }
                }
                //if we came from the same col
                else
                {
                    if (position.stateInstance.Row > position.CameFrom.stateInstance.Row)
                    {
                        pathToReturn.Append("3");
                    }
                    else
                    {
                        pathToReturn.Append("2");
                    }
                }

            }
            return pathToReturn.ToString();
        }
    }


}
