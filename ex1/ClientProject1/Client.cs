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


//--------------------------------------------------------------------------------------------------
//namespace ClientProject
//{
//    class Client
//    {
//        private Maze currentMaze { set; get; }



//        static void Main(string[] args)
//        {
//            //int Port =  int.Parse(Console.ReadLine());

//            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
//            TcpClient client = new TcpClient();
//            client.Connect(ep);
//            Console.WriteLine("Client connected");
//            bool isMultiplayer = false;
//            bool closeMultiplayer = false;
//            bool newInput = false;
//            bool inputStarted = false;
//            bool gotFeedback = false;
//            string clientCommand = "";

//            using (NetworkStream stream = client.GetStream())
//            using (StreamReader reader = new StreamReader(stream))
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                while (true)
//                {
//                    Task multiPlayerReader = new Task(() =>
//                    {
//                        while (!closeMultiplayer)
//                        {
//                            while (true)
//                            {
//                                string feedback = reader.ReadLine();
//                                if (feedback.Contains("close"))
//                                {
//                                    //Console.WriteLine("Other client notified");
//                                    closeMultiplayer = true;
//                                    gotFeedback = true;
//                                    break;
//                                }
//                                if (reader.Peek() == '@')
//                                {
//                                    feedback.TrimEnd('\n');
//                                    break;
//                                }
//                                Console.WriteLine("{0}", feedback);
//                            }
//                            reader.ReadLine();
//                            gotFeedback = true;
//                        }
//                        gotFeedback = true;
//                        Console.WriteLine("reader thread finished");
//                        //Thread.CurrentThread.Abort();
//                    });

//                    Task multiPlayerWriter = new Task(() =>
//                    {
//                        Console.WriteLine("multiPlayerWriter got {0} command", clientCommand);
//                        while (!closeMultiplayer)
//                        {
//                            // Console.Write("Please enter a multiplayer command: ");
//                            //     string multiplayerCommand = Console.ReadLine();
//                            while (newInput)
//                            {
//                                newInput = false;

//                                writer.WriteLine(clientCommand);
//                                writer.Flush();
//                                if (clientCommand.Contains("close"))
//                                {
//                                    Console.WriteLine("Client stops the thread");
//                                    closeMultiplayer = true;
//                                    break;
//                                }
//                            }
//                        }
//                        newInput = false;
//                        Console.WriteLine("MYself while stopped");
//                        //Thread.CurrentThread.Abort();
//                        //multiPlayerReader.Dispose();
//                        //Thread.CurrentThread.Abort();
//                    });

//                    Task consoleReadTask = new Task(() =>
//                    {
//                        while (true)
//                        {
//                            //Sending a command to server
//                            Console.Write("Please enter a command: ");
//                            clientCommand = Console.ReadLine();
//                            newInput = true;
//                            if (clientCommand.Contains("start") || clientCommand.Contains("join"))
//                            {
//                                closeMultiplayer = false;
//                                gotFeedback = false;
//                                Console.WriteLine("STARTING MULTIPLAYER!");


//                                multiPlayerWriter.Start();
//                                Console.WriteLine("multiPlayerWriter started");
//                                //Thread.Sleep(5);
//                                multiPlayerReader.Start();
//                                Console.WriteLine("multiPlayerReader started");
//                                while (!gotFeedback) { }
//                                gotFeedback = false;
//                                Console.WriteLine("AFTERRRRRRR BLOCK");
//                                isMultiplayer = true;

//                            }
//                            if (clientCommand.Contains("close"))
//                                closeMultiplayer = true;
//                        }
//                    });
//                    if (!inputStarted)
//                    {
//                        inputStarted = true;
//                        consoleReadTask.Start();
//                    }

//                    if (isMultiplayer)
//                    {
//                        while (!closeMultiplayer) { }
//                        multiPlayerReader.Wait();
//                        //Console.WriteLine("After multiPlayerReader WAITSSSSS");
//                       // multiPlayerWriter.Wait();
//                       // Console.WriteLine("After multiPlayerWriter WAITSSSSS");
//                        // multiPlayerWriter.Dispose();
//                        //multiPlayerReader.Dispose();

//                        Console.WriteLine("After WAITSSSSS");
//                        isMultiplayer = false;
//                    }
//                }          
//             }
//                //client.Close();
//         } 

//                //client.Close();
//      } // end of the class
//---------------------------------------------------------------------------------------------------------------
// }
namespace ClientProject
{
    class Client
    {
        static void Main(string[] args)
        {
            bool isMultiplayer = false;
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            client.Connect(ep);
            Console.WriteLine("Client connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
            //bool newInput = false;
            bool closeMultiplayer = false;

            

            string clientCommand = "";

                while (true)
                {
                    Console.Write("Please enter a command: ");
                    clientCommand = Console.ReadLine();
                    //newInput = true;
                    if (clientCommand.Contains("start") || clientCommand.Contains("join"))
                    {
                        Task multiPlayerReader = new Task(() =>
                        {
                            while (!closeMultiplayer)
                            {
                                while (true)
                                {
                                    string feedback = reader.ReadLine();
                                    if (feedback.Contains("close"))
                                    {
                                        //Console.WriteLine("Other client notified");
                                        closeMultiplayer = true;
                                        //gotFeedback = true;
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
                        });
                        closeMultiplayer = false;
                        isMultiplayer = true;
                        multiPlayerReader.Start();
                    }
                    writer.WriteLine(clientCommand);
                    writer.Flush();
                    if (clientCommand.Contains("close"))
                    {
                        Console.WriteLine("Client stops the thread");
                        closeMultiplayer = true;
                    }

                }
            }
        }
    }
}



