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
            Player p1 = new Player("Пехотинец", 105, 20, Race.Humans);
            Player p2 = new Player("Бугай", 250, 30, Race.Ogres);


            do
            {
                int doing = GUI.ShowMenu(p2, "\n** Главное меню **", "Атаковать", "Обороняться", "Показать статистику", "Показать статистику противника");
                switch (doing)
                {
                    case 1:
                        p2.ToAttack(p1);
                        Console.ReadKey();
                        break;
                    case 2:
                        p2.ToDefend(p1);
                        Console.ReadKey();
                        break;
                    case 3:
                        p2.ShowAllInfo();
                        Console.ReadKey();
                        break;
                    case 4:
                        p1.ShowAllInfo();
                        Console.ReadKey();
                        break;
                }
            } while (true);
         
        }
    }
}
