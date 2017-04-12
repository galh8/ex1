﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IClientHandler view;

        public Controller()
        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            //....
        }

        public string executeCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }

        public void setModel(IModel m)
        {
            throw new NotImplementedException();
        }

        public void setSolution(SearchAlgorithmsLib.Solution<T> s)
        {
            throw new NotImplementedException();
        }

        public void setView(IClientHandler v)
        {
            throw new NotImplementedException();
        }
    }
}
