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

            //using (NetworkStream stream = client.GetStream())
            //using (StreamReader reader = new StreamReader(stream))
            //using (StreamWriter writer = new StreamWriter(stream))
            //{

            //Thread sender = new Thread(delegate ()
            //{

            //    {
            //        Console.WriteLine("type a command");
            //        string command = Console.ReadLine();
            //        writer.WriteLine(command);
            //        writer.Flush();
            //    }
            //});


            //    Thread receiver = new Thread(delegate ()
            //    {

            //        {
            //            while (true)
            //            {
            //                //Console.WriteLine("Starting client");
            //                string result = reader.ReadLine();
            //                Console.WriteLine(result);
            //            }
            //        }
            //    });

            //    do
            //    {
            //        sender.Start();
            //        sender.Join();
            //        receiver.Start();
            //        receiver.Join();
            //    } while (true);
            //}



            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                while (true)
                {
                    // Sending a command to server
                    Console.Write("Please enter a command: ");
                    string command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    Console.WriteLine("after sending command");
                    Console.WriteLine("{0}", command);

                    //reading a reply from server   
                    while (true)
                    {
                        string feedback = reader.ReadLine();
                        if (reader.Peek() == '@')
                        {
                            feedback.TrimEnd('\n');
                            break;
                        }
                        Console.WriteLine("{0}", feedback);
                    }


                    //************TODO - ADD a condition of receiving empty jason obj to stop loop*****
                }


                //get result from server
                //string result = FromJSON(reader.read());
                // Get result from server
                //int result = reader.ReadInt32();
                //Console.WriteLine("Result = {0}", result);
            }
            client.Close();

        }

    }
}

