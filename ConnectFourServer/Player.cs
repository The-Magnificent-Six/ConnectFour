using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    
    class Player : User 
    {
        public Room room;
        int tokencolor;//must be different player ID
        public int TokenColor { get => tokencolor; }

        public Player(Socket s):base(s){}
        public Player(User u):base(u.Socket)
        {
            this.setName(u.name);
            this.networkStream = u.netStream;
            // this.WaitForMove();
        }
        public void WaitForMove()
        {
            while(true)
            {
                if(networkStream.CanRead)
                {
                    BinaryReader reader = new BinaryReader(networkStream);
                    commOp op = (commOp) int.Parse( reader.ReadStringIgnoreNull());
                    if(op == commOp.playerMoveReq)
                    {
                        int x = int.Parse( reader.ReadStringIgnoreNull());
                        int y = int.Parse( reader.ReadStringIgnoreNull());
                        int TokenColor = int.Parse( reader.ReadStringIgnoreNull());
                        if (!room.makeAMove(x,y,this))
                        {
                            if(this == room.Players[0] )
                                room.Players[1].WaitForMove();
                            else 
                                room.Players[0].WaitForMove();
                        }
                        else
                            return;
                    }
                    else
                    {
                        BinaryWriter writer = new BinaryWriter(networkStream);
                        sendError($"received {op} instead of move Request");
                    }
                    return;
                }
            }
        }
        public void setToken( int tokenColor)
        {
            tokencolor = tokenColor;
        }
        public void setRoom( Room r )
        {
            room = r;
        }

        public void sendWinToPlayer()
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write(((int)commOp.winLoss).ToString());
            bw.Write("Veni, Vidi, Vici");
        }
        public void sendLossToPlayer()
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write(((int)commOp.winLoss).ToString());
            bw.Write("you were outmatched sir");
        }
    }
}
