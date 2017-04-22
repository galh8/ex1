using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerProject
{
    public interface IView
    {
        void HandleClient(TcpClient client);
        void notifyClient(string message, TcpClient client);
    }
}
