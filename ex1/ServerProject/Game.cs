﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using System.Net.Sockets;
using System.Net;

namespace ServerProject
{
    public class Game
    {
        //1. define a delegate - the signature of the method.
        //2. define an event based on that delegate.
        //3. raise the event.

        private System.Net.Sockets.TcpClient gameCreatorPlayer, secondPlayer;
        private bool canStart;
        Maze playedMaze;
        string name;


        public Game(System.Net.Sockets.TcpClient firstPlayer,Maze maze)
        {
            gameCreatorPlayer = firstPlayer;
            playedMaze = maze;
            canStart = false;
            name = maze.Name;
        }
        public string Name
        {
            get { return this.name; }
        }

        //we declare a delegate to a function which looks like void gameEventHandler(object source, EventArgs e).
        //public delegate void gameEventHandler(object source, EventArgs e);

        //public event gameEventHandler gameNeedsToStart;

        public void joinAnotherPlayer(System.Net.Sockets.TcpClient OtherPlayer)
        {
            secondPlayer = OtherPlayer;
            canStart = true;
            //notify
            //OngameNeedsToStart();
        }

        public TcpClient getOtherPlayer(TcpClient player)
        {
            if (player == gameCreatorPlayer)
                return secondPlayer;
            return gameCreatorPlayer;
        }

        //protected virtual void OngameNeedsToStart()
        //{
        //    if (gameNeedsToStart != null)
        //    {
        //        gameNeedsToStart(this, EventArgs.Empty);
        //    }
        //}

        public Maze PlayedMaze()
        {
            return playedMaze;
        }

        public bool CanStart()
        {
            return canStart;
        }

        public TcpClient getCreatorPlayer()
        {
            return gameCreatorPlayer;
        }
        

    }
}
