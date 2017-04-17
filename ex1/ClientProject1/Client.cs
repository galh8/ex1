﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using MazeLib;



namespace ClientProject
{
    class Client
    {
        private Maze currentMaze { set; get; }



        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");

            //StringBuilder commandLine = new StringBuilder();
            ////building a string of the args.
            //for (int i = 0; i < args.Length; i++)
            //{
            //    commandLine.Append(args[i]);
            //    commandLine.Append(" ");
            //}

            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {

                Thread sender = new Thread(delegate ()
            {

                {
                    while (true)
                    { 
                    Console.WriteLine("type a command");
                    string command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                        //writer.WriteLine("");

                        // Thread.Sleep(3000)
                    }
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
                
                receiver.Start();
                receiver.Join();
                sender.Join();
            }



            //using (NetworkStream stream = client.GetStream())
            //using (StreamReader reader = new StreamReader(stream))
            //using (StreamWriter writer = new StreamWriter(stream))
            //{
            //    while (true)
            //    {
            //        // Send data to server
            //        Console.WriteLine("type a command");
            //        string command = Console.ReadLine();
            //        writer.WriteLine(command);
            //        writer.Flush();

            //        if (String.Compare(command, "close") == 0)
            //        {
            //            //need to add here something of course.
            //            break;
            //        }

            //        // Get result from server
            //        string result = reader.ReadLine();
            //        Console.Write( result);
            //    }
            //}
            //client.Close();


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
}

