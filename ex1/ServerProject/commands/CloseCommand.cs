using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.commands
{
    class CloseCommand : ICommand
    {
        private Model model;
        private IClientHandler clientHendler;

        public CloseCommand(IClientHandler clientHendler, Model model)
        {
            this.model = model;
            this.clientHendler = clientHendler;
        }


        public string Execute(string[] args, TcpClient client )
        {
            TcpClient clientToNotify = model.getOtherPlayerClient(client);
            clientHendler.notifyClient("close_connection", clientToNotify);
            Console.WriteLine("Closing client connection");
            return "close_connection";
        }
    }
}
