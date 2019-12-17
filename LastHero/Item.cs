using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastHero
{
    [Serializable]
    public class Item
    {
        internal string Name;

        public int Quality;
        public int Level;
        public int State;

        public string type;
        public Item()
        {

        }

        public Item(string _name, int _quality, int _level, int _state)
        {

        }
    }
}
