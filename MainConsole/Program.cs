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
            Player p1 = new Player("Пехотинец (2 ур.)", 105, 20);
            Player p2 = new Player("Бугай (3 ур.)",200, 15);
            p1.ShowAllInfo();
            p2.ShowAllInfo();
            p1.ToAttack(p2);
            p2.ToAttack(p1);
            p1.ShowAllInfo();
            p2.ShowAllInfo();
            Console.ReadKey();
        }
    }
}
