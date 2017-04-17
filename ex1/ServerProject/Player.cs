using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerProject
{
    class Player : TcpClient
    {
        private bool isCurrentlyPlaying;
        private string gameName;

        public bool IsCurrentlyPlaying
        {
            get; set;

        }

        public string GameName
        {
            get; set;

        }

    }
}
