using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class ServerTest
    {
        static void Main(string[] args)
        {
            IController controller = new Controller();
            IClientHandler ch = new ClientHandler(controller);
            Server server = new Server(49251, ch);
            server.start();
        }
    }
}
