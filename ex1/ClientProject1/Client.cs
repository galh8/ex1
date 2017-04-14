using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading;

using System.IO;


namespace ClientProject
{
    class Client
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");
            StringBuilder commandLine = new StringBuilder();
            //building a string of the args.
            for (int i = 0; i < args.Length; i++)
            {
                commandLine.Append(args[i]);
                commandLine.Append(" ");
            }
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {

                Thread sender = new Thread(delegate ()
            {

                {
                    //Console.WriteLine("Starting client");

                    writer.WriteLine(commandLine.ToString());
                    writer.Flush();
                    //writer.WriteLine("");

                    // Thread.Sleep(3000)
                }
            });
               

                Thread receiver = new Thread(delegate ()
                {

                    {
                        while (true)
                        {
                            //Console.WriteLine("Starting client");
                            string result = reader.ReadLine();
                            Console.WriteLine(result);
                        }
                    }
                });
                sender.Start();
                sender.Join();
                receiver.Start();
                receiver.Join();
            }


        }


        //using (NetworkStream stream = client.GetStream())
        //using (StreamReader reader = new StreamReader(stream))
        //using (StreamWriter writer = new StreamWriter(stream))
        //{
        //    Console.WriteLine("Starting client");
        //    writer.WriteLine(commandLine.ToString());
        //   // Console.WriteLine("BLABLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        //    //Console.WriteLine("Before sleeping");
        //    // System.Threading.Thread.Sleep(3000);
        //    //string result = reader.ReadLine();
        //   // Console.WriteLine("Result = {0} ", result);

        //}


    }
       
    }

