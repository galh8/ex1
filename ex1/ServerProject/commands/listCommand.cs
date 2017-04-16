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

namespace ServerProject
{
    class listCommand : ICommand
    {
        private Model model;
        public listCommand(Model model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {

            string listOfGames = model.getListOfGames();

            return listOfGames;
        }
    }
}