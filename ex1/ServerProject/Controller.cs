using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerProject.commands;

namespace ServerProject
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private Model model;
        private IClientHandler view;
        private bool isMultiplayer;

        public Controller()
        {
            isMultiplayer = false;
        }

        public void buildController(Model model, IClientHandler view)
        {
            this.model = model;
            this.view = view;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("start", new StartGameCommand(model));
            commands.Add("list", new listCommand(model));
            commands.Add("play", new PlayCommand(view, model));
            commands.Add("close", new CloseCommand(view, model));
        }


        public string executeCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";

            
            if (commandKey.Contains("start")  || commandKey.Contains("join")) {
                isMultiplayer = true;
            }
        
            //checking and modifying the player play status
            if ((commandKey.Contains("close")) || commandKey.Contains("play"))
            {
                if (!isMultiplayer)
                {
                    return "Not a multiplayer game";
                }
                else
                {
                    isMultiplayer = false;
                }
            }
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }

        public void setModel(IModel m)
        {
            throw new NotImplementedException();
        }


        //public void setSolution(Solution<T> s)
        //{
        //   throw new NotImplementedException();
        //}

        public void setView(IClientHandler v)
        {
            this.view = v;
            
        }
    }
}
