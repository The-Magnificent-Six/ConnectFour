
namespace game
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.GridDimentionsPanel = new System.Windows.Forms.Panel();
            this.Cols = new System.Windows.Forms.TextBox();
            this.Rows = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.GridDimentionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "player1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "color1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(110, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 58);
            this.button2.TabIndex = 15;
            this.button2.Text = "Open Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 29);
            this.button1.TabIndex = 16;
            this.button1.Text = "Choose Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GridDimentionsPanel
            // 
            this.GridDimentionsPanel.Controls.Add(this.button3);
            this.GridDimentionsPanel.Controls.Add(this.Rows);
            this.GridDimentionsPanel.Controls.Add(this.Cols);
            this.GridDimentionsPanel.Location = new System.Drawing.Point(366, 61);
            this.GridDimentionsPanel.Name = "GridDimentionsPanel";
            this.GridDimentionsPanel.Size = new System.Drawing.Size(250, 179);
            this.GridDimentionsPanel.TabIndex = 17;
            // 
            // Cols
            // 
            this.Cols.Location = new System.Drawing.Point(100, 21);
            this.Cols.Name = "Cols";
            this.Cols.Size = new System.Drawing.Size(125, 27);
            this.Cols.TabIndex = 0;
            // 
            // Rows
            // 
            this.Rows.Location = new System.Drawing.Point(100, 80);
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(125, 27);
            this.Rows.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(68, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "CalculateDim.";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 400);
            this.Controls.Add(this.GridDimentionsPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form2";
            this.Text = "Connect4";
            this.GridDimentionsPanel.ResumeLayout(false);
            this.GridDimentionsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel GridDimentionsPanel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox Rows;
        private System.Windows.Forms.TextBox Cols;
    }
}