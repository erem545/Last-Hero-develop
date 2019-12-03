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
            Ogre c1 = new Ogre("Бугай", 200, 30, false, 10, 3, 2);
            Weapon weapon1 = new Weapon("Топор", 1, 1, 100, 3, 10, "Ближний бой");
            c1.ToTake(weapon1);

            Human c2 = new Human("Пехотинец", 150, 30, false, 7, 7, 4);
            Weapon weapon2 = new Weapon("Меч", 1, 1, 100, 7, 8, "Ближний бой");
            c2.ToTake(weapon2);

            TextFactory text = new TextFactory();
            //text.CharacterInfo(c1);
            //Armor armor1 = new Armor("Бумажная шляпа",   1, 1, 100, 1, 1, 1, "Голова");
            //Armor armor2 = new Armor("Старая футболка",  1, 1, 100, 1, 1, 1, "Торс");
            //Armor armor3 = new Armor("Дырявая перчатка", 1, 1, 100, 1, 0, 0, "Л.Рука");
            //Armor armor4 = new Armor("Дырявая перчатка", 1, 1, 100, 1, 0, 0, "П.Рука");
            //Armor armor5 = new Armor("Носок",            1, 1, 100, 0, 1, 0, "Л.Нога");
            //Armor armor6 = new Armor("Носок",            1, 1, 100, 0, 1, 0, "П.Нога");
            BattleClass.CreateBattle(c1,c2);
            //c1.bodyNode.WearArmor(armor1);
            //c1.bodyNode.WearArmor(armor2);
            //c1.bodyNode.WearArmor(armor3);
            //c1.bodyNode.WearArmor(armor4);
            //c1.bodyNode.WearArmor(armor5);
            //c1.bodyNode.WearArmor(armor6);

            Console.ReadLine();
        }
    }
}
