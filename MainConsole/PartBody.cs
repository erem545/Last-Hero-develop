using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class PartBody
    {
        public string Name; // Название 

        // Броня
        float Armor;

        // Состояние
        internal float Status;
        float MaxStatus;
        float PercentStatus { get { return Status * 100 / MaxStatus; } }


        float multiplayDamage; // Мультипликатор урона
        float multiplayOut; // Мультипликатор на выход

        internal bool ok; // Наличие


        /// <summary>
        /// При создании новой части, для успешной работы, параметр _name 
        /// задается одним из названий: Body, Head, Left Hand, Right Hand, Left Foot, Right Foot  
        /// </summary>                                                                                      
        /// <param name="_name"></param>                                                               
        /// <param name="_parent"></param>                                                           
        /// <param name="_ok"></param>                                                                 
        /// <param name="_maxStatus"></param>                                                         
        public PartBody(string _name, float _maxStatus)
        {
            ok = true;
            // Обязательный порядок - сначала имя, потом объявление костант
            Name = _name;
            CreateMultyplay();
            MaxStatus = _maxStatus * multiplayOut;
            Status = _maxStatus * multiplayOut;                       
        }
        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        public void Heal(float value)
        {
            if (ok)
            {
                if (Status + value < MaxStatus)
                    Status += value;
                else
                    Status = MaxStatus;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Лечение на {(value)} единиц {ToString()}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Отсутствие части тела: {ToString()}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        public void Damage(float value)
        {
            if (ok)
            {
                Status += value*multiplayDamage*-1;
                if (Status < 0)
                    Status = 0;
                if (Status < 1)
                {
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Уничтожено {ToString()}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Получено {value * multiplayDamage}({value}) урона по {ToString()}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }     
        }
        /// <summary>
        /// Обновить информацию части тела
        /// </summary>
        public void Refresh()
        {
            if (!ok)
                Status = 0;
        }
        /// <summary>
        /// Задать индивидульные значения для полей
        /// </summary>
        void CreateMultyplay()
        {
            switch (Name)
            {
                case "Body":
                    multiplayDamage = 1.0f;
                    multiplayOut = 0.42f;
                    break;
                case "Head":
                    multiplayDamage = 2.0f;
                    multiplayOut = 0.16f;
                    break;
                case "Left Hand":
                    multiplayDamage = 1.3f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Hand":
                    multiplayDamage = 1.3f;
                    multiplayOut = 0.105f;
                    break;
                case "Left Foot":
                    multiplayDamage = 1.4f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Foot":
                    multiplayDamage = 1.4f;
                    multiplayOut = 0.105f;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Status} / {MaxStatus})";
            //return $"{Name} ({Math.Round(PercentStatus)}% / 100%)";
        }
    }
    
}
