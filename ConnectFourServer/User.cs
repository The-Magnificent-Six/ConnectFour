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
                if(networkStream.CanRead)
                {
                    commOp op = (commOp)reader.Read();
                    switch (op)
                    {
                        case commOp.availRoomsReq:
                            sendRooms();
                            break;


                        case commOp.createRoomReq:
                            
                            int tokenColor_ = reader.Read();
                            int rows_= reader.Read();   
                            int cols_= reader.Read();
                            string roomName_ = reader.ReadString();
                            string playerName_ = reader.ReadString();
                            bool roomNameUnique = true;
                            
                            foreach (Room r in Program.rooms)
                                if(r.name == roomName_)
                                    roomNameUnique = false;
                            
                            if ( roomNameUnique )
                            {
                                BinaryWriter writer = new BinaryWriter(networkStream);  
                                writer.Write((int)commOp.accept);
                                Player p1 = new Player(this);
                                p1.setName(playerName_);
                                p1.setToken(tokenColor_);
                                Program.rooms.Add(new Room(roomName_, rows_, cols_, p1));
                                
                                return;
                            }
                            else
                                sendError("room name is not unique");
                            
                            break;


                        case commOp.joinRoomAsPlayer:
                            int tokenColor2_ = reader.Read();
                            string player2Name_ = reader.ReadString();
                            string room2Name_ = reader.ReadString();

                            Room r_ = null;

                            foreach(Room r in Program.rooms)
                                if (r.name == room2Name_)
                                    r_ = r;
                            
                            if ( r_ == null)
                                sendError("room doesn't exist");
                            else if (!r_.isPlayersIncomplete())
                                sendError("a player has already joined");
                            else if (tokenColor2_ == r_.Players[0].TokenColor)
                                sendError("player token is not unique");
                            else
                            {
                                Player p2 = new Player(this);
                                p2.setName(player2Name_);
                                p2.setToken(tokenColor2_);
                                p2.setRoom(r_);
                                r_.addPlayer(p2);
                                p2.WaitForMove();
                                return;
                            }

                            break;


                        case commOp.joinRoomAsSpectator:
                            
                            string roomName = reader.ReadString();
                            
                            Room joinIntoRoom = null;
                            
                            foreach(Room r in Program.rooms)
                                if(r.name == roomName)
                                    joinIntoRoom = r;
                            if (joinIntoRoom != null)
                            {
                                Spectator s = new Spectator(this,joinIntoRoom);
                                joinIntoRoom.addSpectator(s);
                            
                                return;
                            }
                            else
                                sendError("no room by this name");

                            break;


                        case 0:
                            //nada
                            Console.WriteLine("0 received");//fih moshkela hena
                            break;


                        default:
                            throw new Exception("sth went horribly wrong XD");
                    }
                }
               
            }
            
        }

        private void sendError(string v)
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write((int)commOp.error);
            bw.Write(v);
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
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write((int)commOp.playerMoveReq);
            bw.Write(x);
            bw.Write(y);
            bw.Write(PlayerTokenColor);
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
