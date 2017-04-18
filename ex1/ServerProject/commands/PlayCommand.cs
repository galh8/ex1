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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ServerProject.commands
{
    class PlayCommand : ICommand
    {
        private Model model;
        private IClientHandler clientHendler;
        public PlayCommand(IClientHandler clientHendler, Model model)
        {
            this.model = model;
            this.clientHendler = clientHendler;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];
            TcpClient clientToNotify = model.getOtherPlayerClient(client);
            JObject solveObj = new JObject();
            solveObj["Name"] = model.getClientGameName(client);
            solveObj["Direction"] = direction;
            //Console.WriteLine(solveObj.ToString());
            //Console.ReadLine();
            clientHendler.notifyClient(solveObj.ToString(), clientToNotify);
            Console.WriteLine("After notify");
            //makes the move and returns the direction the other player moved.
            //string otherPlayerDirection = model.play(direction, client);
            return "Other Player notified!";
        }
    }
}
