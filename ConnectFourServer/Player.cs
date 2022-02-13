using System;
using System.Collections.Generic;
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

        }
        public void sendLossToPlayer()
        {

        }
        
        //thread to receive a move


    }
}
