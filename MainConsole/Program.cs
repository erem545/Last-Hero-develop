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
            Player p1 = new Player("Денис ДОП АДМИН", 120, 10, 250);
            Player p2 = new Player("БОООТТТТТ ДОП 222132131", 150, 10, 450);
            p1.body.PartBodyManager("Damage", 10);
            p1.ShowCharacteristic();
            p1.body.PartBodyManager("Heal", 20);
            p1.ShowCharacteristic();
            Console.ReadKey();
        }
    }
}
