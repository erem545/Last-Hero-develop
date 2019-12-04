using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    public class Human : Character
    {
        public Human() : base()
        {

        }
        public Human(string _name, float _maxHealth, float _maxEndurance, int s, int a, int i) :
            base(_name, _maxHealth, _maxEndurance, s, a, i)
        {

        }
    }
}
