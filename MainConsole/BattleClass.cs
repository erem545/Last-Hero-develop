using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            int chance1 = (int)(c1.Accuaracy / 2 * 30);
            int chance2 = (int)(c1.Accuaracy / 2 * 60);
            int chance3 = (int)(c1.Accuaracy / 2 * 40);
            int chance4 = (int)(c1.Accuaracy / 2 * 40);
            int chance5 = (int)(c1.Accuaracy / 2 * 40);
            int chance6 = (int)(c1.Accuaracy / 2 * 40);

            Random rnd = new Random(DateTime.Now.Millisecond);
            switch (rnd.Next(1, 7))
            {
                case 1:
                    if (ProbabilityClass.ChanceToHit(c1, chance1))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар в голову " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.head);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                    break;
                case 2:
                    if (ProbabilityClass.ChanceToHit(c1, chance2))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар в тело " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.body);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                    break;
                case 3:
                    if (ProbabilityClass.ChanceToHit(c1, chance3))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по левой руке " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.lhand);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                    break;
                case 4:
                    if (ProbabilityClass.ChanceToHit(c1, chance4))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по правой руке " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.rhand);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                    break;
                case 5:
                    if (ProbabilityClass.ChanceToHit(c1, chance5))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по левой ноге " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.lfoot);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                    break;
                case 6:
                    if (ProbabilityClass.ChanceToHit(c1, chance6))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по правой ноге " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.rfoot);
                    }
                    else
                        Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
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
            /// Процент попадания по частям тела:
            /// Голова 20%
            /// Тело 50%
            /// Л.П. Руки и Л.П. Ноги 30%

            int chance1 = (int)(c1.Accuaracy / 2 * 30);
            int chance2 = (int)(c1.Accuaracy / 2 * 60);
            int chance3 = (int)(c1.Accuaracy / 2 * 40);
            int chance4 = (int)(c1.Accuaracy / 2 * 40);
            int chance5 = (int)(c1.Accuaracy / 2 * 40);
            int chance6 = (int)(c1.Accuaracy / 2 * 40);

            do
            {
                Console.Clear();
                TextFactory.TwoCharacterInfo(c1, c2);
                Console.WriteLine();
                switch (GUI.ShowMenu(
                    $"Голова  :: {Math.Round(c2.bodyNode.head.Status)}  ({ Math.Round(c2.bodyNode.head.PercentStatus )}%) (Точность {chance1}%)",
                    $"Тело    :: {Math.Round(c2.bodyNode.body.Status)}  ({ Math.Round(c2.bodyNode.body.PercentStatus )}%) (Точность {chance2}%)",
                    $"Л. Рука :: {Math.Round(c2.bodyNode.lhand.Status)}  ({Math.Round(c2.bodyNode.lhand.PercentStatus)}%) (Точность {chance3}%)",
                    $"П. Рука :: {Math.Round(c2.bodyNode.rhand.Status)}  ({Math.Round(c2.bodyNode.rhand.PercentStatus)}%) (Точность {chance4}%)",
                    $"Л. Нога :: {Math.Round(c2.bodyNode.lfoot.Status)}  ({Math.Round(c2.bodyNode.lfoot.PercentStatus)}%) (Точность {chance5}%)",
                    $"П. Нога :: {Math.Round(c2.bodyNode.rfoot.Status)}  ({Math.Round(c2.bodyNode.rfoot.PercentStatus)}%) (Точность {chance6}%)"))
                {
                    case 1:
                        if (ProbabilityClass.ChanceToHit(c1, chance1))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар в голову " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.head);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                    case 2:        
                        if (ProbabilityClass.ChanceToHit(c1, chance2))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар в тело " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.body);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                    case 3:                    
                        if (ProbabilityClass.ChanceToHit(c1, chance3))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по левой руке " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.lhand);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                    case 4:                     
                        if (ProbabilityClass.ChanceToHit(c1, chance4))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по правой руке " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.rhand);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                    case 5:                        
                        if (ProbabilityClass.ChanceToHit(c1, chance5))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по левой ноге " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.lfoot);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                    case 6:                     
                        if (ProbabilityClass.ChanceToHit(c1, chance6))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по правой ноге " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.rfoot);
                        }
                        else
                            Console.WriteLine($"{DateTime.Now.ToString()} | Промах!");
                        break;
                }
                Thread.Sleep(1000);
                AttackEnemy(c2, c1);
                Thread.Sleep(1000);
                if (c1.ok == false || c2.ok == false)
                    return;
            } while ((c1.ok) || (c2.ok));
        }
    }
}
