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


        private static char[][] JoinArrays(char[][] arr1, char[][] arr2)
        {
            int max = 0;
            char[][] array;
            if (arr1.Length >= arr2.Length) 
            {
                array = new char[arr1.Length][];
                for (int i = 0; i < arr1.Length; i++)
                {
                    int counter = 0;
                    for (int j = 0; j < arr1[i].Length + arr2[i].Length + 2; j++)
                        counter++;
                    array[i] = new char[counter + 10];
                } // Создание пустого массива

                max = MaxLengthInTornArray(arr1);
                for (int i = 0; i < array.Length; i++) // Первый массив
                {
                    int counter = 0;
                    for (int j = 0; j < arr1[i].Length; j++)
                    {
                        counter++;
                        array[i][j] = arr1[i][j];
                    }

                    // Кол-во пробелов
                    for (int k = counter; k <= max + 4; k++)
                        array[i][k] = ' ';

                    counter = max + 4;

                    for (int j = counter; j < arr2[i].Length; j++) // Второй массив
                    {
                        array[i][j] = arr2[i][j];
                    }
                }
            }
            else
            {
                array = new char[arr2.Length][];
                for (int i = 0; i < arr2.Length; i++)
                {
                    int counter = 0;
                    for (int j = 0; j < arr1[i].Length + arr2[i].Length + 2; j++)
                        counter++;
                    array[i] = new char[counter + 10];
                } // Создание пустого массива

                max = MaxLengthInTornArray(arr2);
                for (int i = 0; i < array.Length; i++) // Первый массив
                {
                    int counter = 0;
                    for (int j = 0; j < arr2[i].Length; j++)
                    {
                        counter++;
                        array[i][j] = arr2[i][j];
                    }

                    // Кол-во пробелов
                    for (int k = counter; k <= max + 4; k++)
                        array[i][k] = ' ';

                    counter = max + 4;

                    for (int j = counter; j < arr1[i].Length; j++) // Второй массив
                    {
                        array[i][j] = arr1[i][j];
                    }
                }
            }
            return array;
        }

        /// <summary>
        /// Информация об игроке (Основное) записывается в рваный массив символов
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static char[][] CharacterInfo(NPS.Character player)
        {
            char[][] MainInformation = new char[4][];
            #region
            MainInformation[0] = ConvertText.ConvertStringToArraryChar($"*** Общая инофрмация ***");
            MainInformation[1] = ConvertText.ConvertStringToArraryChar($"{player.MainName} ({player.Level} ур.) {player.XP} xp.");
            MainInformation[2] = ConvertText.ConvertStringToArraryChar($"Здоровье: {player.Health} / {player.MaxHealth} ({player.PercentHealth}%)");
            MainInformation[3] = ConvertText.ConvertStringToArraryChar($"Выносливость: {player.Endurance} / {player.MaxEndurance} ({player.PercentEndurance}%)");
            #endregion
            return MainInformation;
        }

        /// <summary>
        /// Информация об игроке (Подробно) записывается в рваный массив символов
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns></returns>
        private static char[][] CharacterAllInfo(NPS.Character player)
        {
            char[][] MainInformation = new char[20][];
            #region
            MainInformation[0] =  ConvertText.ConvertStringToArraryChar($"*** ОБЩАЯ ИНФОРМАЦИЯ ***");
            MainInformation[1] =  ConvertText.ConvertStringToArraryChar($"| {player.MainName} ({player.Level} ур.) {player.XP} xp.");
            MainInformation[2] =  ConvertText.ConvertStringToArraryChar($"| Здоровье: {player.Health} / {player.MaxHealth} ({player.PercentHealth}%)");
            MainInformation[3] =  ConvertText.ConvertStringToArraryChar($"| Выносливость: {player.Endurance} / {player.MaxEndurance} ({player.PercentEndurance}%)");
            MainInformation[4] =  ConvertText.ConvertStringToArraryChar($"# -- -- -- -- -- -- -- --");
            MainInformation[5] =  ConvertText.ConvertStringToArraryChar($"| ( ХАРАКТЕРИСТИКИ )");
            MainInformation[6] =  ConvertText.ConvertStringToArraryChar($"| Защита: {player.ArmorValue}");
            MainInformation[7] =  ConvertText.ConvertStringToArraryChar($"| Атака: {player.minAttack} - {player.maxAttack}");
            MainInformation[8] =  ConvertText.ConvertStringToArraryChar($"| Сила: {player.Strength}");
            MainInformation[9] =  ConvertText.ConvertStringToArraryChar($"| Ловкость: {player.Agility}");
            MainInformation[10] = ConvertText.ConvertStringToArraryChar($"| Интеллект: {player.Intelligance}");
            MainInformation[11] = ConvertText.ConvertStringToArraryChar($"| Лидерство: {player.Leadership}");
            MainInformation[12] = ConvertText.ConvertStringToArraryChar($"| Карма: {player.Karma}");
            MainInformation[13] = ConvertText.ConvertStringToArraryChar($"| ( ЧАСТИ ТЕЛА )");
            MainInformation[14] = ConvertText.ConvertStringToArraryChar($"# Торс: {player.bodyNode.body.ToString()}");
            MainInformation[15] = ConvertText.ConvertStringToArraryChar($"# Голова: {player.bodyNode.head.ToString()}");
            MainInformation[16] = ConvertText.ConvertStringToArraryChar($"# Л.Рука: {player.bodyNode.lhand.ToString()}");
            MainInformation[17] = ConvertText.ConvertStringToArraryChar($"# П.Рука: {player.bodyNode.rhand.ToString()}");
            MainInformation[18] = ConvertText.ConvertStringToArraryChar($"# Л.Нога: {player.bodyNode.lfoot.ToString()}");
            MainInformation[19] = ConvertText.ConvertStringToArraryChar($"# П.Нога: {player.bodyNode.rfoot.ToString()}");
            #endregion
            return MainInformation;
        }

        public static void CreatePicturePlayer(NPS.Character player)
        {
            char[][] tmp = new char[5][];
            char[][] head_array = new char[5][];
            head_array[0] = ConvertText.ConvertStringToArraryChar($"     ||||||||     ");
            head_array[1] = ConvertText.ConvertStringToArraryChar($"    | '0,,0' |    ");
            head_array[2] = ConvertText.ConvertStringToArraryChar($"    |  .::.  |    ");
            head_array[3] = ConvertText.ConvertStringToArraryChar($"     |__WW__|     ");
            head_array[4] = ConvertText.ConvertStringToArraryChar($"       |  |       ");
            char[][] leftHand_array = new char[5][]; // Левая рука
            leftHand_array[0] = ConvertText.ConvertStringToArraryChar($"    oo");
            leftHand_array[1] = ConvertText.ConvertStringToArraryChar($"   oo ");
            leftHand_array[2] = ConvertText.ConvertStringToArraryChar($"  oo  ");
            leftHand_array[3] = ConvertText.ConvertStringToArraryChar($" oo   ");
            leftHand_array[4] = ConvertText.ConvertStringToArraryChar($" VV   "); 
            // Левая рука
            char[][] body_array = new char[6][]; // Тело
            body_array[0] = ConvertText.ConvertStringToArraryChar($"OTTTTO");
            body_array[1] = ConvertText.ConvertStringToArraryChar($"[HHHH]");
            body_array[2] = ConvertText.ConvertStringToArraryChar($"[HHHH]");
            body_array[3] = ConvertText.ConvertStringToArraryChar($"[HHHH]");
            body_array[4] = ConvertText.ConvertStringToArraryChar($"[HHHH]");
            body_array[5] = ConvertText.ConvertStringToArraryChar($" [WW]");
            // Тело
            char[][] rightHand_array = new char[5][];// Правая рука
            rightHand_array[0] = ConvertText.ConvertStringToArraryChar($"oo    ");
            rightHand_array[1] = ConvertText.ConvertStringToArraryChar($" oo   ");
            rightHand_array[2] = ConvertText.ConvertStringToArraryChar($"  oo  ");
            rightHand_array[3] = ConvertText.ConvertStringToArraryChar($"   oo ");
            rightHand_array[4] = ConvertText.ConvertStringToArraryChar($"   VV ");
            PrintThreeArray(leftHand_array, body_array, rightHand_array); 
            // Правая рука
            char[][] letrFoot_array = new char[6][]; // Левая нога
            letrFoot_array[0] = ConvertText.ConvertStringToArraryChar($"    ooo");
            letrFoot_array[1] = ConvertText.ConvertStringToArraryChar($"    oo ");
            letrFoot_array[2] = ConvertText.ConvertStringToArraryChar($"   oo  ");
            letrFoot_array[3] = ConvertText.ConvertStringToArraryChar($"    oo ");
            letrFoot_array[4] = ConvertText.ConvertStringToArraryChar($"    Ht ");
            letrFoot_array[5] = ConvertText.ConvertStringToArraryChar($"  oOht "); // Левая нога
            char[][] rightFoot_array = new char[6][]; // Правая нога
            rightFoot_array[0] = ConvertText.ConvertStringToArraryChar($"ooo  ");
            rightFoot_array[1] = ConvertText.ConvertStringToArraryChar($" oo  ");
            rightFoot_array[2] = ConvertText.ConvertStringToArraryChar($"  oo ");
            rightFoot_array[3] = ConvertText.ConvertStringToArraryChar($" oo  ");
            rightFoot_array[4] = ConvertText.ConvertStringToArraryChar($" tH  ");
            rightFoot_array[5] = ConvertText.ConvertStringToArraryChar($" thOo");
            PrintTwoArray(letrFoot_array, rightFoot_array);// Правая нога


        }

        public void DrawElement()
        {

        }


        public void PlayerRelationship(NPS.Character player)
        {
            
        }

        /// <summary>
        /// Изобразить на дисплее профиля двух игроков горизонтально
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public static void TwoCharacterInfo(NPS.Character player1, NPS.Character player2)
        {
            char[][] ArrayPlayer1 = CharacterAllInfo(player1);
            char[][] ArrayPlayer2 = CharacterAllInfo(player2);

            int maximum = MaxLengthInTornArray(ArrayPlayer1) + 5;
            PrintTwoArray(ArrayPlayer1, ArrayPlayer2);
        }

        /// <summary>
        /// Изобразить данные из одного рваного массива символов
        /// </summary>
        /// <param name="arr1"></param>
        private static void PrintOneArray(char[][] arr1)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < arr1.Length; i++)
            { // Работаем с подмассивом
                // Счетчик
                int counter = 0;
                for (int j = 0; j < arr1[i].Length; j++)
                { // Работа с массивом 1
                    counter++;
                    sb.Append(arr1[i][j]);
                }
                sb.Append(Environment.NewLine);
            }
            string str = sb.ToString();
            Console.Write(str);
        }
        /// <summary>
        /// Изобразить данные из двух рваных массивов символов
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        private static void PrintTwoArray(char[][] arr1, char[][] arr2)
        {
            StringBuilder sb = new StringBuilder();
            int maximum = MaxLengthInTornArray(arr1);
            // Работаем с подмассивом
            for (int i = 0; i < arr1.Length; i++)
            {               
                int counter = 0; // Счетчик
                // Работа с массивом 1
                for (int j = 0; j < arr1[i].Length; j++)
                { 
                    counter++;
                    sb.Append(arr1[i][j]);
                }
                
                // Кол-во пробелов
                for (int k = counter; k <= maximum; k++)
                    sb.Append(" ");
                sb.Append(" | ");
                counter = 0;

                for (int j = 0; j < arr2[i].Length; j++)
                { // Работа с массивом 2
                    counter++;
                    sb.Append(arr2[i][j]);
                }
                sb.Append(Environment.NewLine);
            }
            string str = sb.ToString();
            Console.Write(str);
        }
        /// <summary>
        /// Изобразить данные из трех рваных массивов символов
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <param name="arr3"></param>
        private static void PrintThreeArray(char[][] arr1, char[][] arr2, char[][] arr3)
        {
            StringBuilder sb = new StringBuilder();
            int maximum = MaxLengthInTornArray(arr1);
            // Работаем с подмассивом
            for (int i = 0; i < arr1.Length; i++)
            {
                int counter = 0; // Счетчик
                // Работа с массивом 1
                for (int j = 0; j < arr1[i].Length; j++)
                {
                    counter++;
                    sb.Append(arr1[i][j]);
                }
                // Кол-во пробелов
                counter = 0;
                for (int j = 0; j < arr2[i].Length; j++)
                { // Работа с массивом 2
                    counter++;
                    sb.Append(arr2[i][j]);
                }
                counter = 0;
                for (int j = 0; j < arr3[i].Length; j++)
                { // Работа с массивом 3
                    counter++;
                    sb.Append(arr3[i][j]);
                }
                sb.Append(Environment.NewLine);
            }
            string str = sb.ToString();
            Console.Write(str);
        }

    }
}
