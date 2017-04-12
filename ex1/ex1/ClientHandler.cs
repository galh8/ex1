using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ex1
{
    class ClientHandler : IClientHandler
    {
        private 

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string commandLine = reader.ReadLine();

            }

            ).Start();
        }
    }
}
