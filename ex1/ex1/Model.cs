using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;


namespace ex1
{
    class Model : IModel
    {
        private Dictionary<string, string> mazeSolutions;
        DFSMazeGenerator mazeGenerator;
        BFS<Position> bfsSolver;
        DFS<Position> dfsSolver;
        public Model()
        {
            mazeGenerator = new DFSMazeGenerator();
            BFS<Position> bfsSolver = new BFS<Position>();
            DFS<Position> dfsSolver = new DFS<Position>();
            mazeSolutions = new Dictionary<string, string>();
        }

        public void search(string problem)
        {
            throw new NotImplementedException();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            
            Maze maze = mazeGenerator.Generate(rows, cols);
            mazeSolutions.Add(name,"null");
            return maze;
        }

        public void solve(string name,string algo)
        {
            if (mazeSolutions.ContainsKey(name))
            {
                return;
            }
            else
            {
                
            }

        }
    }
}
