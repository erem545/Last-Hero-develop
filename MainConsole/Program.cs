using MainConsole.NPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Ogre c1 = new Ogre("Бугай",200, 30, true);
            GUI.Action(c1);
        }
    }
}
