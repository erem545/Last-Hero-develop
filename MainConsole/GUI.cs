using MainConsole.NPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class GUI
    {     
        public static int ShowMenu(params string[] Enter)
        {           
            {
                int TopPrev = Console.CursorTop - 1;

                //Описание переменных
                int IndCur = 0, IndPerv = 0, i;
                int posX = 39, posY = Console.CursorTop;
                bool itemSelected = false;

                // Пункты меню
                string[] selected = Enter;

                // Начальный ввод пунктов
                for (i = 0; i < selected.Length; i++)
                {
                    //Позицианирование и выбор цвета на начальных значениях
                    Console.CursorLeft = posX;
                    Console.CursorTop = posY + i;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(selected[i]);
                }
                do
                {
                    // Предыдущий пункт меню
                    Console.CursorLeft = posX;
                    Console.CursorTop = posY + IndPerv;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(selected[IndPerv]+"  ");

                    // Активный пункт меню
                    Console.CursorLeft = posX;
                    Console.CursorTop = posY + IndCur;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("->" + selected[IndCur]);

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    IndPerv = IndCur;

                    // Работа клавиш Вверх, Вниз и Выбор(Enter)
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            IndCur++;
                            break;
                        case ConsoleKey.UpArrow:
                            IndCur--;
                            break;
                        case ConsoleKey.Enter:
                            itemSelected = true;
                            break;
                    }
                    if (IndCur == selected.Length)
                        IndCur = selected.Length - 1;
                    else if (IndCur < 0)
                        IndCur = 0;
                } while (!itemSelected);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorTop = 2 + selected.Length + TopPrev;
                return ++IndCur;
            } //Вывод меню
        }
        public static void Action(Character character)
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Персонаж: {character.MainName}");
                Console.WriteLine(character.ToString());
                Console.WriteLine($"\"I\" - инвентарь \"O\" - локация  \"P\" - профиль");
                Console.WriteLine($"\"A\" - атаковать \"S\" - общаться \"D\" - двигаться");
                if (character.isAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\"Z\" - получить 20 урона \"X\" - восстановить 10 здоровья \"C\" - ...");


                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.I:
                        Console.WriteLine($"Инвентарь");
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine($"Локация");
                        break;
                    case ConsoleKey.P:
                        Console.WriteLine($"Профиль");
                        character.ShowAllInfo();
                        break;

                    case ConsoleKey.A:
                        Console.WriteLine($"Атака");
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine($"Общение");
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine($"Двигаться");
                        break;

                    case ConsoleKey.Z:
                        Console.WriteLine($"Получить урон");
                        character.ToDamage(20);
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine($"Восстановитть здоровье");
                        character.ToHeal(20);
                        break;
                    case ConsoleKey.C:
                        Console.WriteLine($"...");
                        break;
                    default:
                        Console.WriteLine($"Ничего");
                        break;
                }
                Console.ReadKey();
                character.Refresh();
            } while (true);
        }
    }
}
