using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Pokino
{
    public partial class BattleForm : Form
    {
        private Label MessagLb;

        private PictureBox pbE;
        private Label EnemyLb;
        private Label EnemyHP;

        private PictureBox pbP;
        private Label PlayerLb;
        private Label PlayerHP;
        private Label PlayerExp;

        private Button fightBtn;
        private Button runBtn;
        private Button catchBtn;
        private Button pointBtn;

        private Button move1;
        private Button move2;
        private Button move3;

        private int turn;

        private int count;

        private int index;

        private bool isCatch;

        private bool isDead = false;

        public int movecount = -1;

        void createLabel(Label lb, int x, int y, string text, Color color)
        {
            //label
            lb.AutoSize = true;
            lb.Font = new Font("Ariel", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lb.Location = new Point(x, y); //location label
            lb.Text = text;
            lb.ForeColor = color;
            Controls.Add(lb);
        }

        void createBtn(Button btn, int x, int y, int sizeX, int sizeY, Color color, Color backColor, string name)
        {
            btn.Size = new Size(sizeX, sizeY);
            btn.Location = new Point(x, y);
            btn.Text = name;
            btn.BackColor = backColor; //black
            btn.ForeColor = color; // white
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Black;
            btn.FlatAppearance.BorderSize = 0;
            Controls.Add(btn);
            //btn.Click += new EventHandler(click);
        }

        public BattleForm()
        {
            //form
            turn = 0;
            index = 0;
            isCatch = false;

            count = -1;
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightCyan;
            this.Location = new Point(990, 140);
            ClientSize = new Size(600, 400); //form size
            //BackColor = Color.FromArgb(255, 255, 255);//form color
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            ControlBox = false;
            this.KeyPreview = true;
            this.KeyDown += Form2_KeyDown;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.FormClosing += Form3_FormClosing;

            //StaticSaver st = new StaticSaver(player.Text);
            MessagLb = new Label();
            createLabel(MessagLb, 160, 160, "A Wild " + StaticSaver._pokinoEName.getName() + " appeard!", Color.Black);


            pbE = new PictureBox();
            pbE.Location = new Point(440, 0);
            pbE.Size = new Size(150, 150);
            pbE.Image = Image.FromFile("./"+ StaticSaver._pokinoEName.getName() + ".png");
            pbE.Visible = true;
            Controls.Add(pbE);

            EnemyLb = new Label();
            createLabel(EnemyLb, 300, 0, StaticSaver._pokinoEName.getName() + " ["+ StaticSaver._pokinoEName.getLvl() + "]:", Color.Black);
            EnemyHP = new Label();
            createLabel(EnemyHP, 300, 30, "HP: " + StaticSaver._pokinoEName.getHp(), Color.Green);

            for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
            {
                if(StaticSaver._pokinoList[i].getHp() > 0)
                {
                    index = i;
                    i = StaticSaver._pokinoList.Length;
                }
            }

            pbP = new PictureBox();
            pbP.Location = new Point(0, 220);
            pbP.Size = new Size(150, 200);
            pbP.Image = Image.FromFile("./"+StaticSaver._pokinoList[index].getName()+".png");
            pbP.Visible = true;
            Controls.Add(pbP);

            PlayerLb = new Label();
            createLabel(PlayerLb, 160, 250, StaticSaver._pokinoList[index].getName()+" ["+ StaticSaver._pokinoList[index].getLvl() + "]:", Color.Black);
            PlayerHP = new Label();
            createLabel(PlayerHP, 160, 290, "HP: "+StaticSaver._pokinoList[index].getHp() + "/" + StaticSaver._pokinoList[index].getMaxHp(), Color.Green);
            PlayerExp = new Label();
            createLabel(PlayerExp, 160, 330, "EXP: " + StaticSaver._pokinoList[index].getExp() + "/"+ StaticSaver._pokinoList[index].getMaxEXP(), Color.DeepSkyBlue);

            pointBtn = new Button();
            createBtn(pointBtn, 450, 360, 50, 25, Color.White, Color.Black, ">>>");
            //pointBtn.Enabled = false;

            fightBtn = new Button();
            createBtn(fightBtn, 500, 250, 100, 50, Color.White, Color.Black, "FIGHT!");
            
            runBtn = new Button();
            createBtn(runBtn, 500, 300, 100, 50, Color.Black, Color.Gray, "Run");
           
            catchBtn = new Button();//500 350
            createBtn(catchBtn, 500 ,350, 100, 50, Color.Black, Color.LightSteelBlue, "Catch!");


            move1 = new Button();
            createBtn(move1, 500, 250, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[0].getName());

            move2 = new Button();
            createBtn(move2, 500, 300, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[1].getName());

            move3 = new Button();
            createBtn(move3, 500, 350, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[2].getName());
            Controls.Remove(move1);
            Controls.Remove(move2);
            Controls.Remove(move3);
        }

        int movers = 50;
        bool isFreeAttack = false;
        private void Next_Click()
        {
            isDead = false;
            if(turn == 2)
            {
                movers = 0;
                count += 1;
                if(isFreeAttack)
                {
                    switch (count)
                    {
                        case 2:
                            if (StaticSaver._pokinoEName.getHp() <= 0)
                            {
                                Random rnd = new Random();

                                StaticSaver._pokinoList[index].addEXP(rnd.Next(10, StaticSaver._pokinoEName.getLvl() + 10));
                                this.Close();
                            }
                            break;
                        case 3:
                            MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " used "+ StaticSaver._pokinoEName.getStrongMoveName() + "!\n(Press K..)";
                            break;
                        case 4:
                            if(StaticSaver._pokinoEName.getStrongMoveHit())
                            {
                                StaticSaver._pokinoList[index].takeDmg(StaticSaver._pokinoEName.getStrongMove(), StaticSaver._pokinoEName.getStrongMoveType());
                                PlayerHP.Text = "HP: " + StaticSaver._pokinoList[index].getHp() + "/" + StaticSaver._pokinoList[index].getMaxHp();
                                if (StaticSaver._pokinoList[index].getHp() <= 0)
                                {
                                    MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " is Dead!\n(Press K.)";
                                }
                            }else
                            {
                                MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " Missed!\n(Press K.)";
                            }
                            
                            break;
                        case 5:
                            if (StaticSaver._pokinoList[index].getHp() <= 0)
                            {
                                //StaticSaver._pokinoList[0].addEXP(10);
                                index += 1;
                                if (StaticSaver._pokinoList[index] == null)
                                {
                                    isDead = true;
                                    this.Close();
                                }
                                update();
                                //count = -1;
                                count = 7;
                                turn = 0;
                            }
                            break;
                        case 6:
                            MessagLb.Text = "What will " + StaticSaver._pokinoList[index].getName() + " do?";
                            Controls.Add(fightBtn);
                            Controls.Add(runBtn);
                            Controls.Add(catchBtn);
                            Controls.Add(pointBtn);
                            turn = 0;
                            break;
                        case 1000:
                            turn = 0;
                            this.Close();

                            break;


                        default:
                            break;
                    }
                    isFreeAttack = false;
                }
                else if(StaticSaver._pokinoList[index].getDex() > StaticSaver._pokinoEName.getDex())
                {
                    switch (count)
                    {
                        case 0:
                            MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " used " + StaticSaver._pokinoList[index].getMoves()[movecount].getName() + "!\n(Press K...)";

                            break;
                        case 1:
                            int dmg = StaticSaver._pokinoList[index].getMoves()[movecount].getBase() + getBono();

                            if (StaticSaver._pokinoList[index].getMoves()[movecount].didHit())
                            {
                                StaticSaver._pokinoEName.takeDmg(dmg, StaticSaver._pokinoList[index].getMoves()[movecount].getType());
                                EnemyHP.Text = "HP: " + StaticSaver._pokinoEName.getHp();
                                if (StaticSaver._pokinoEName.getHp() <= 0)
                                {
                                    MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " is Dead!\n(Press K..)";
                                }
                            }
                            else
                            {
                                MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " Missed!\n(Press K..)";
                            }
                            break;
                        case 2:
                            if (StaticSaver._pokinoEName.getHp() <= 0)
                            {
                                Random rnd = new Random();
                               
                                StaticSaver._pokinoList[index].addEXP(rnd.Next(10, StaticSaver._pokinoEName.getLvl()+10));
                                this.Close();
                            }
                            break;
                        case 3:
                            MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " used "+ StaticSaver._pokinoEName.getStrongMoveName() + "!\n(Press K..)";
                            break;
                        case 4:
                            if (StaticSaver._pokinoEName.getStrongMoveHit())
                            {
                                StaticSaver._pokinoList[index].takeDmg(StaticSaver._pokinoEName.getStrongMove(), StaticSaver._pokinoEName.getStrongMoveType());
                                PlayerHP.Text = "HP: " + StaticSaver._pokinoList[index].getHp() + "/" + StaticSaver._pokinoList[index].getMaxHp();
                                if (StaticSaver._pokinoList[index].getHp() <= 0)
                                {
                                    MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " is Dead!\n(Press K.)";
                                }
                            }
                            else
                            {
                                MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " Missed!\n(Press K.)";
                            }
                            break;
                        case 5:
                            if (StaticSaver._pokinoList[index].getHp() <= 0)
                            {
                                //StaticSaver._pokinoList[0].addEXP(10);
                                index += 1;
                                if (StaticSaver._pokinoList[index] == null)
                                {
                                    isDead = true;
                                    this.Close();
                                }
                                update();
                                //count = -1;
                                count = 7;
                                turn = 0;
                            }
                            break;
                        case 6:
                            MessagLb.Text = "What will " + StaticSaver._pokinoList[index].getName() + " do?";
                            Controls.Add(fightBtn);
                            Controls.Add(runBtn);
                            Controls.Add(catchBtn);
                            Controls.Add(pointBtn);
                            turn = 0;
                            break;
                        case 1000:
                            turn = 0;
                            this.Close();

                            break;


                        default:
                            break;
                    }
                }else
                {
                    switch (count)
                    {
                        case 0:
                            MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " used " + StaticSaver._pokinoEName.getStrongMoveName() + "!\n(Press K..)";
                            break;
                        case 1:
                            if (StaticSaver._pokinoEName.getStrongMoveHit())
                            {
                                StaticSaver._pokinoList[index].takeDmg(StaticSaver._pokinoEName.getStrongMove(), StaticSaver._pokinoEName.getStrongMoveType());
                                PlayerHP.Text = "HP: " + StaticSaver._pokinoList[index].getHp() + "/" + StaticSaver._pokinoList[index].getMaxHp();
                                if (StaticSaver._pokinoList[index].getHp() <= 0)
                                {
                                    MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " is Dead!\n(Press K.)";
                                }
                            }
                            else
                            {
                                MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " Missed!\n(Press K.)";
                            }
                            break;
                        case 2:
                            if (StaticSaver._pokinoList[index].getHp() <= 0)
                            {
                                //StaticSaver._pokinoList[0].addEXP(10);
                                index += 1;
                                if (StaticSaver._pokinoList[index] == null)
                                {
                                    isDead = true;
                                    this.Close();
                                }
                                update();
                                //count = -1;
                                count = 7;
                                turn = 0;
                            }
                            break;
                        case 3:
                            MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " used "+ StaticSaver._pokinoList[index].getMoves()[movecount].getName() + "!\n(Press K...)";
                            break;
                        case 4:
                            int dmg = StaticSaver._pokinoList[index].getMoves()[movecount].getBase() + getBono();
                            if(StaticSaver._pokinoList[index].getMoves()[movecount].didHit())
                            {
                                StaticSaver._pokinoEName.takeDmg(dmg, StaticSaver._pokinoList[index].getMoves()[movecount].getType());
                                EnemyHP.Text = "HP: " + StaticSaver._pokinoEName.getHp();
                                if (StaticSaver._pokinoEName.getHp() <= 0)
                                {
                                    MessagLb.Text = "" + StaticSaver._pokinoEName.getName() + " is Dead!\n(Press K..)";
                                }
                            }else
                            {
                                MessagLb.Text = "" + StaticSaver._pokinoList[index].getName() + " Missed!\n(Press K..)";
                            }
                            
                            break;
                        case 5:
                            if (StaticSaver._pokinoEName.getHp() <= 0)
                            {
                                Random rnd = new Random();

                                StaticSaver._pokinoList[index].addEXP(rnd.Next(10, StaticSaver._pokinoEName.getLvl() + 10));
                                this.Close();
                            }
                            break;
                        
                        case 6:
                            MessagLb.Text = "What will " + StaticSaver._pokinoList[index].getName() + " do?";
                            Controls.Add(fightBtn);
                            Controls.Add(runBtn);
                            Controls.Add(catchBtn);
                            Controls.Add(pointBtn);
                            turn = 0;
                            break;
                        case 1000:
                            turn = 0;
                            this.Close();

                            break;


                        default:
                            break;
                    }
                }

                
                movers = 50;
            }
            
        }

        public int getBono()
        {
            int num = 0;
            switch (StaticSaver._pokinoList[index].getMoves()[movecount].getType())
            {
                case "str":
                    num = (int)(StaticSaver._pokinoList[index].getMoves()[movecount].getScale() / 100f) * StaticSaver._pokinoList[index].getStr();
                    break;
                case "wis":
                    num = (int)(StaticSaver._pokinoList[index].getMoves()[movecount].getScale() / 100f) * StaticSaver._pokinoList[index].getWis();
                    break;
                case "dex":
                    num = (int)(StaticSaver._pokinoList[index].getMoves()[movecount].getScale() / 100f) * StaticSaver._pokinoList[index].getDex();
                    break;
                default:
                    break;
            }
            return num;
        }

        private void move(int movment, bool isX, bool isUp)
        {
            
                int x = pointBtn.Location.X;
                int y = pointBtn.Location.Y;
                if (!isX)
                {

                    if (!isUp)
                    {
                        if (y != 360)
                        {
                            y += movment;
                            pointBtn.Location = new Point(pointBtn.Location.X, y);
                        }
                    }
                    else
                    {
                        if (y != 260)
                        {
                            y -= movment;
                            pointBtn.Location = new Point(pointBtn.Location.X, y);
                        }
                    }


                }
                
            


        }

        DateTime timeStamp;
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {

            if ((DateTime.Now - timeStamp).Ticks < 500000) return;
            timeStamp = DateTime.Now;

            switch (e.KeyCode)
            {
                case Keys.K:
                    if (turn == 2)
                    {
                        Next_Click();
                    }else if(turn == 1)
                    {
                        int y = pointBtn.Location.Y;
                        switch (y)
                        {
                            case 360:
                                movecount = 2;
                                break;
                            case 310:
                                movecount = 1;
                                break;
                            case 260:
                                movecount = 0;
                                break;
                            default:
                                break;
                        }
                        if(StaticSaver._pokinoList[index].getMoves()[movecount].getName() != "Nothing")
                        {
                            turn = 2;
                            Controls.Remove(move1);
                            Controls.Remove(move2);
                            Controls.Remove(move3);
                            Controls.Remove(pointBtn);
                        }
                        
                    }
                    else
                    {
                        int y = pointBtn.Location.Y;
                        switch (y)
                        {
                            case 360:
                                Catch_Click();
                                break;
                            case 310:
                                this.Close();
                                break;
                            case 260:
                                Fight_Click();
                                break;
                            default:
                                break;
                        }
                        //Controls.Remove(pointBtn);
                        Controls.Remove(fightBtn);
                        Controls.Remove(runBtn);
                        Controls.Remove(catchBtn);
                    }
                    break;

                case Keys.W:

                    move(movers, false, true);
                    break;
               
                case Keys.S:

                    move(movers, false, false);
                    break;

                default:
                    break;
            }
        }

        private void update()
        {
            Controls.Remove(move1);
            Controls.Remove(move2);
            Controls.Remove(move3);
            Controls.Add(fightBtn);
            Controls.Add(runBtn);
            Controls.Add(catchBtn);
            Controls.Add(pointBtn);

            pbP.Image = Image.FromFile("./" + StaticSaver._pokinoList[index].getName() + ".png");
            PlayerLb.Text = StaticSaver._pokinoList[index].getName() + " [" + StaticSaver._pokinoList[index].getLvl() + "]:";

            PlayerHP.Text = "HP: " + StaticSaver._pokinoList[index].getHp() + "/" + StaticSaver._pokinoList[index].getMaxHp();
            
            PlayerExp.Text = "EXP: " + StaticSaver._pokinoList[index].getExp() + "/" + StaticSaver._pokinoList[index].getMaxEXP();

            move1 = new Button();
            createBtn(move1, 500, 250, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[0].getName());

            move2 = new Button();
            createBtn(move2, 500, 300, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[1].getName());

            move3 = new Button();
            createBtn(move3, 500, 350, 100, 45, Color.Black, Color.LightSteelBlue, StaticSaver._pokinoList[index].getMoves()[2].getName());
            Controls.Remove(move1);
            Controls.Remove(move2);
            Controls.Remove(move3);
        }
        private void Fight_Click()
        {
            Controls.Add(move1);
            Controls.Add(move2);
            Controls.Add(move3);

            if (StaticSaver._pokinoList[index].getHp() > 0)
            {
                if(turn == 0)
                {
                    count = -1;
                    turn = 1;
                }
               
            }else
            {
                this.Close();
            }
            
        }
        private void Catch_Click()
        {
            Random rnd = new Random();
            int n = 0;
            if (StaticSaver._pokinoList[index].getHp() > 0)
            {
                n = rnd.Next(0, (int)(StaticSaver._pokinoEName.getMaxHp() * 1.5) + 1);
                if(n > StaticSaver._pokinoEName.getHp())
                {
                    count = 999;
                    MessagLb.Text = StaticSaver._pokinoEName.getName()+ " was caught!";
                    isCatch = true;
                    //this.Close();
                    for (int i = 0; i < StaticSaver._pokinoList.Length; i++)
                    {
                        if(StaticSaver._pokinoList[i] == null)
                        {
                            StaticSaver._pokinoList[i] = StaticSaver._pokinoEName;
                            i = StaticSaver._pokinoList.Length;
                        }
                    }
                    turn = 2;
                }else
                {
                    isFreeAttack = true;
                    count = 2;
                    
                    turn = 2;
                }
                
            }
            else
            {
                this.Close();
            }
        }
        void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prompt user to save his data
                if (isDead)
                {
                    index -= 1;
                }

                if (StaticSaver._pokinoEName.getHp() <= 0 || isCatch || StaticSaver._pokinoList[index].getHp() <= 0)
                {
                    StaticSaver._isInFight = false;
                }
                else
                {

                    Random rnd = new Random();
                    int n = rnd.Next(0, 101);
                    if (50 <= n || StaticSaver._pokinoList[index].getLvl() > StaticSaver._pokinoEName.getLvl())
                    {
                        MessageBox.Show("Escaped!");
                        StaticSaver._isInFight = false;
                    }
                    else
                    {
                        MessagLb.Text = StaticSaver._pokinoList[index].getName() + " hurt himself trying to run!";
                        StaticSaver._pokinoList[index].takeDmg(5* StaticSaver._pokinoList[index].getLvl(), "dex");
                        update();
                        MessageBox.Show("Escape Failed!");
                        e.Cancel = true;
                    }
                }



            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Prompt user to save his data
                if (isDead)
                {
                    index -= 1;
                }

                if (StaticSaver._pokinoEName.getHp() == 0 || isCatch || StaticSaver._pokinoList[index].getHp() == 0)
                {
                    StaticSaver._isInFight = false;
                }
                else
                {

                    Random rnd = new Random();
                    int n = rnd.Next(0, 101);
                    if (50 <= n || StaticSaver._pokinoList[index].getLvl() > StaticSaver._pokinoEName.getLvl())
                    {
                        MessageBox.Show("Escaped!");
                        StaticSaver._isInFight = false;
                    }
                    else
                    {
                        MessagLb.Text = StaticSaver._pokinoList[index].getName() + " hurt himself trying to run!";
                        StaticSaver._pokinoList[index].takeDmg(5 * StaticSaver._pokinoList[index].getLvl(), "dex");
                        update();
                        MessageBox.Show("Escape Failed!");
                        e.Cancel = true;
                    }
                }
            }

        }

    }
}
