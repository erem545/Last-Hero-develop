﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    [Serializable]
    class Item
    {
        internal string Name;

        internal int Quality;
        internal int Level;
        internal int State;

        public Item()
        {

        }

        public Item(string _name, int _quality, int _level, int _state)
        {

        }
    }
}
