using System;
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
            minAttack = 0;
            maxAttack = 0;
        }
        public Ogre(string _name, float _maxHealth, float _maxEndurance, bool isAdm) : 
            base(_name, _maxHealth, _maxEndurance, isAdm)
        {
            EnduranceRegenPercent = 0.01f;
            HealthRegenPercent = 0.006f;
            minAttack = 0;
            maxAttack = 0;
        }
    }
}
