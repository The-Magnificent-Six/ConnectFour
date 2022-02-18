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
                if (networkStream.CanRead)
                {
                    commOp op = (commOp)int.Parse( reader.ReadStringIgnoreNull());
                    switch (op)
                    {
                        case commOp.availRoomsReq:
                            sendRooms();
                            break;


                        case commOp.createRoomReq:
                            
                            int tokenColor_ = int.Parse( reader.ReadStringIgnoreNull());
                            int rows_= int.Parse( reader.ReadStringIgnoreNull());
                            int cols_= int.Parse( reader.ReadStringIgnoreNull());
                            string roomName_ = reader.ReadStringIgnoreNull();
                            string playerName_ = reader.ReadStringIgnoreNull();
                            Console.WriteLine(tokenColor_ + " " + roomName_ + " " + rows_ + " " + cols_ + " " + playerName_);
                            bool roomNameUnique = true;
                          
                            foreach (Room r in Program.rooms)
                                if (r.name == roomName_)
                                    roomNameUnique = false;


                            if (roomNameUnique)
                            {

                                BinaryWriter writer = new BinaryWriter(networkStream);
                                writer.Write(((int)commOp.accept).ToString());
                                Player p1 = new Player(this);
                                p1.setName(playerName_);
                                p1.setToken(tokenColor_);
                                Console.WriteLine(p1.name+" "+p1.TokenColor.ToString());

                                Program.rooms.Add(new Room(roomName_, rows_, cols_, p1));
                                Console.WriteLine("room name is " + Program.rooms[0].name);
                                return;
                            }
                            else
                                sendError("room name is not unique");
                            
                            
                            break;


                        case commOp.joinRoomAsPlayer:
                            int tokenColor2_ = int.Parse( reader.ReadStringIgnoreNull());
                            string player2Name_ = reader.ReadStringIgnoreNull();
                            string room2Name_ = reader.ReadStringIgnoreNull();

                            Room r_ = null;
                           
                            foreach (Room r in Program.rooms)
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
                            
                            string roomName = reader.ReadStringIgnoreNull();
                            
                            Room joinIntoRoom = null;
                           
                            foreach (Room r in Program.rooms)
                                if (r.name == roomName)
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
                            Console.WriteLine("0 received");//law 7asal moshkela hena control chars
                            break;


                        default:
                            throw new Exception("sth went horribly wrong XD");
                    }
                }
               
            }
            
        }

        public void sendError(string v)
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write(((int)commOp.error).ToString());
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
            bw.Write(((int)commOp.playerMoveReq).ToString());
            bw.Write(x.ToString());
            bw.Write(y.ToString());
            bw.Write(PlayerTokenColor.ToString());
        }

        public void sendRoomDetails(Room r)
        {
            BinaryWriter bw = new BinaryWriter(netStream);
            bw.Write(r.name);
            if (r.isPlayersIncomplete())
            {
                bw.Write("1");//num of players in room
                bw.Write(r.Players[0].TokenColor.ToString());
            }
            else
            {
                bw.Write("2"); // num of players in room
            }
            bw.Write(r.Spectators.Count.ToString());

        }
        public void sendRooms()
        {
            BinaryWriter bw = new BinaryWriter( netStream );
            
            bw.Write( ((int)commOp.roomsResp).ToString() );
            lock (Program.rooms)
            {
                bw.Write(Program.rooms.Count.ToString());

                foreach (Room r in Program.rooms)
                {
                    sendRoomDetails(r);
                }
            }
        }

    }
}
