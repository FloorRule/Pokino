using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pokino
{
    public partial class StarterForm : Form
    {
        private Label lb;

        private Button[] pokinos;

        private bool isPicked;

        private PictureBox pbE;

        private TextBox textBox1;

        void createBtn(Button btn, int x, int y, int sizeX, int sizeY, Color color, Color backColor, Color borderColor, string name)
        {
            btn.Size = new Size(sizeX, sizeY);
            btn.Location = new Point(x, y);
            btn.Text = name;
            btn.BackColor = backColor; //black
            btn.ForeColor = color; // white
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = borderColor;
            btn.FlatAppearance.BorderSize = 0;
            Controls.Add(btn);
            //btn.Click += new EventHandler(click);
        }

        public StarterForm()
        {
            //form
            isPicked = false;
            StartPosition = FormStartPosition.CenterScreen;
            //this.Location = new Point(990, 140);
            ClientSize = new Size(400, 400); //form size
            BackColor = Color.FromArgb(255, 255, 245);//form color
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            this.FormClosing += Form4_FormClosing;

            
            //StaticSaver st = new StaticSaver(player.Text);
            textBox1 = new TextBox();
            textBox1.Location = new Point(150, 300);
            textBox1.Width = 100;
            textBox1.Height = 50;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.BorderStyle = BorderStyle.Fixed3D;
            textBox1.MaxLength = 40;
            Controls.Add(textBox1);
            //textBox1.Text = "test";

            //label
            lb = new Label();
            lb.AutoSize = true;
            lb.Font = new Font("Ariel", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lb.Location = new Point(50, 0); //location label
            lb.Text = "Pick Your Starter Pokino!";
            lb.ForeColor = Color.Black;
            Controls.Add(lb);

            pbE = new PictureBox();
            pbE.Location = new Point(0, 0);
            pbE.Size = new Size(400, 400);
            pbE.Image = Image.FromFile("./start.png");
            pbE.Visible = true;
            

            pokinos = new Button[3];

            pokinos[0] = new Button();
            createBtn(pokinos[0], 150, 50, 100, 100, Color.Black, Color.IndianRed, Color.Black, "Bramber");
            pokinos[0].Click += new EventHandler(Fire_Click);


            pokinos[1] = new Button();
            createBtn(pokinos[1], 250, 200, 100, 100, Color.Black, Color.LightBlue, Color.Black, "Droqi");
            pokinos[1].Click += new EventHandler(Water_Click);

            pokinos[2] = new Button();
            createBtn(pokinos[2], 50, 200, 100, 100, Color.Black, Color.LightGreen, Color.Black, "Crango");
            pokinos[2].Click += new EventHandler(Grass_Click);

            Controls.Add(pbE);
            StaticSaver.f1.Hide();
        }
        private void Fire_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                MessageBox.Show("Enter UserName!");
            }else
            {
                
                isPicked = true;
                Monster strter = new Monster("Bramber", 0, 20, new int[5] { 10, 15, 20 , 7, 5}, 0);
                StaticSaver sc = new StaticSaver(textBox1.Text, strter);
                
                this.Close();
            }
           
           
        }

        private void Water_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Enter UserName!");
            }
            else
            {
               
                isPicked = true;
                Monster strter = new Monster("Droqi", 0, 20, new int[5] { 10, 20, 25, 5,7}, 1);
                StaticSaver sc = new StaticSaver(textBox1.Text, strter);
                
                this.Close();
            }

        }

        private void Grass_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Enter UserName!");
            }
            else
            {
               
                isPicked = true;
                Monster strter = new Monster("Crango", 0, 20, new int[5] { 20, 10, 5, 5, 5}, 2);
                StaticSaver sc = new StaticSaver(textBox1.Text, strter);
               
                this.Close();
            }

        }
        void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if(isPicked)
                {
                    StaticSaver._playerName = textBox1.Text;
                    StaticSaver.f1.Show();

                }else
                {

                    StaticSaver.invForm.Close();
                }
                
            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Autosave and clear up ressources
            }

        }
        
    }
}
