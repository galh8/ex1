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

namespace ServerProject.commands
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

            Game currentGame = model.startGame(gameName, rows, cols,client);

            while (!currentGame.CanStart()) { }

            return currentGame.PlayedMaze().ToJSON() + '\n';
        }

        //public void OngameNeedsToStart(object Sorce, EventArgs e)
        //{
        //    canStart = true;
        //}

    }

}