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
        String UserName;

        public User(Socket s,NetworkStream net ,String name)
        {
            socket = s;
            networkStream = net;
            UserName = name;
        }

        public void SendMoveToUser(int x,int y,int PlayerTokenColor)
        {

        }

        public void sendRoomInitialDetails(Room r)
        {

        }


    }
}
