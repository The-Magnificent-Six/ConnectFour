using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    abstract class User
    {
        Socket socket;
        NetworkStream networkStream;
        public NetworkStream netStream { get => networkStream;}
        public Socket Socket { get => socket; }
        String UserName;
        public string name { get => UserName; }


        public User(Socket s)
        {
            socket = s;
            buildStream();
            networkInitialize();
        }
        public networkInitialize()
        {
            // the comm protocol
            
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

        public void sendRoomInitialDetails(Room r)
        {

        }
        public void sendRooms()
        {
            BinaryWriter bw = new BinaryWriter( netStream );
            bw.Write(Program.rooms.Count);
            foreach (Room r in Program.rooms)
            {
                sendRoomInitialDetails(r);
            }
        }

    }
}
