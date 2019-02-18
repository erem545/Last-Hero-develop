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
            Player player = new Player("Админ 11111111111");

            player.run();
            Console.ReadKey();
        }
    }
}
