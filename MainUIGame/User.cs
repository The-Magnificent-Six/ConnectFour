using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Drawing;

namespace MainUIGame
{
    public class User
    {
        public NetworkStream ns;
        public string username;
        public tokencolor userColor;
        public TcpClient client;
        public BinaryReader BR;
        public BinaryWriter BW;
        public byte[] bt = new byte[] { 172,16 ,13 , 113 };
        private IPAddress serverIP;
        static User instance = null;
        
        private User()
        {
            restartConnection();
        }
        public static User getInstance()
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
        public void restartConnection()
        {
            serverIP = new IPAddress(bt);
            client = new TcpClient();
            client.Connect(serverIP, 3000);
            ns = client.GetStream();
            BR = new BinaryReader(ns);
            BW = new BinaryWriter(ns);
        }




    };

        }
       
    
  
    
