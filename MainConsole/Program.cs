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
            Ogre c1 = new Ogre("Бугай", 200, 30, false, 10, 8, 2);
            Weapon weapon1 = new Weapon("Топор", 1, 1, 100, 3, 10, "Ближний бой");
            c1.ToTake(weapon1);

            Human c2 = new Human("Пехотинец", 150, 30, false, 7, 10, 4);
            Weapon weapon2 = new Weapon("Меч", 1, 1, 100, 7, 8, "Ближний бой");
            c2.ToTake(weapon2);

            BattleClass.CreateBattle(c1,c2);
            Console.ReadLine();
        }
    }
}
