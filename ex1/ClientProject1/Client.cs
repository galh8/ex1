using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


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
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                Console.WriteLine("Starting client");
                Console.WriteLine(args.ToString());
                writer.Write(args.ToString());
                string result = reader.ReadString();
                Console.WriteLine("Result = {0} ", result);
                Console.Read();
            }
            client.Close();
        
        }
       
    }
}
