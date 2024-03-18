using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokino
{
    public partial class GameEngine : Form
    {

        private List<Button> GrassBtn = new List<Button>();
        private List<Button> WallBtn = new List<Button>();

        private Button HealBtn;

        private Button player;
        private int x;
        private int y;

        private Label lb;
        private Label lb2;

        
        private Button ancor;

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

        public void update()
        {
            Controls.Remove(HealBtn);
            for (int i = 0; i < GrassBtn.Count; i++)
            {
                Controls.Remove(GrassBtn[i]);
            }
            

            Controls.Add(player);

            Controls.Add(HealBtn);
            for (int i = 0; i < GrassBtn.Count; i++)
            {
                Controls.Add(GrassBtn[i]);
            }
            
        }

        public GameEngine()
        {
            StaticSaver._isCreated = false;
            StaticSaver._isInFight = false;
           
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(600, 400); //form size
            BackColor = Color.LightGreen;//form color
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.FormClosing += Form1_FormClosing;

            
            player = new Button();
            createBtn(player, 250, 1450, 50, 50, Color.White, Color.Black, Color.Black, StaticSaver._playerName);
            player.Click += new EventHandler(player_Click);

            //MessageBox.Show(StaticSaver._playerName);

            lb = new Label();
            lb.AutoSize = true;
            lb.Font = new Font("Ariel", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lb.Location = new Point(65, 15); //location label
            lb.Text = "(" + player.Location.X.ToString() + "," + player.Location.Y.ToString() + ")";
            lb.ForeColor = Color.Black;

            //Controls.Add(lb);
            Controls.Add(player);

            ancor = new Button();
            createBtn(ancor, 0, 0, 50, 50, Color.Black, Color.Pink, Color.Pink, "A");
            ancor.Enabled = false;
            Controls.Remove(ancor);

            


            
            /*
            Button GrassBtn2;
            Random rnd = new Random();
            int n = 50;
            int n2 = 50;
            int index = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        GrassBtn2 = new Button();
                        createBtn(GrassBtn2, n2, n, 50, 50, Color.Green, Color.Green, Color.Green, "");
                        GrassBtn2.Enabled = false;
                        GrassBtn.Add(GrassBtn2);
                        Controls.Add(GrassBtn[index]);
                        index++;
                        n2 += 50;
                    }
                    n += 50;
                    n2 = 0;
                }
                
            }
            */


            //Monster strter = new Monster("Bramber", 0, 20);
            //StaticSaver st = new StaticSaver(player.Text, strter);

            //label


            lb2 = new Label();
            lb2.AutoSize = true;
            lb2.Font = new Font("Ariel", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lb2.Location = new Point(125, 50); //location label
            
            lb2.ForeColor = Color.Black;
            //Controls.Add(lb2);

            createMaze();
            sizeMap = 50;
            for (int i = 0; i < 26; i++)
            {
                updateMap(false, false);
                if(i < 25)
                {
                   // move(50, false, false);
                }
            }
            
            sizeMap = 10;

        }

        void createMaze()
        {
            StreamReader reader = new StreamReader("./maze.txt");
            string mazeString = reader.ReadToEnd();
            string[] mazeLayers = mazeString.Split('*');

            Button WallBtntemp;
            Button GrassBtn2;
            int index = 0;
            int index2 = 0;
            int n = 0, n2 = 0;
            int jump = 50;
            int tempX = 0;
            int tempY = 0;
            for (int i = 0; i < mazeLayers.Length; i++)
            {
                n2 = 0;
                for (int j = 0; j < mazeLayers[i].Length; j++)
                {
                    switch (mazeLayers[i][j])
                    {
                        case '3':
                            HealBtn = new Button();
                            createBtn(HealBtn, 275, 250, 50, 50, Color.Red, Color.Pink, Color.Pink, "Heal");
                            HealBtn.Enabled = false;
                            Controls.Add(HealBtn);
                            break;
                        case '2':
                            tempX = n2;
                            tempY = n;
                            for (int k = 0; k < 4; k++)
                            {
                                tempX = n2;
                                for (int l = 0; l < 4; l++)
                                {
                                    GrassBtn2 = new Button();
                                    createBtn(GrassBtn2, tempX, tempY, 50, 50, Color.Green, Color.Green, Color.Green, "");
                                    GrassBtn2.Enabled = false;
                                    GrassBtn.Add(GrassBtn2);
                                    Controls.Add(GrassBtn[index2]);
                                    index2++;
                                    tempX += 50;
                                }
                                tempY += 50;
                            }
                            
                            break;
                        case '6':
                            WallBtntemp = new Button();
                            createBtn(WallBtntemp, n2, n, 200, 200, Color.Black, Color.Black, Color.Black, "W");
                            WallBtntemp.Enabled = false;
                            WallBtn.Add(WallBtntemp);
                            Controls.Add(WallBtn[index]);
                            index++;
                            break;
                        default:
                            break;
                    }
                    n2 += 200;
                }
                n += 200;
            }

        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

                StaticSaver.invForm.Close();
            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                StaticSaver.invForm.Close();
            }

        }

        public void checkPokino()
        {
            int n = 0;
            bool isAlive = false;
            
            if (checkForGrassNearby())
            {
                 Random rnd = new Random();
                 n = rnd.Next(0, 100);
                int temp = 0;
                for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
                {
                    if(StaticSaver._pokinoList[i] != null)
                    {
                        if (temp < StaticSaver._pokinoList[i].getLvl())
                        {
                            temp = StaticSaver._pokinoList[i].getLvl();
                        }
                    }
                   
                }
                for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
                {
                    if (StaticSaver._pokinoList[i] != null)
                    {
                        if (StaticSaver._pokinoList[i].getHp() > 0)
                        {
                            isAlive = true;
                        }
                    }

                }
                

                if (n >= 25 && n <= 55 && isAlive)
                 {
                     //lb2.Text = "Pokino found " + n.ToString();
                     n = rnd.Next(0, 101);
                     if(n == 100 || n == 99)
                     {
                        Monster enmy = new Monster("DromioS", 999, 2, new int[5] { 10, 10, 15, 10, 5 }, 2, temp);
                        StaticSaver._pokinoEName = enmy;
                    }
                    else if (n >= 50 && n < 99)
                     {
                        //Move[] moveset = new Move[4] { new Move( 1, "sr", 1 ), new Move(1, "sr", 1), new Move(1, "sr", 1), new Move(1, "sr", 1) };
                         Monster enmy = new Monster("Dromio", 999, 2, new int[5]{10, 10, 10, 5, 5}, 2, temp);
                         StaticSaver._pokinoEName = enmy;
                     }else
                     {
                        Monster enmy = new Monster("Dromio", 999, 2, new int[5] { 10, 10, 10 , 5, 5}, 2, temp);
                        StaticSaver._pokinoEName = enmy;
                    }
                     if(!StaticSaver._isInFight)
                     {
                        StaticSaver._isInFight = true;
                        
                        BattleForm f3 = new BattleForm();
                        f3.Show();
                        

                     }
                     
                 }
                 else
                 {
                     //lb2.Text = "Pokino not found " + n.ToString();

                 }

            }
               
            
            
            if(HealBtn.Location.X == player.Location.X && HealBtn.Location.Y == player.Location.Y)
            {
                lb2.Text = "Pokino Healed! " + n.ToString();
                for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
                {
                    if(StaticSaver._pokinoList[i] != null)
                    {
                        StaticSaver._pokinoList[i].healMaxHp();
                    }
                }
            }
            else
            {
                lb2.Text = "Pokino not found " + n.ToString();
                
            }
        }

        private void moveAll(int movment, bool isX, bool isUp)
        {
            int x = 0;
            int y = 0;
            if (!StaticSaver._isInFight && !StaticSaver._isCreated)
            {
                
                if (isX)
                {

                    if (isUp)
                    {
                        x = player.Location.X;
                        

                        x += movment;
                        player.Location = new Point(x, player.Location.Y);
                        for (int i = 0; i < GrassBtn.Count; i++)
                        {

                            x = GrassBtn[i].Location.X;
                            x += movment;
                            GrassBtn[i].Location = new Point(x, GrassBtn[i].Location.Y);
                        }
                        for (int i = 0; i < WallBtn.Count; i++)
                        {

                            x = WallBtn[i].Location.X;
                            x += movment;
                            WallBtn[i].Location = new Point(x, WallBtn[i].Location.Y);
                        }
                        x = HealBtn.Location.X;
                        x += movment;
                        HealBtn.Location = new Point(x, HealBtn.Location.Y);
                    }
                    else
                    {
                        x = player.Location.X;
                        

                        x -= movment;
                        player.Location = new Point(x, player.Location.Y);
                        for (int i = 0; i < GrassBtn.Count; i++)
                        {

                            x = GrassBtn[i].Location.X;
                            x -= movment;
                            GrassBtn[i].Location = new Point(x, GrassBtn[i].Location.Y);
                        }
                        for (int i = 0; i < WallBtn.Count; i++)
                        {

                            x = WallBtn[i].Location.X;
                            x -= movment;
                            WallBtn[i].Location = new Point(x, WallBtn[i].Location.Y);
                        }
                        x = HealBtn.Location.X;
                        x -= movment;
                        HealBtn.Location = new Point(x, HealBtn.Location.Y);
                    }


                   
                }
                else
                {

                    if (!isUp)
                    {
                       
                        y = player.Location.Y;

                        y += movment;
                        player.Location = new Point(player.Location.X, y);

                        for (int i = 0; i < GrassBtn.Count; i++)
                        {

                            y = GrassBtn[i].Location.Y;
                            y += movment;
                            GrassBtn[i].Location = new Point(GrassBtn[i].Location.X, y);
                        }
                        for (int i = 0; i < WallBtn.Count; i++)
                        {

                            y = WallBtn[i].Location.Y;
                            y += movment;
                            WallBtn[i].Location = new Point(WallBtn[i].Location.X, y);
                        }
                        y = HealBtn.Location.Y;
                        y += movment;
                        HealBtn.Location = new Point(HealBtn.Location.X, y);
                    }
                    else
                    {
                       
                        y = player.Location.Y;

                        y -= movment;
                        player.Location = new Point(player.Location.X, y);

                        for (int i = 0; i < GrassBtn.Count; i++)
                        {
                           
                            y = GrassBtn[i].Location.Y;
                            y -= movment;
                            GrassBtn[i].Location = new Point(GrassBtn[i].Location.X, y);
                        }
                        for (int i = 0; i < WallBtn.Count; i++)
                        {

                            y = WallBtn[i].Location.Y;
                            y -= movment;
                            WallBtn[i].Location = new Point(WallBtn[i].Location.X, y);
                        }
                        y = HealBtn.Location.Y;
                        y -= movment;
                        HealBtn.Location = new Point(HealBtn.Location.X, y);

                    }


                   
                }
            }


        }

        private bool tryMove(int movment, bool isX, bool isUp)
        {
            if (!StaticSaver._isInFight && !StaticSaver._isCreated)
            {
                int x = player.Location.X;
                int y = player.Location.Y;
                if (isX)
                {

                    if (isUp)
                    {
                        x += movment;
                        for (int i = 0; i < WallBtn.Count; i++)
                        {
                            for (int x2 = WallBtn[i].Location.X -25; x2 < WallBtn[i].Location.X + WallBtn[i].Width +25; x2++)
                            {
                                for (int y2 = WallBtn[i].Location.Y - 50; y2 < WallBtn[i].Location.Y + WallBtn[i].Height +25; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        x -= movment;
                        for (int i = 0; i < WallBtn.Count; i++)
                        {
                            for (int x2 = WallBtn[i].Location.X; x2 < WallBtn[i].Location.X + WallBtn[i].Width; x2++)
                            {
                                for (int y2 = WallBtn[i].Location.Y; y2 < WallBtn[i].Location.Y + WallBtn[i].Height; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        
                    }


                    //checkPokino();
                }
                else
                {

                    if (!isUp)
                    {
                        y += movment;
                        for (int i = 0; i < WallBtn.Count; i++)
                        {
                            for (int x2 = WallBtn[i].Location.X - 25; x2 < WallBtn[i].Location.X + WallBtn[i].Width - 25; x2++)
                            {
                                for (int y2 = WallBtn[i].Location.Y-25 ; y2 < WallBtn[i].Location.Y + WallBtn[i].Height-25; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        y -= movment;
                        for (int i = 0; i < WallBtn.Count; i++)
                        {
                            for (int x2 = WallBtn[i].Location.X; x2 < WallBtn[i].Location.X + WallBtn[i].Width; x2++)
                            {
                                for (int y2 = WallBtn[i].Location.Y; y2 < WallBtn[i].Location.Y + WallBtn[i].Height; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        
                    }

                    
                    //checkPokino();
                }
                return true;
            }else
            {
                return false;
            }


        }

        int sizeMap = 10;
        private void updateMap(bool isX, bool isUp)
        {
            int x = 0;
            int y = 0;
            if (isX)
            {
                if (isUp)
                {
                    for (int i = 0; i < WallBtn.Count; i++)
                    {
                        x = WallBtn[i].Location.X + sizeMap;
                        y = WallBtn[i].Location.Y;

                        WallBtn[i].Location = new Point(x, y);

                    }
                    for (int i = 0; i < GrassBtn.Count; i++)
                    {
                        x = GrassBtn[i].Location.X + sizeMap;
                        y = GrassBtn[i].Location.Y;

                        GrassBtn[i].Location = new Point(x, y);

                    }

                    
                    x = HealBtn.Location.X + sizeMap;
                    y = HealBtn.Location.Y;
                    HealBtn.Location = new Point(x, y);


                    x = player.Location.X + sizeMap;
                    y = player.Location.Y;
                    player.Location = new Point(x, y);

                }
                else
                {
                    for (int i = 0; i < WallBtn.Count; i++)
                    {
                        x = WallBtn[i].Location.X - sizeMap;
                        y = WallBtn[i].Location.Y;

                        WallBtn[i].Location = new Point(x, y);

                    }

                    for (int i = 0; i < GrassBtn.Count; i++)
                    {
                        x = GrassBtn[i].Location.X - sizeMap;
                        y = GrassBtn[i].Location.Y;

                        GrassBtn[i].Location = new Point(x, y);

                    }

                    x = HealBtn.Location.X - sizeMap;
                    y = HealBtn.Location.Y;
                    HealBtn.Location = new Point(x, y);


                    x = player.Location.X - sizeMap;
                    y = player.Location.Y;
                    player.Location = new Point(x, y);
                }
            }
            else
            {
                if (isUp)
                {

                    for (int i = 0; i < WallBtn.Count; i++)
                    {
                        x = WallBtn[i].Location.X;
                        y = WallBtn[i].Location.Y + sizeMap;

                        WallBtn[i].Location = new Point(x, y);

                    }

                    for (int i = 0; i < GrassBtn.Count; i++)
                    {
                        x = GrassBtn[i].Location.X;
                        y = GrassBtn[i].Location.Y + sizeMap;

                        GrassBtn[i].Location = new Point(x, y);

                    }

                    x = HealBtn.Location.X;
                    y = HealBtn.Location.Y + sizeMap;
                    HealBtn.Location = new Point(x, y);


                    x = player.Location.X;
                    y = player.Location.Y + sizeMap;
                    player.Location = new Point(x, y);

                }
                else
                {
                    for (int i = 0; i < WallBtn.Count; i++)
                    {
                        x = WallBtn[i].Location.X;
                        y = WallBtn[i].Location.Y - sizeMap;

                        WallBtn[i].Location = new Point(x, y);

                    }

                    for (int i = 0; i < GrassBtn.Count; i++)
                    {
                        x = GrassBtn[i].Location.X;
                        y = GrassBtn[i].Location.Y - sizeMap;

                        GrassBtn[i].Location = new Point(x, y);

                    }

                    x = HealBtn.Location.X;
                    y = HealBtn.Location.Y - sizeMap;
                    HealBtn.Location = new Point(x, y);

                    x = player.Location.X;
                    y = player.Location.Y - sizeMap;
                    player.Location = new Point(x, y);

                }
            }
        }

        private bool is_at_edge(bool isX, bool isUp)
        {
            bool value = false;
            if (isX)
            {
                if (isUp)
                {
                    if (player.Location.X == 25)
                    {
                        //StaticSave.man.Location = new Point(625, StaticSave.man.Location.Y);
                        for (int i = 0; i < 15; i++)
                        {
                            updateMap(true, true);
                        }
                        value = true;
                    }

                }
                else
                {

                    if (player.Location.X == 525)
                    {

                        for (int i = 0; i < 15; i++)
                        {
                            updateMap(true, false);
                        }
                        value = true;
                    }
                }
            }
            else
            {
                if (isUp)
                {


                    if (player.Location.Y == 25)
                    {

                        for (int i = 0; i < 15; i++)
                        {
                            updateMap(false, true);
                        }
                        value = true;
                    }
                }
                else
                {


                    if (player.Location.Y == 325)
                    {

                        for (int i = 0; i < 15; i++)
                        {
                            updateMap(false, false);
                        }
                        value = true;
                    }
                }
            }

            return value;
        }

        private void move(int movment, bool isX, bool isUp)
        {
            if(!StaticSaver._isInFight && !StaticSaver._isCreated)
            {
                int x = player.Location.X;
                int y = player.Location.Y;
                if (isX)
                {
                   
                    if (isUp)
                    {
                        x += movment;
                        player.Location = new Point(x, player.Location.Y);
                    }
                    else
                    {
                        x -= movment;
                        player.Location = new Point(x, player.Location.Y);
                    }
                   
                    
                    checkPokino();
                }
                else
                {
                    
                    if (!isUp)
                    {
                        y += movment;
                        player.Location = new Point(player.Location.X, y);
                    }
                    else
                    {
                        y -= movment;
                        player.Location = new Point(player.Location.X, y);
                    }

                    
                    checkPokino();
                }
            }
                
            
        }
        DateTime timeStamp;
        int cam = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
              
            if ((DateTime.Now - timeStamp).Ticks < 1000000) return;
            timeStamp = DateTime.Now;

            player.Text = StaticSaver._playerName;

            switch (e.KeyCode)
            {
                case Keys.W:
                    if(cam == 1)
                    {
                        moveAll(50, false, false);
                    }
                    else
                    {
                        if(tryMove(25, false, true))
                        {
                            if(!is_at_edge(false,true))
                            {
                                move(25, false, true);
                            }
                            
                            
                        }
                       
                    }
                    
                    
                    break;
                case Keys.A:

                    
                    if (cam == 1)
                    {
                        moveAll(50, true, true);
                    }
                    else
                    {
                        if (tryMove(25, true, false))
                        {
                            if (!is_at_edge(true, true))
                            {
                                move(25, true, false);
                            }
                        }
                       
                    }
                    break;
                case Keys.D:

                    
                    if (cam == 1)
                    {
                        moveAll(50, true, false);
                    }
                    else
                    {
                        if (tryMove(25, true, true))
                        {
                            if (!is_at_edge(true, false))
                            {
                                move(25, true, true);
                            }
                        }
                        
                    }
                    break;
                case Keys.S:

                    
                    if (cam == 1)
                    {
                        moveAll(50, false, true);
                    }
                    else
                    {
                        if (tryMove(25, false, false))
                        {
                            if (!is_at_edge(false, false))
                            {
                                move(25, false, false);
                            }
                        }
                       
                    }
                    break;
                case Keys.M:
                   
                    if(cam == 0)
                    {
                        cam = 1;
                    }else
                    {
                        cam = 0;
                    }
                   
                    break;

                default:
                    break;
            }
        }

        private void player_Click(object sender, EventArgs e)
        {
            if(!StaticSaver._isCreated)
            {
                StaticSaver._isCreated = true;
                TeamForm f2 = new TeamForm();
                f2.Show();
            }
            
            
        }

        public bool checkForGrassNearby()
        {
            for (int i = 0; i < GrassBtn.Count; i++)
            {
                for (int y = GrassBtn[i].Location.Y-25; y < GrassBtn[i].Location.Y+25; y++)
                {
                    for (int x = GrassBtn[i].Location.X-25; x < GrassBtn[i].Location.X+25; x++)
                    {
                        if (x == player.Location.X && y == player.Location.Y)
                        {
                            return true;
                        }
                        
                    }
                }
                
            }
            return false;
        }

       
    }
}
