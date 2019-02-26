using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class GUI
    {
        public static int ShowMenu(Character character, string Des, params string[] Enter)
        {
            {
                Console.Clear();
                Console.WriteLine(character.ToString());

                Console.WriteLine(Des);


                int TopPrev = Console.CursorTop;

                //Описание переменных
                int IndCur = 0, IndPerv = 0, i;
                int posX = 0, posY = Console.CursorTop;
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
                    Console.Write(selected[IndPerv]);

                    // Активный пункт меню
                    Console.CursorLeft = posX;
                    Console.CursorTop = posY + IndCur;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(selected[IndCur]);

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

        //public GUI(int a, int b, Race race )
        //{

        //}
        //public static GUI NewDeadRace(int a, int b)
        //{
        //    return null;
        //}
    }
}
