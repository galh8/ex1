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
                        bool close = false;
                        Task multiplayerReader = new Task(() =>
                        {
                            
                            while (!close)
                            {
                                
                                while (!close)
                                {
                                    string feedback = reader.ReadLine();
                                    if (feedback.Contains("close_connection"))
                                    {
                                        Console.WriteLine("Other client notified");
                                        close = true;
                                        break;
                                    }
                                    if (reader.Peek() == '@')
                                    {
                                        feedback.TrimEnd('\n');
                                        break;
                                    }
                                    Console.WriteLine("{0}", feedback);

                                }
                                
                                reader.ReadLine();
                            }
                            Console.WriteLine("Other while loop ended");
                            
                        });



                        
                        Task multiplayerWriter = new Task(() =>
                        {
                            while (!close)
                            {
                                Console.Write("Please enter a multiplayer command: ");
                                try
                                {
                                    string multiplayerCommand = Console.ReadLine();
                                    writer.WriteLine(multiplayerCommand);
                                    writer.Flush();
                                    if (multiplayerCommand.Contains("close"))
                                    {
                                        Console.WriteLine("Client stops the thread");
                                        close = true;
                                        break;
                                    }
                                }catch (SocketException s)
                                {
                                    break;
                                }
                                
                               // Console.WriteLine("{0}", multiplayerCommand);
                            }
                            Console.WriteLine("MYself while stopped");

                        });

                        multiplayerReader.Start();
                        multiplayerWriter.Start();

                        multiplayerReader.Wait();
                        
                        //multiplayerWriter.di
                        //multiplayerWriter.Wait();

                        //multiplayerWriter;
                        Console.WriteLine("After Writer wait");

                    }
                    
                }
                client.Close();
             } 

                //client.Close();
            } // end of the class


        }
    }


