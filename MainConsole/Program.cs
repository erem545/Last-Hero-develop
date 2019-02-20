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
            Player p1 = new Player("Воин", 105, 20);

            p1.body.lfoot.Damage(3);
            p1.body.rhand.Damage(2);
            p1.Refresh();
            p1.ShowAllInfo();

            Console.ReadKey();
        }
    }
}
