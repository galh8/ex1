using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace ServerProject
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

            //string json = @"{
            //    'Name': 'mymaze',
            //    'Maze':
            //    '0001010001010101110101010000010111111101000001000111010101110001010001011111110100000000011111111111',
            //    'Rows': 10,
            //    'Cols': 10,
            //    'Start': {
            //        'Row': 0,
            //        'Col': 4
            //    },
            //    'End': {
            //        'Row': 0,
            //        'Col': 0
            //    }
            //}";
            //Maze maze = Maze.FromJSON(json);
            //Console.Write(maze.ToString());
            //SearchableMaze searchableMaze = new SearchableMaze(maze);
            //BFS<Position> bfsSolver = new BFS<Position>();
            //DFS<Position> dfsSolver = new DFS<Position>();
            //Console.WriteLine("BFS:");
            //Console.WriteLine(bfsSolver.search(searchableMaze));
            //Console.WriteLine(bfsSolver.getNumberOfNodesEvaluated());

            //Console.WriteLine("DFS:");
            //Console.WriteLine(dfsSolver.search(searchableMaze));
            //Console.WriteLine(dfsSolver.getNumberOfNodesEvaluated());



            Console.Read();
        }
    }
}
