using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class User
    {
        Socket socket;
        protected NetworkStream networkStream;
        public NetworkStream netStream { get => networkStream;}
        public Socket Socket { get => socket; }
        String UserName = "";
        public string name { get => UserName; }


        public User(Socket s)
        {
            socket = s;
            buildStream();
            networkController();
        }
        public void networkController()
        {
            // the comm protocol
            BinaryReader reader = new BinaryReader(networkStream);
            while(true)
            {
                while(networkStream.CanRead)
                {
                    commOp op = (commOp)reader.Read();
                    switch (op)
                    {
                        case commOp.availRoomsReq:
                            sendRooms();
                            break;
                        case commOp.createRoomReq:
                            
                            // comon' do sth
                            break;

                        case commOp.joinRoomAsPlayer:
                            // comon' do sth
                            break;
                        case commOp.joinRoomAsSpectator:
                            //join room as a spectator
                            
                            // comon' do sth
                            break;
                        default :
                            throw new Exception("sth went horribly wrong XD");
                    }
                }
            }
            
        }

        public void setName(string name)
        {
            UserName = name;
        }
        public void buildStream()
        {
            networkStream= new NetworkStream(socket);
        }

        public void SendMoveToUser(int x,int y,int PlayerTokenColor)
        {

        }

        public void sendRoomDetails(Room r)
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write(r.name);
            if (r.isPlayersIncomplete())
            {
                bw.Write(1);//num of players in room
                bw.Write(r.Players[0].TokenColor);
            }
            else
            {
                bw.Write(2); // num of players in room
            }
            bw.Write(r.Spectators.Count);

        }
        public void sendRooms()
        {
            BinaryWriter bw = new BinaryWriter( netStream );
            
            bw.Write( (int)commOp.roomsResp );
            
            bw.Write(Program.rooms.Count);
            
            foreach (Room r in Program.rooms)
            {
                sendRoomDetails(r);
            }
        }

    }
}
