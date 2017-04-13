using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;


using System.IO;


namespace ClientProject
{
    class Client
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 55555);
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
            while (true)
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    Console.WriteLine("Starting client");
                    writer.Write(commandLine.ToString());
                    string c = Console.ReadLine();
                    string result = reader.ReadLine();
                    Console.WriteLine("Result = {0} ", result);
                }
            }
            client.Close();

            //Task sender = new Task(() =>
            //{
            //    using (NetworkStream stream = client.GetStream())
            //    using (StreamWriter writer = new StreamWriter(stream))
            //    {

            //            writer.Write(commandLine);
            //            Console.WriteLine("message has been sent to the server");

            //    }
            //});

            //sender.Start();
            //sender.Wait();

            //Task receiver = new Task(() =>
            //{
            //    using (NetworkStream stream = client.GetStream())
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        try {
            //            string result = reader.ReadLine();
            //            Console.WriteLine("message has been received to the server");
            //        }catch(IOException)
            //        {
            //            Console.WriteLine("EXCePTIONNNNNNNNNNNN");                    }

            //    }
            //});

            //receiver.Start();
            //receiver.Wait();



        }
       
    }
}
