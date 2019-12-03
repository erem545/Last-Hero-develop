using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class TextFactory
    {
        class ConvertText
        {
            /// <summary>
            /// Конвертор из строки String в массив символов
            /// </summary>
            /// <param name="str">Строка на вход</param>
            /// <returns>Возвращает массив вида char[]</returns>
            public static char[] ConvertStringToArraryChar(string str)
            {
                if ((str == null) || (str.Length == 0))
                    return null;
                char[] arr = new char[str.Length];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = str[i];
                return arr;
            }         
            public static string ConvertArrayCharToString(char[] arr)
            {
                return new string(arr);
            }
        }


        ///////////
        int a_size;
        int b_size;
        ///////////



        /// <summary>
        /// Максимальная длина в рваном массиве
        /// </summary>
        /// <param name="arr"></param>
        private static int MaxLengthInTornArray(char[][]arr)
        {
            int MaxLength = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int counter = 0;
                for (int j = 0; j < arr[i].Length; j++)
                {
                    counter++;
                }
                if (counter > MaxLength)
                    MaxLength = counter;
            }
            return MaxLength;
        }
        public static char[][] CharacterInfo(NPS.Character player)
        {
            char[][] MainInformation = new char[4][];
            string str1 = $"Общая инофрмация"; char[] str1_c = new char[5];
            string str2 = $"{player.MainName} ({player.Level} ур.) {player.XP} xp.";
            string str3 = $"Здоровье: {player.Health} / {player.MaxHealth} ({player.PercentHealth}%)";
            string str4 = $"Выносливость: {player.Endurance} / {player.MaxEndurance} ({player.PercentEndurance}%)";
            #region
            MainInformation[0] = ConvertText.ConvertStringToArraryChar(str1);
            MainInformation[1] = ConvertText.ConvertStringToArraryChar(str2);
            MainInformation[2] = ConvertText.ConvertStringToArraryChar(str3);
            MainInformation[3] = ConvertText.ConvertStringToArraryChar(str4);
# endregion
            
            //foreach (char[] arr in MainInformation) 
            //{ 
            //    foreach (char c in arr)
            //        Console.Write(c);
            //    Console.WriteLine();
            //}
            return MainInformation;
        }
        public static char[][] CharacterAllInfo(NPS.Character player)
        {
            char[][] MainInformation = new char[12][];
            string str1 = $"Общая инофрмация"; char[] str1_c = new char[5];
            string str2 = $"{player.MainName} ({player.Level} ур.) {player.XP} xp.";
            string str3 = $"Здоровье: {player.Health} / {player.MaxHealth} ({player.PercentHealth}%)";
            string str4 = $"Выносливость: {player.Endurance} / {player.MaxEndurance} ({player.PercentEndurance}%)";
            string str5 = $"-- Характеристики";
            string str6 = $"- Защита: {player.ArmorValue}";
            string str7 = $"- Атака: {player.minAttack} - {player.maxAttack}";
            string str8 = $"- Сила: {player.Strength}";
            string str9 = $"- Ловкость: {player.Agility}";
            string str10 = $"- Интеллект: {player.Intelligance}";
            string str11 = $"- Лидерство: {player.Leadership}";
            string str12 = $"- Карма: {player.Karma}";
            #region
            MainInformation[0] = ConvertText.ConvertStringToArraryChar(str1);
            MainInformation[1] = ConvertText.ConvertStringToArraryChar(str2);
            MainInformation[2] = ConvertText.ConvertStringToArraryChar(str3);
            MainInformation[3] = ConvertText.ConvertStringToArraryChar(str4);
            MainInformation[4] = ConvertText.ConvertStringToArraryChar(str5);
            MainInformation[5] = ConvertText.ConvertStringToArraryChar(str6);
            MainInformation[6] = ConvertText.ConvertStringToArraryChar(str7);
            MainInformation[7] = ConvertText.ConvertStringToArraryChar(str8);
            MainInformation[8] = ConvertText.ConvertStringToArraryChar(str9);
            MainInformation[9] = ConvertText.ConvertStringToArraryChar(str10);
            MainInformation[10] = ConvertText.ConvertStringToArraryChar(str11);
            MainInformation[11] = ConvertText.ConvertStringToArraryChar(str12);
            #endregion

            //foreach (char[] arr in MainInformation)
            //{
            //    foreach (char c in arr)
            //        Console.Write(c);
            //    Console.WriteLine();
            //}
            return MainInformation;
        }


        public static void TwoCharacterInfo(NPS.Character player1, NPS.Character player2)
        {
            char[][] ArrayPlayer1 = CharacterInfo(player1);
            char[][] ArrayPlayer2 = CharacterInfo(player2);
            int maximum = MaxLengthInTornArray(ArrayPlayer1) + 5;
            for (int i = 0; i < ArrayPlayer1.Length; i++)
            { // Работаем с подмассивом
                // Счетчик
                int counter = 0;

                for (int j = 0; j < ArrayPlayer1[i].Length; j++)
                { // Работа с массивом 1
                    counter++;
                    Console.Write(ArrayPlayer1[i][j]);
                }
                // Кол-во пробелов
                for (int k = counter; k <= maximum + 4; k++)
                    Console.Write(" ");
                counter = 0;

                for (int j = 0; j < ArrayPlayer2[i].Length; j++)
                { // Работа с массивом 2
                    counter++;
                    Console.Write(ArrayPlayer2[i][j]);
                    //for (int k = counter; k < maximum; k++)
                    //    Console.Write(" ");
                }
                Console.WriteLine(); 
            }
        }

    }
}
