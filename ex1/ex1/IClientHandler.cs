﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
