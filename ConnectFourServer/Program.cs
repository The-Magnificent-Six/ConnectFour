using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Program
    {
        List<Room> room = new List<Room>(); 
        static void Main(string[] args)
        {
            //have a tcpServer that sendes available rooms on connection
            //then wait for a flag response of either new room , join room , spectate 
            //followed by the op details
        }
    }
}
