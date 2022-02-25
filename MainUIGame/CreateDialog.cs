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
    public partial class CreateDialog : Form
    {
        public string op { get; set; }
        public string RomeName { get; set; }
        public string RowNo { get; set; }
        public string ColNo { get; set; }
        public tokencolor TokenCol { get; set; }
        public CreateDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            RomeName = textBox1.Text;
            RowNo = textBox2.Text;
            ColNo = textBox3.Text;

            String Col = comboBox1.SelectedItem.ToString();
            switch (Col)
            {
                case "Red":
                    TokenCol = tokencolor.Red;
                    break;
                case "Blue":
                    TokenCol = tokencolor.Blue;
                    break;
                case "Green":
                    TokenCol = tokencolor.Green;
                    break;
                case "Violet":
                    TokenCol = tokencolor.Violet;
                    break;

            }

            User.getInstance().userColor =TokenCol; 
            User.getInstance().BW.Write("2");
            User.getInstance().BW.Write(((int)TokenCol).ToString());
            User.getInstance().BW.Write(RowNo);
            User.getInstance().BW.Write(ColNo);
            User.getInstance().BW.Write(RomeName);
            User.getInstance().BW.Write(User.getInstance().username);
            op = User.getInstance().BR.ReadStringIgnoreNull();
            this.Close();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
