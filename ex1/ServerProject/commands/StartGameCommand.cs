using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using SearchAlgorithmsLib;

namespace ServerProject
{
    class StartGameCommand : ICommand
    {
        private Model model;
        public StartGameCommand(Model model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string gameName = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            //what to do here?
            string solution = model.startGame(gameName, rows, cols);

            return solution;
        }
    }
}