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
            Ogre c1 = new Ogre("Бугай", 200, 30, false);
            Weapon weapon = new Weapon("Топор", 1, 1, 100, 5, 7, "Ближний бой");
            c1.ToTake(weapon);
            Ogre c2 = new Ogre("Волк", 100, 30, false);
            BattleClass.CreateBattle(c1,c2);
            Console.ReadLine();
        }
    }
}
