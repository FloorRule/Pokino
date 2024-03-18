using System;
using System.Collections.Generic;
using System.Text;

namespace Pokino
{
    public class Move
    {
        private string _name;
        private int _baseDmg;
        private string _typeMove;
        private int _statScale;
        private int _missScale;

        public Move(string name, int baseD, string type, int scale, int miss)
        {
            this._name = name;
            this._baseDmg = baseD;
            this._typeMove = type;
            this._statScale = scale;
            this._missScale = miss;

        }

        public int getBase()
        {
            return this._baseDmg;
        }

        public int getScale()
        {
            return this._statScale;
        }

        public string getType()
        {
            return this._typeMove;
        }

        public string getName()
        {
            return this._name;
        }

        public bool didHit()
        {
            Random rnd = new Random();
            int n = rnd.Next(1, 101);
            if(n <= this._missScale)
            {
                return true;
            }else
            {
                return false;
            }
        }

    }
}
