using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ServerProject
{
    class ClientHandler : IClientHandler
    {
        private IController controller;
        
        public ClientHandler(IController controller)
        {
            this.controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
                 new Task(() =>
                { 
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    string result = controller.executeCommand(commandLine, client);
                    writer.Write(result);
                }
                    //maybe we shouldnt close it...
                    client.Close();
            }).Start();
        }
    }
}
