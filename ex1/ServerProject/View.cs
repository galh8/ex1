﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ServerProject
{
    class View : IView
    {
        private IController controller;
        
        public View(IController controller)
        {
            this.controller = controller;
            //TODO Check if necessary?
            controller.setView(this);
        }

        /// <summary>
        /// runs that specific client thread.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
                Task handleClient =  new Task(() =>
                {
                //string result = "dummy_start";
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                        while (true)
                        {
                            string commandLine = reader.ReadLine();
                            //Console.WriteLine(commandLine);
                            string result = controller.executeCommand(commandLine, client);
                            //Console.WriteLine("the result we wanna send: {0}", result);
                            result += '\n';
                            result += '@';
                            writer.WriteLine(result);
                            writer.Flush();

                        }
                }
                    //maybe we shouldnt close it...
                    //client.Close();
            });
            handleClient.Start();
            //handleClient.Wait();
        }

        /// <summary>
        /// Notifies specific client(other client) with a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="client">The client.</param>
        public void notifyClient (string message , TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            message += '\n';
                message += '@';
                writer.WriteLine(message);
                writer.Flush();
        }
    }
}
