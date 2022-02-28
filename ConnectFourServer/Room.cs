﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Room
    {
        public Board board;
        String roomName;
        public String name { get => roomName; }
        Player[] players;
        public Player[] Players { get => players; }
        List<Spectator> spectators = new List<Spectator>() ;
        public List<Spectator> Spectators { get => spectators; }
        public Room(string rName, int boardRows , int boardCols , Player p)
        {
            roomName = rName;
            board = new Board(boardRows,boardCols);
            players = new Player[2];
            players[0] = p;
            players[0].room = this;
            players[1] = null;
        }

        public bool makeAMove(int x , int y,Player p)
        {
            bool GameOver = board.play(x,y,p.TokenColor);
            broadcastMove(x,y,p);
            if (GameOver)
            {
                if (board.checkDrawCondition)
                {
                    broadcastDraw();
                    saveToFile(null);
                }
                else
                {
                    broadcastWin(p);
                    saveToFile(p.name);
                }
            }
            return GameOver;
        }

        private void broadcastDraw()
        {
            foreach (Player player in players)
                player.sendDraw();
            foreach (Spectator spectator in spectators)
                spectator.sendDraw();
        }

        void broadcastWin(Player p)
        {
                p.sendWinToPlayer();
                foreach (Player player in players)
                    if (player != p)
                        player.sendLossToPlayer();
                foreach (Spectator spectator in spectators)
                        spectator.sendPlayerXWon(p);
        }
        void broadcastMove(int x,int y,Player p )
        {
            foreach (Player player in players)
            {
                player.SendMoveToUser(x,y,p.TokenColor);
            }
            foreach (Spectator spectator in spectators)
            {
                spectator.SendMoveToUser(x,y,p.TokenColor);
            }
        }
        public bool isPlayersIncomplete()
        {
            return (players[1] == null);
        }

        public void addPlayer(Player p)
        {
            players[1] = p;
            // if(isPlayersIncomplete())
            // {
                // players[1] = p;
                // players[1].room = this;
                //p.sendRoomDetails(this);
            // }
            // else
            // {

            // }
        }
        public void addSpectator(Spectator s)
        {
            spectators.Add(s);
            //s.sendRoomDetails(this);
        }
        private void saveToFile(string winnerName)
        {
            List<string> buffer_ = new List<string>();
            buffer_.Add("room : " + this.name);

            buffer_.Add("\n");

            if (winnerName != null)
                buffer_.Add("winner : " + winnerName);
            else
                buffer_.Add("Draw");

            buffer_.Add("\n");

            buffer_.Add("number of rows : " + this.board.rows);
            buffer_.Add("number of columns : " + this.board.columns);

            buffer_.Add("\n");

            for (int i = 0; i < 2; i++)
                buffer_.Add($"player {i+1} name : {this.players[i].name} : color : {this.players[i].TokenColor} ");

            buffer_.Add("\n");

            buffer_.Add("Boad : ");

            buffer_.Add("\n");

            for (int row_ = 0; row_ < this.board.rows; row_++)
            {
                string rowString = "";
                for (int col_ = 0; col_ < this.board.columns ; col_++)
                    rowString += this.board.matrix[row_, col_].ToString() + " ";
                
                buffer_.Add(rowString);
            }


            File.WriteAllLines($"{this.players[0].name}_vs_{this.players[1].name}.txt", buffer_);
        }

    }
}
