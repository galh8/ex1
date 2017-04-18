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
            //int Port =  int.Parse(Console.ReadLine());

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");
            bool isMultiplayer = false;

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
                    if (command.Contains("start") || command.Contains("join"))
                    {
                        isMultiplayer = true;
                    }
                    writer.WriteLine(command);
                    writer.Flush();
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
                    reader.ReadLine();

                    if (isMultiplayer)
                    {
                        Task multiplayerReader = new Task(() =>
                        {
                            while (true)
                            {
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
                                reader.ReadLine();
                            }
                            
                        });



                        
                        Task multiplayerWriter = new Task(() =>
                        {
                            while (true)
                            {
                                Console.Write("Please enter a multiplayer command: ");
                                string multiplayerCommand = Console.ReadLine();
                                if (command.Contains("close"))
                                {
                                    multiplayerReader.Dispose();
                                    break;
                                }
                                writer.WriteLine(multiplayerCommand);
                                writer.Flush();
                                Console.WriteLine("{0}", multiplayerCommand);
                            }

                        });

                        multiplayerReader.Start();
                        multiplayerWriter.Start();

                        multiplayerReader.Wait();
                        multiplayerWriter.Wait();

                     } // end of if multiplayer

                        //while (true)
                        //{
                        //    Console.Write("Please enter a multiplayer command: ");
                        //    string multiplayerCommand = Console.ReadLine();
                        //    if (command.Contains("close"))
                        //    {
                        //        multiplayerReader.Dispose();
                        //        break;
                        //    }
                        //    writer.WriteLine(multiplayerCommand);
                        //    writer.Flush();
                        //    Console.WriteLine("{0}", multiplayerCommand);
                        //}

                        //************TODO - ADD a condition of receiving empty jason obj to stop loop*****
                    } // end of while (true)


                    //get result from server
                    //string result = FromJSON(reader.read());
                    // Get result from server
                    //int result = reader.ReadInt32();
                    //Console.WriteLine("Result = {0}", result);
                } // end of using
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

                client.Close();
            } // end of the class


        }
    }


