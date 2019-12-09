using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastHero
{
    public class Ogre : Character
    {
        public Ogre() : base()
        {

        }
        public Ogre(string _name, float _maxHealth, float _maxEndurance, int s, int a, int i) :
            base(_name, _maxHealth, _maxEndurance, s, a, i)
        {

        }
    }
}
