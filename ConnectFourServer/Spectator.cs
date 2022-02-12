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

        public Spectator(Socket s):base(s){}
        public Spectator(User u):base(u.Socket)
        {
            this.setName(u.name);
            this.networkStream = u.netStream;
        }

        public void sendPlayerXWon(Player p)
        {
            
        }

    }
}
