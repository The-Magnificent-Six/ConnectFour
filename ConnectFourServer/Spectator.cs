using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Spectator:User
    {
        public Spectator(Socket s,NetworkStream net ,String name):base(s,net ,name)
        {

        }


        public void sendPlayerXWon(Player p)
        {
            
        }

    }
}
