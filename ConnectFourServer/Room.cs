using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Room
    {
        public Board board;
        String roomName;
        public String name { get; }
        Player[] players;
        public Player[] Players { get => players; }
        List<Spectator> spectators = new List<Spectator>() ;
        public List<Spectator> Spectators { get => spectators; }
        public Room(string rName, int boardRows , int boardCols , Player p)
        {
            roomName = rName;
            board = new Board(boardRows,boardCols);
            players = new Player[2];
            players[0] = p;
            players[0].room = this;
            players[1] = null;
        }

        public bool makeAMove(int x , int y,Player p)
        {
            bool someOneWon = board.play(x,y,p.TokenColor);
            broadcastMove(x,y,p);
            if(someOneWon)
                broadcastWin(p);
            return someOneWon;
        }

        void broadcastWin(Player p)
        {
                p.sendWinToPlayer();
                foreach (Player player in players)
                    if (player.TokenColor != p.TokenColor)
                        p.sendLossToPlayer();
                foreach (Spectator spectator in spectators)
                        spectator.sendPlayerXWon(p);
        }
        void broadcastMove(int x,int y,Player p )
        {
            foreach (Player player in players)
            {
                player.SendMoveToUser(x,y,p.TokenColor);
            }
            foreach (Spectator spectator in spectators)
            {
                spectator.SendMoveToUser(x,y,p.TokenColor);
            }
        }
        public bool isPlayersIncomplete()
        {
            return (players[1] == null);
        }

        public void addPlayer(Player p)
        {
            players[1] = p;
            // if(isPlayersIncomplete())
            // {
                // players[1] = p;
                // players[1].room = this;
                //p.sendRoomDetails(this);
            // }
            // else
            // {

            // }
        }
        public void addSpectator(Spectator s)
        {
            spectators.Add(s);
            //s.sendRoomDetails(this);
        }

    }
}
