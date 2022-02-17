using System;
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
        public tokencolor Col ;
        public string op { get; set; }

        public dialog()
        {
            InitializeComponent();
            Player1Col();
            


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           User.getInstance().userColor= (tokencolor)comboBox1.SelectedIndex;
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            // request to join the game by 2secnd player
            string reqno = "3";
            User.getInstance().BW.Write(reqno);
            //send color to server 
            User.getInstance().userColor = (tokencolor)comboBox1.SelectedIndex;
            User.getInstance().BW.Write(comboBox1.SelectedIndex);
            op = User.getInstance().BR.ReadString();


        }

        private void Player1Col()
        { 
            for(int i=0; i<comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == Col.ToString())
                {
                    comboBox1.Items.RemoveAt(i);
                }
            }
        }
    }
}
