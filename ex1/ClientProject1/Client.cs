using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Configuration;

namespace Client
{
    public class Client
    {
        //MEMBERS
        private int port;

        //CONSTRUCTOR
        public Client()
        {
            
        }

        /****************
        start - creates a connection with the server
        and sends messages saying what command it wants
        to be executed by the sever.
        ****************/
        /// <summary>
        /// Starts this instance.
        /// </summary>
        static void Main(string[] args)
        {
            //int Port =  int.Parse(Console.ReadLine());

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client connected");
            bool isMultiplayer = false;
            bool isClose = false;
            bool isNewSPCommand = false;
            bool IsMPReaderClosed = false;

            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string command = null;
                bool isNewCommand = false;
                Task commandLineReader = new Task(() =>
                {
                    while (true)
                    {
                        Console.WriteLine("Please enter a command: ");
                        command = Console.ReadLine();
                        isNewCommand = true;
                        if (command.Contains("start") || command.Contains("join"))
                        {
                            isMultiplayer = true;
                        }
                        if (command.Contains("close"))
                        {
                            isClose = true;
                        }
                    }

                });
                commandLineReader.Start();
                while (true)
                {
                    isMultiplayer = false;
                    isClose = false;
                    isNewSPCommand = false;
                    IsMPReaderClosed = false;


                    if (isNewCommand)
                    {
                        writer.WriteLine(command);
                        writer.Flush();
                        isNewCommand = false;
                        isNewSPCommand = true;
                    }

                    //reading a reply from server
                    if (isNewSPCommand)
                    {
                        while (true)
                        {
                            string feedback = reader.ReadLine();
                            if (reader.Peek() == '@' && !command.Contains("play"))
                            {
                                feedback.TrimEnd('\n');
                                break;
                            }
                            if (reader.Peek() == '@' && command.Contains("play"))
                            {
                                break;
                            }
                            Console.WriteLine("{0}", feedback);
                        }
                        reader.ReadLine();
                        isNewSPCommand = false;
                    }


                    if (isMultiplayer)
                    {
                        Task multiplayerReader = new Task(() =>
                        {
                            while (isClose == false)
                            {
                                Console.WriteLine("waiting to read a multiplayer command: ");
                                while (true)
                                {

                                    string feedback = reader.ReadLine();
                                    if (reader.Peek() == '@')
                                    {
                                        feedback.TrimEnd('\n');
                                        break;
                                    }
                                    Console.WriteLine("{0}", feedback);
                                    if (feedback.Contains("close"))
                                    {
                                        Console.WriteLine("updating is close to true ");
                                        isClose = true;
                                        break;
                                    }
                                }
                                reader.ReadLine();

                            }
                            IsMPReaderClosed = true;
                            isMultiplayer = false;
                            Console.WriteLine("exiting multiplayerReader");



                        });




                        Task multiplayerWriter = new Task(() =>
                        {
                            while (isClose == false)
                            {
                                //Console.Write("Please enter a multiplayer command: ");
                                string multiplayerCommand = null;
                                if (isNewCommand)
                                {
                                    Console.WriteLine("entered is new command tnay ");
                                    multiplayerCommand = command;
                                    isNewCommand = false;
                                    writer.WriteLine(multiplayerCommand);
                                    writer.Flush();
                                    Console.WriteLine("{0}", multiplayerCommand);
                                    if (multiplayerCommand.Contains("close"))
                                    {
                                        Console.WriteLine("the command that was received is closed! ");
                                        isClose = true;
                                        //while(IsMPReaderClosed == false) { }
                                        break;
                                    }
                                }
                            }
                            Console.WriteLine("exiting multiplayerWriter");
                            isMultiplayer = false;

                        });

                        multiplayerReader.Start();
                        multiplayerWriter.Start();

                        multiplayerReader.Wait();
                        multiplayerWriter.Wait();



                    }

                }



            }


            client.Close();
        } // end of the class
    }
}


//namespace ClientProject
//{
//    class Client
//    {
//        static void Main(string[] args)
//        {
//            bool newInput = false;
//            bool closeMultiPlayer = false;
//            bool isMultiplayer = false;
//            bool newSingleCommand = false;
//            string clientCommand = "";;
//            //The console read line task


//            TcpClient client = new TcpClient();
//            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
//            client.Connect(ep);
//            Console.WriteLine("Client connected");

//            using (NetworkStream stream = client.GetStream())
//            using (StreamReader reader = new StreamReader(stream))
//            using (StreamWriter writer = new StreamWriter(stream))
//            {
//                Task consoleReadTask = new Task(() =>
//                {
//                    while (true)
//                    {
//                        Console.WriteLine("Please enter a command:");
//                        clientCommand = Console.ReadLine();
//                        newInput = true;
//                        if (clientCommand.Contains("start") || (clientCommand.Contains("join")))
//                            isMultiplayer = true;
//                        if (clientCommand.Contains("close"))
//                            closeMultiPlayer = true;
//                        //while (!newInput) { }
//                    }//end while true of console readline.
//                });
//                consoleReadTask.Start();
//                while (true)
//                {
//                    newSingleCommand = false;
//                    closeMultiPlayer = false;
//                    isMultiplayer = false;
//                    if (newInput)
//                    {
//                        writer.WriteLine(clientCommand);
//                        writer.Flush();
//                        //Sleep to wait for sever response
//                        newSingleCommand = true;
//                        //Thread.Sleep(5);
//                    }
//                        if (newSingleCommand)
//                        {
//                            while (true)
//                            {
//                                string feedback = reader.ReadLine();
//                                if (reader.Peek() == '@' && !clientCommand.Contains("play"))
//                                {
//                                    feedback.TrimEnd('\n');
//                                    break;
//                                }
//                                if (reader.Peek() == '@' && clientCommand.Contains("play"))
//                                    break;
//                                Console.WriteLine("{0}", feedback);
//                            }
//                            reader.ReadLine();
//                            newSingleCommand = false;
//                        }


//                    if (isMultiplayer)
//                    {
//                        Task multiPlayerReader = new Task(() =>
//                        {
//                            while (!closeMultiPlayer)
//                            {

//                                while (true)
//                                {
//                                    string feedback = reader.ReadLine();
//                                    if (reader.Peek() == '@')
//                                    {
//                                        feedback.TrimEnd('\n');
//                                        break;
//                                    }
//                                    Console.WriteLine("{0}", feedback);
//                                    if (feedback.Contains("close"))
//                                    {
//                                        closeMultiPlayer = true;
//                                        break;
//                                    }

//                                }
//                                reader.ReadLine();
//                            }
//                            isMultiplayer = false;
//                        });

//                        Task multiPlayerWriter = new Task(() =>
//                        {
//                            while (!closeMultiPlayer)
//                            {
//                                if (newInput)
//                                {
//                                    newInput = false;
//                                    writer.WriteLine(clientCommand);
//                                    writer.Flush();
//                                    if (clientCommand.Contains("close"))
//                                    {
//                                        closeMultiPlayer = true;
//                                        break;
//                                    }
//                                }
//                            }
//                            isMultiplayer = false;
//                        });
//                            multiPlayerReader.Start();
//                            multiPlayerWriter.Start();
//                            multiPlayerReader.Wait();
//                            multiPlayerWriter.Wait();
//                    }//is multiplayer
//                }

//            }//Outer while true
//        }
//        }
//    }


//        static void Main(string[] args)
//        {
//            bool isMultiplayer = false;
//            //bool newInput = false;
//            bool closeMultiplayer = false;
//            bool newInput = true;
//            bool gotFeedback = false;
//            string clientCommand = "";
//            while (true)
//            {
//                TcpClient client = new TcpClient();
//                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
//                client.Connect(ep);
//                Console.WriteLine("Client connected");
//                using (NetworkStream stream = client.GetStream())
//                using (StreamReader reader = new StreamReader(stream))
//                using (StreamWriter writer = new StreamWriter(stream))
//                {
//                    while (true)
//                    {
//                        if (newInput)
//                        {
//                            clientCommand = Console.ReadLine();
//                            if (clientCommand.Contains("start") || (clientCommand.Contains("join")))
//                            {
//                                    Task multiPlayerReader = new Task(() =>
//                                    {
//                                        while (!closeMultiplayer)
//                                        {
//                                            while (true)
//                                            {
//                                                string feedback = reader.ReadLine();
//                                                if (feedback.Contains("close"))
//                                                {
//                                                    closeMultiplayer = true;
//                                                    reader.DiscardBufferedData();
//                                                    gotFeedback = true;
//                                                    break;
//                                                }
//                                                if (reader.Peek() == '@')
//                                                {
//                                                    feedback.TrimEnd('\n');
//                                                    break;
//                                                }
//                                                Console.WriteLine("{0}", feedback);
//                                            }
//                                            reader.ReadLine();
//                                            //gotFeedback = true;
//                                        }
//                                        //gotFeedback = true;
//                                        Console.WriteLine("reader thread finished");
//                                    //Thread.CurrentThread.Abort();
//                                });

//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
//--------------------------------------------------------------------------------------------------
//namespace ClientProject
//{
//    class Client
//    {
//        private Maze currentMaze { set; get; }



//        static void Main(string[] args)
//        {
//            string clientCommand = "";
//            bool newInput = false;
//            bool isMultiplayer = false;
//            bool closeMultiplayer = false;




//            //bool isMultiplayer = false;
//            //bool closeMultiplayer = false;
//            //bool newInput = false;
//            //bool inputStarted = false;
//            //bool gotFeedback = false;
//            while (true)
//            {
//                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
//                TcpClient client = new TcpClient();
//                client.Connect(ep);
//                Console.WriteLine("Client connected");


//                using (NetworkStream stream = client.GetStream())
//                using (StreamReader reader = new StreamReader(stream))
//                using (StreamWriter writer = new StreamWriter(stream))
//                {
//                    Task multiPlayerWriter = new Task(() =>
//                    {
//                        while (!closeMultiplayer)
//                        {
//                            if (newInput)
//                            {
//                                writer.WriteLine(clientCommand);
//                                writer.Flush();
//                            }
//                        }
//                    });
//                    Task multiPlayerReader = new Task(() =>
//                    {

//                    });

//                    Task consoleReadTask = new Task(() =>
//                    {
//                        while (true)
//                        {
//                            clientCommand = Console.ReadLine();
//                            newInput = true;
//                            if ((clientCommand.Contains("start")) || (clientCommand.Contains("join")))
//                            {
//                                isMultiplayer = true;

//                            }
//                        }
//                    });

//                    while (true)
//                    {




//                    }
//                }
//                //client.Close();
//            }
//        }
//    }
//}


//client.Close();
//} // end of the class
//---------------------------------------------------------------------------------------------------------------


///            bool isMultiplayer = false;

////bool newInput = false;
//bool closeMultiplayer = false;




