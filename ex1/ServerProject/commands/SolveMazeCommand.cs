﻿using System;
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
    class SolveMazeCommand : ICommand
    {
        private Model model;
        public SolveMazeCommand(Model model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string mazeName = args[0];
            int algo = int.Parse(args[1]);

            string solution = model.solve(mazeName,algo);

            return solution;
        }
    }
}