﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    class Ogre : Character
    {
        public Ogre() : base()
        {          
            EnduranceRegenPercent = 0;
            HealthRegenPercent = 0;
        }
        public Ogre(string _name, float _maxHealth, float _maxEndurance, bool isAdm, int s, int a, int i) : 
            base(_name, _maxHealth, _maxEndurance, isAdm, s, a, i)
        {
            EnduranceRegenPercent = 0.01f;
            HealthRegenPercent = 0.006f;
        }
    }
}
