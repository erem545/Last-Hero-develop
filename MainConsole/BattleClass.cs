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
            Random rnd = new Random(DateTime.Now.Millisecond);
            switch (rnd.Next(1, 7))
            {
                case 1:
                    if (ProbabilityClass.ChanceToHit(c1, 20))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар в голову " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.head);
                    }
                    else
                        Console.WriteLine("Промах!");
                    break;
                case 2:
                    if (ProbabilityClass.ChanceToHit(c1, 50))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар в тело " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.body);
                    }
                    else
                        Console.WriteLine("Промах!");
                    break;
                case 3:
                    if (ProbabilityClass.ChanceToHit(c1, 30))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по левой руке " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.lhand);
                    }
                    else
                        Console.WriteLine("Промах!");
                    break;
                case 4:
                    if (ProbabilityClass.ChanceToHit(c1, 30))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по правой руке " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.rhand);
                    }
                    else
                        Console.WriteLine("Промах!");
                    break;
                case 5:
                    if (ProbabilityClass.ChanceToHit(c1, 30))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по левой ноге " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.lfoot);
                    }
                    else
                        Console.WriteLine("Промах!");
                    break;
                case 6:
                    if (ProbabilityClass.ChanceToHit(c1, 30))
                    {
                        Console.Write($"{DateTime.Now.ToString()} | Удар по правой ноге " + c2.MainName);
                        c1.ToAttack(c2, c2.bodyNode.rfoot);
                    }
                    else
                        Console.WriteLine("Промах!");
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
                    $"Голова  :: {Math.Round(c2.bodyNode.head.Status)}  ({ Math.Round(c2.bodyNode.head.PercentStatus )}%) (Точность {(c1.Accuaracy * 20)/10}%)",
                    $"Тело    :: {Math.Round(c2.bodyNode.body.Status)}  ({ Math.Round(c2.bodyNode.body.PercentStatus )}%) (Точность {(c1.Accuaracy * 50)/10}%)",
                    $"Л. Рука :: {Math.Round(c2.bodyNode.lhand.Status)}  ({Math.Round(c2.bodyNode.lhand.PercentStatus)}%) (Точность {(c1.Accuaracy * 30)/10}%)",
                    $"П. Рука :: {Math.Round(c2.bodyNode.rhand.Status)}  ({Math.Round(c2.bodyNode.rhand.PercentStatus)}%) (Точность {(c1.Accuaracy * 30)/10}%)",
                    $"Л. Нога :: {Math.Round(c2.bodyNode.lfoot.Status)}  ({Math.Round(c2.bodyNode.lfoot.PercentStatus)}%) (Точность {(c1.Accuaracy * 30)/10}%)",
                    $"П. Нога :: {Math.Round(c2.bodyNode.rfoot.Status)}  ({Math.Round(c2.bodyNode.rfoot.PercentStatus)}%) (Точность {(c1.Accuaracy * 30)/10}%)"))
                {
                    case 1:
                        if (ProbabilityClass.ChanceToHit(c1, 20))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар в голову " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.head);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                    case 2:        
                        if (ProbabilityClass.ChanceToHit(c1, 50))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар в тело " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.body);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                    case 3:                    
                        if (ProbabilityClass.ChanceToHit(c1, 30))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по левой руке " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.lhand);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                    case 4:                     
                        if (ProbabilityClass.ChanceToHit(c1, 30))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по правой руке " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.rhand);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                    case 5:                        
                        if (ProbabilityClass.ChanceToHit(c1, 30))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по левой ноге " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.lfoot);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                    case 6:                     
                        if (ProbabilityClass.ChanceToHit(c1, 30))
                        {
                            Console.Write($"{DateTime.Now.ToString()} | Удар по правой ноге " + c2.MainName);
                            c1.ToAttack(c2, c2.bodyNode.rfoot);
                        }
                        else
                            Console.WriteLine("Промах!");
                        break;
                }
                Thread.Sleep(1000);
                AttackEnemy(c2, c1);               
                Console.ReadKey();
                if (c1.ok == false || c2.ok == false)
                    return;
            } while ((c1.ok) || (c2.ok));
        }
    }
}
