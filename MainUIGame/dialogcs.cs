﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUIGame
{
    public partial class dialog : Form
    {
        public dialog()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           User.getInstance().userColor= (tokencolor)comboBox1.SelectedIndex;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // request to join the game by 2secnd player
            int reqno = 3;
            User.getInstance().BW.Write(reqno);
            //send color to server 
            User.getInstance().BW.Write(comboBox1.SelectedIndex);
        }
    }
}