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
        public tokencolor Col { get; set; }
        public string op { get; set; }

        public dialog()
        {
            InitializeComponent();
            //Player1Col();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

           String Color = comboBox1.SelectedItem.ToString();
            switch (Color)
            {
                case "Red":
                    User.getInstance().userColor = tokencolor.Red;
                    break;
                case "Blue":
                    User.getInstance().userColor = tokencolor.Blue;
                    break;
                case "Green":
                    User.getInstance().userColor = tokencolor.Green;
                    break;
                case "Violet":
                    User.getInstance().userColor = tokencolor.Violet;
                    break;

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.GetItemText(comboBox1.SelectedItem) == Col.ToString())
            {
                MessageBox.Show("Player 1 already chose this color");
            }
            else
            {

                // request to join the game by 2secnd player
                string reqno = "3";
                User.getInstance().BW.Write(reqno);
                //send color to ser
                User.getInstance().BW.Write(((int)User.getInstance().userColor).ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }   
         
        }

        private void Player1Col()
        {
      
            for (int i=0; i<comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == Col.ToString())
                {
                    comboBox1.Items.RemoveAt(i);
                }
            }
        }
    }
}
