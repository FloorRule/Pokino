using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Pokino
{
    public class Monster
    {
        private string _name;
        private int _lvl;
        private int _exp;
        private int maxExp;

        private int hp;
        private int maxHp;

        private int[] _stats = new int[5];//0str 1wis 2dex 3def 4mental
        private List<Move> _moves = new List<Move>();
        
        private bool isDef = false;
        private int _main;

        public Monster(string name, int exp, int hp, int[] stats, int main, int lvlset = 0)
        {
            this._name = name;
            this.hp = hp;
            this.maxHp = hp;
            int lvl = 5;
            maxExp = 25;
            this._main = main;

            this._stats = stats;

            //this._moves.Add(new Move("Bite", 5, "str", 10));
            //this._moves.Add(new Move("Nothing", 0, "wis", 0));
            //this._moves.Add(new Move("Nothing", 0, "wis", 0));
            if (exp == 999)
            {
                this._lvl = lvlset;
                if (lvlset > 5)
                {
                    for (int i = 5; i < lvlset; i++)
                    {
                        this.maxHp += hp * 2;
                    }
                }
            }
            else
            {

                while (exp >= maxExp)
                {
                    lvl += 1;
                    this.maxHp += hp * 2;
                    exp -= maxExp;
                    maxExp += 25;

                    for (int i = 0; i < this._stats.Length; i++)
                    {
                        if (i == this._main)
                        {
                            this._stats[i] += 2;
                        }
                        else
                        {
                            this._stats[i]++;
                        }
                    }
                    setUpMoves(name);
                }
                this._lvl = lvl;
                this._exp = exp;
            }
            this.hp = this.maxHp;

            setUpMoves(name);
            
        }

        public int getStrongMove()
        {
            int dmg = -1;
            int index = -1;
            for (int i = 0; i < _moves.Count; i++)
            {
                if(_moves[i].getBase() > dmg)
                {
                    dmg = _moves[i].getBase();
                    index = i;
                }
            }
            return dmg;
        }

        public string getStrongMoveType()
        {
            int dmg = -1;
            int index = -1;
            for (int i = 0; i < _moves.Count; i++)
            {
                if (_moves[i].getBase() > dmg)
                {
                    dmg = _moves[i].getBase();
                    index = i;
                }
            }
            return _moves[index].getType();
        }

        public bool getStrongMoveHit()
        {
            int dmg = -1;
            int index = -1;
            for (int i = 0; i < _moves.Count; i++)
            {
                if (_moves[i].getBase() > dmg)
                {
                    dmg = _moves[i].getBase();
                    index = i;
                }
            }
            return _moves[index].didHit();
        }

        public string getStrongMoveName()
        {
            int dmg = -1;
            int index = -1;
            for (int i = 0; i < _moves.Count; i++)
            {
                if (_moves[i].getBase() > dmg)
                {
                    dmg = _moves[i].getBase();
                    index = i;
                }
            }
            return _moves[index].getName();
        }

        public void setUpMoves(string name)
        {
            this._moves.Clear();
            StreamReader reader = new StreamReader("./" + name + "MoveSet.txt");
            
            string content = reader.ReadToEnd();
            string[] moveList = content.Split("+");
            string[] move;
            for (int i = 0; i < moveList.Length; i++)
            {
                move = moveList[i].Split("#");
                
                if (move[0] == this._lvl.ToString())
                {
                    if (this._moves.Count != 3)
                    {
                        string[] movinfo = move[1].Split("-");
                        this._moves.Add(new Move(movinfo[0], int.Parse(movinfo[1]), movinfo[2], int.Parse(movinfo[3]), int.Parse(movinfo[4])));
                    }
                }
            }
            reader.Close();
        }

        public void healMaxHp()
        {
            this.hp = this.maxHp;
        }

        public string getName()
        {
            return this._name;
        }

        public int getDex()
        {
            return this._stats[2];
        }

        public int getStr()
        {
            return this._stats[0];
        }

        public int getWis()
        {
            return this._stats[1];
        }

        public List<Move> getMoves()
        {
            return this._moves;
        }

        public int getLvl()
        {
            return this._lvl;
        }

        public int getHp()
        {
            return this.hp;
        }

        public int getExp()
        {
            return this._exp;
        }

        public int getMaxHp()
        {
            return this.maxHp;
        }

        public int getMaxEXP()
        {
            return this.maxExp;
        }

        public void setDef(bool def)
        {
            this.isDef = def;
        }
        public void takeDmg(int dmg, string type)
        {
            if(type == "str")
            {
                dmg -= (this._stats[3] / 2);
                this.hp -= dmg;
            }
            else if(type == "wis")
            {
                dmg -= (this._stats[4] / 2);
                this.hp -= dmg;
            }
            else
            {
                this.hp -= dmg;
            }
            
        }

        public void addEXP(int exp)
        {
            if(this._lvl != 10)
            {
                this._exp += exp;
                while (this._exp >= this.maxExp)
                {
                    this._lvl += 1;
                    this.maxHp += (this.maxHp / 2);
                    this._exp -= this.maxExp;
                    this.maxExp += 25;
                    for (int i = 0; i < this._stats.Length; i++)
                    {
                        if (i == this._main)
                        {
                            this._stats[i] += 2;
                        }
                        else
                        {
                            this._stats[i]++;
                        }

                    }
                    setUpMoves(this._name);
                }
            }
            
           
        }
    }
}
