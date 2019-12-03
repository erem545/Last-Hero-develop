using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    class BattleClass
    {
        /// <summary>
        /// Атакующие действия противника
        /// </summary>
        /// <param name="c1">Противник</param>
        /// <param name="c2">Персонаж игрока (ГГ)</param>
        private static void AttackEnemy(Character c1, Character c2)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            switch (rnd.Next(1, 6 + 1))
            {
                case 1:
                    c1.ToAttack(c2, c2.bodyNode.head);
                    break;
                case 2:
                    c1.ToAttack(c2, c2.bodyNode.body);
                    break;
                case 3:
                    c1.ToAttack(c2, c2.bodyNode.lhand);
                    break;
                case 4:
                    c1.ToAttack(c2, c2.bodyNode.rhand);
                    break;
                case 5:
                    c1.ToAttack(c2, c2.bodyNode.lfoot);
                    break;
                case 6:
                    c1.ToAttack(c2, c2.bodyNode.rfoot);
                    break;
            }
        }
        /// <summary>
        /// Создать бой между Character c1 и Character c2
        /// </summary>
        /// <param name="c1">Персонаж игрока (ГГ)</param>
        /// <param name="c2">Противник</param>
        public static void CreateBattle(Character c1, Character c2)
        {
            do
            {
                Console.Clear();
                TextFactory.TwoCharacterInfo(c1, c2);
                Console.WriteLine();
                switch (GUI.ShowMenu(
                    $"Голова  \t {c2.bodyNode.head.Status} ({c2.bodyNode.head.PercentStatus}%)  ",
                    $"Тело    \t {c2.bodyNode.body.Status} ({c2.bodyNode.body.PercentStatus}%)  ",
                    $"Л. Рука \t {c2.bodyNode.lhand.Status} ({c2.bodyNode.lhand.PercentStatus}%)",
                    $"П. Рука \t {c2.bodyNode.rhand.Status} ({c2.bodyNode.rhand.PercentStatus}%)",
                    $"Л. Нога \t {c2.bodyNode.lfoot.Status} ({c2.bodyNode.lfoot.PercentStatus}%)",
                    $"П. Нога \t {c2.bodyNode.rfoot.Status} ({c2.bodyNode.rfoot.PercentStatus}%)"))
                {
                    case 1:
                        c1.ToAttack(c2, c2.bodyNode.head);
                        break;
                    case 2:
                        c1.ToAttack(c2, c2.bodyNode.body);
                        break;
                    case 3:
                        c1.ToAttack(c2, c2.bodyNode.lhand);
                        break;
                    case 4:
                        c1.ToAttack(c2, c2.bodyNode.rhand);
                        break;
                    case 5:
                        c1.ToAttack(c2, c2.bodyNode.lfoot);
                        break;
                    case 6:
                        c1.ToAttack(c2, c2.bodyNode.rfoot);
                        break;
                }               
                Console.WriteLine("Ход " + c2.MainName);
                AttackEnemy(c2,c1);
                Console.ReadKey();
                if (c1.ok == false || c2.ok == false)
                    return;
            } while ((c1.ok) || (c2.ok));
        }
    }
}
