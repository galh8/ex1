using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ServerProject
{
    public interface IController
    {
        void setModel(IModel m);
        void setView(IClientHandler v);
       // void setSolution(Solution<T> s);
        string executeCommand(String problem,TcpClient client );
    }
}
