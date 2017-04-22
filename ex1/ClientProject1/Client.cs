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
        public Client()
        {

        }

        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
            //Console.WriteLine(port + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client has been connected");
            
            string command = null;
            bool getNewCommand = true;
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            {
                //always working till the user himself exit the program via the GUI.
                while (true)
                {
                    bool isMultiplayerGame = false;
                    if (getNewCommand)
                    {
                        command = Console.ReadLine();
                    }
                    getNewCommand = true;
                    if (!client.Connected)
                    {
                        client = new TcpClient();
                        client.Connect(ep);
                        stream = client.GetStream();
                        reader = new StreamReader(stream);
                        writer = new StreamWriter(stream);
                    }
                    if ((command.Contains("join")) || (command.Contains("start")))
                    {
                        isMultiplayerGame = true;
                    }
                    writer.WriteLine(command);
                    writer.Flush();
                    while (true)
                    {
                        string feedback = reader.ReadLine();
                        if (reader.Peek() == '@')
                        {
                            Console.WriteLine("{0}", feedback);
                            feedback.TrimEnd('\n');
                            break;
                        }
                        Console.WriteLine("{0}", feedback);
                        //if the name of the game already exists, or the game we want to join
                        //does not exists - exits multiplayer mode.
                        if (feedback.Contains("try another name"))
                            {
                            isMultiplayerGame = false;
                        }
                    }
                    reader.ReadLine();
                    if (isMultiplayerGame)
                    {
                        bool close = false;
                        Task sendTask = new Task(() =>
                        {
                            while (!close)
                            {

                                //varifies the player use just multiplayer commands in multi mode.
                                command = Console.ReadLine();
                                if (!command.Contains("close") && !command.Contains("play") &&
                                    !close)
                                {
                                    Console.WriteLine("Multiplayer game.Please enter new command");
                                    continue;
                                }

                                //if the player wants to close the session - it will be closed.
                                if (command.Contains("close"))
                                {
                                    close = true;
                                }

                                writer.WriteLine(command);
                                writer.Flush();
                            }
                        });
                        Task listenTask = new Task(() =>
                        {
                            while (!close)
                            {
                                string feedback;
                                while (true)
                                {
                                    feedback = reader.ReadLine();
                                    if (reader.Peek() == '@')
                                    {
                                        {
                                            if (feedback != "close")
                                            {
                                                Console.WriteLine("{0}", feedback);
                                            }
                                        }
                                        feedback.TrimEnd('\n');
                                        break;
                                    }
                                    Console.WriteLine("{0}", feedback);
                                }
                                reader.ReadLine();
                                if (feedback == "close")
                                {
                                    writer.WriteLine(feedback);
                                    writer.Flush();
                                    close = true;
                                    Console.WriteLine("other player closed connection");
                                    getNewCommand = false;
                                }
                            }
                        });
                        sendTask.Start();
                        listenTask.Start();
                        sendTask.Wait();
                        listenTask.Wait();
                    }
                    client.Close();
                }
                stream.Dispose();
                writer.Dispose();
                reader.Dispose();
            }
        }
    }
}