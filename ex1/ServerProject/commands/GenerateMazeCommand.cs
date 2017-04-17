﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;

namespace ServerProject.commands
{
    class GenerateMazeCommand : ICommand
    {
        private Model model;
        public GenerateMazeCommand(Model model)
        {
            this.model = model;
        }


        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = model.GenerateMaze(name, rows, cols);
            maze.Name = name;
            Console.WriteLine("maze created");
            return maze.ToJSON();
            
        }
    }
}