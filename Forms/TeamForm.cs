using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pokino
{
    public partial class TeamForm : Form
    {
        //form2
        private Label lb;

        private Label[] poke;

        private PictureBox poksc;

        public TeamForm()
        {
            //form
            
            StartPosition = FormStartPosition.Manual;
            this.Location = new Point(990, 140);
            ClientSize = new Size(250, 400); //form size
            BackColor = Color.FromArgb(0, 0, 212);//form color
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Load += Form2_Load2;
            this.FormClosing += Form2_FormClosing;


            //StaticSaver st = new StaticSaver(player.Text);

            poksc = new PictureBox();
            poksc.Location = new Point(0, 0);
            poksc.Size = new Size(250, 400);
            poksc.Image = Image.FromFile("./pokscreen.png");
            poksc.Visible = true;
            

            //label
            lb = new Label();
            lb.AutoSize = true;
            lb.Font = new Font("Ariel", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lb.Location = new Point(0, 5); //location label
            lb.Text = "\nYour Very Own Pokino Team";
            lb.ForeColor = Color.Black;
            Controls.Add(lb);

            poke = new Label[5];
            for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
            {
                poke[i] = new Label();
                poke[i].AutoSize = true;
                poke[i].Font = new Font("Ariel", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                poke[i].Location = new Point(0, 75*(i+1)); //location label 
                if(StaticSaver._pokinoList[i] != null)
                {
                    poke[i].Text = "     " + StaticSaver._pokinoList[i].getName() + " (" + StaticSaver._pokinoList[i].getHp() + "/" + StaticSaver._pokinoList[i].getMaxHp() + ")";
                }else
                {
                    poke[i].Text = "     ";
                }
                
                poke[i].ForeColor = Color.Black;
                Controls.Add(poke[i]);
                
            }
            Controls.Add(poksc);
            //TransparetBackground(lb);
        }

        void TransparetBackground(Control C)
        {
            C.Visible = false;

            C.Refresh();
            Application.DoEvents();


            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            int Right = screenRectangle.Left - this.Left;

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            Bitmap bmpImage = new Bitmap(bmp);
            bmp = bmpImage.Clone(new Rectangle(C.Location.X + Right, C.Location.Y + titleHeight, C.Width, C.Height), bmpImage.PixelFormat);
            C.BackgroundImage = bmp;

            C.Visible = true;
        }
        private void Form2_Load2(object sender, EventArgs e)
        {
            TransparetBackground(lb);
            for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
            {
                TransparetBackground(poke[i]);
            }
        }

        void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prompt user to save his data
                StaticSaver._isCreated = false;
            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Autosave and clear up ressources
            }

        }


    }
}

