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
    class PlayCommand : ICommand
    {
        private Model model;
        public PlayCommand(Model model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];

            //makes the move and returns the direction the other player moved.
            string otherPlayerDirection = model.play(direction, client);
            return otherPlayerDirection;
        }
    }
}
