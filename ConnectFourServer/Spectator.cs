using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Spectator:User
    {
        Room room;
        public Spectator(Socket s,Room r):base(s){ this.room = r; }
        public Spectator(User u, Room r) :this(u.Socket,r)
        {
            this.setName(u.name);
            this.networkStream = u.netStream;
            sendCurrentRoom();
        }

        private void sendCurrentRoom()
        {
            BinaryWriter writer = new BinaryWriter(networkStream);

            writer.Write(((int)commOp.WholeBoard).ToString());

            writer.Write(room.Players[0].name.ToString());
            writer.Write(room.Players[0].TokenColor.ToString());
            writer.Write(room.Players[1].name.ToString());
            writer.Write(room.Players[1].TokenColor.ToString());
            writer.Write(room.board.rows.ToString());
            writer.Write(room.board.columns.ToString());
            for (int i = 0; i < room.board.rows; i++)
            {
                for (int j = 0; j < room.board.columns; j++)
                {
                    writer.Write( room.board.matrix[i,j].ToString() );
                }
            }
        }

        public void sendPlayerXWon(Player p)
        {
            BinaryWriter writer = new BinaryWriter(networkStream);
            writer.Write(((int)commOp.winLoss).ToString());
            writer.Write($"Player {p.name} won ");
        }

    }
}
