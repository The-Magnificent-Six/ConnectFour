using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    
    class Player : User 
    {
        Room room;
        int tokencolor;//must be different player ID
        public int TokenColor { get => TokenColor;}

        public Player(Socket s,NetworkStream net ,String name,int tokenColor,Room r):base(s,net ,name)
        {
            room = r;
            tokencolor = tokenColor;
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
