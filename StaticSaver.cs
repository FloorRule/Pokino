using System;
using System.Collections.Generic;
using System.Text;

namespace Pokino
{
    public class StaticSaver
    {
        public static string _playerName;

        public static Monster[] _pokinoList;

        public static Monster _pokinoEName;

        public static bool _isCreated;
        public static bool _isInFight;

        public static GameEngine f1;
        public static StarterForm form4;
        public static InvForm invForm;

        public StaticSaver(string name, Monster starter)
        {
            _playerName = name;
            _pokinoList = new Monster[5];
            _pokinoList[0] = starter;
        }

        public void setEnemy(Monster enemy)
        {
            _pokinoEName = enemy;
        }

        public void setCreated(bool isCreated)
        {
            _isCreated = isCreated;
        }
    }
}
