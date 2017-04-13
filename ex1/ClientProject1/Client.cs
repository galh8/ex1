﻿using System;
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
            System.Threading.Thread.Sleep(3000);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49251);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(args.ToString());
                string result = reader.ReadString();
                Console.WriteLine("Result = {0} ", result);
                Console.Read();
            }
            client.Close();
            
        }
       
    }
}
