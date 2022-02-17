using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;   
using System.Net.Sockets; 

namespace ConnectFourServer
{
    class Program
    {
        static TcpListener server;
        static public List<Room> rooms = new List<Room>(); 

        

        static void Main(string[] args)
        {
            //have a tcpServer that sendes available rooms on connection
            //then wait for a flag response of either new room , join room , spectate 
            //followed by the op details

            Byte[] bt = new byte[] {127,0,0,1 };
            IPAddress localHost = new IPAddress(bt);
            server = new TcpListener(localHost, 3000);
            server.Start(); 

            Task serverTask = Task.Factory.StartNew( () =>{ 

                while (true)
                {
                    Console.WriteLine("waiting for a connection");
                    Socket socketConnection = server.AcceptSocket();
                    Console.WriteLine("connecting ..");

                    Task.Factory.StartNew( (socketConnection_) =>{
                        
                        User nUser = new User((Socket)socketConnection_);

                    },socketConnection );
                    
                } 

            } );
            Task.WaitAll(serverTask);
            

        }
    }
}
