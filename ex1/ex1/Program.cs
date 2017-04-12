using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace ex1a
{
    class Program
    {
        static void Main(string[] args)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(5, 5);
            Console.WriteLine(maze.ToString());

            SearchableMaze searchableMaze = new SearchableMaze(maze);
            BFS<Position> bfsSolver = new BFS<Position>();
            DFS<Position> dfsSolver = new DFS<Position>();
            Console.WriteLine("BFS:");
            Console.WriteLine(bfsSolver.search(searchableMaze));
            Console.WriteLine(bfsSolver.getNumberOfNodesEvaluated());

            Console.WriteLine("DFS:");
            Console.WriteLine(dfsSolver.search(searchableMaze));
            Console.WriteLine(dfsSolver.getNumberOfNodesEvaluated());

            Console.Read();
        }
    }
}
