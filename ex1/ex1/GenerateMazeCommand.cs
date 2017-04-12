using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    class GenerateMazeCommand : ICommand
    {
        private IModel model;
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args,TcpClient client)
        {
            string 
        }
    }
}
