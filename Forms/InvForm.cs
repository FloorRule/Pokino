using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pokino
{
    public partial class InvForm : Form
    {
        public InvForm()
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(-100, -100);
            ClientSize = new Size(10, 10); //form size
            //BackColor = Color.Wheat;//form color
            GameEngine f = new GameEngine();
            StaticSaver.f1 = f;
            f.Show();

            StarterForm f2 = new StarterForm();
            StaticSaver.form4 = f2;
            f2.Show();

        }

        [STAThread]
        static void Main()
        {
            InvForm f = new InvForm();
            StaticSaver.invForm = f;
            Application.EnableVisualStyles();
            Application.Run(f);
            f.Hide();

        }
    }
}
