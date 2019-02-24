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
        internal float Armor;

        // Состояние
        internal float Status;
        float MaxStatus;
        float PercentStatus { get { return Status * 100 / MaxStatus; } }


        float multiplayDamage; // Мультипликатор урона
        float multiplayOut; // Мультипликатор на выход

        internal bool ok; // Наличие

        public PartBody()
        {

        }
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
            Console.ForegroundColor = ConsoleColor.Green;
            if (ok)
            {
                if (Status + value < MaxStatus)
                    Status += value;
                else
                    Status = MaxStatus;
                Console.WriteLine($"Лечение на {(value)} единиц {ToString()}");
            }
            else
                Console.WriteLine($"Отсутствие части тела: {ToString()}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        public void Damage(float value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (ok)
            {
                Status += value*multiplayDamage*-1;
                if (Status < 0)
                    Status = 0;
                if (Status < 1)
                {
                    ok = false;
                    Console.WriteLine($" Уничтожено {ToString()}");
                }
                else
                    Console.WriteLine($" {value * multiplayDamage}({value}) урона по {Name}{ToString()}");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
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
            //return $"{Name} ({Status} / {MaxStatus})";
            return $"({Math.Round(PercentStatus)}% / 100%) ";
        }
    }
    class PartBodyNode : PartBody
    {
        public float MaxSumStatus;
        public float SumStatus { get { return body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status; } }

        public PartBody body;
        public PartBody head;
        public PartBody lhand;
        public PartBody rhand;
        public PartBody lfoot;
        public PartBody rfoot;

        public bool ok;

        public PartBodyNode()
        {

        }
        public PartBodyNode(float _maxSum)
        {
            ok = true;
            body = new PartBody("Body", _maxSum);
            head = new PartBody("Head", _maxSum);
            lhand = new PartBody("Left Hand", _maxSum);
            rhand = new PartBody("Right Hand", _maxSum);
            lfoot = new PartBody("Left Foot", _maxSum);
            rfoot = new PartBody("Right Foot", _maxSum);
            MaxSumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }
        void Dead()
        {
            ok = false;
            body.ok = false;
            head.ok = false;
            lhand.ok = false;
            rhand.ok = false;
            lfoot.ok = false;
            rfoot.ok = false;
            body.Refresh();
            head.Refresh();
            lhand.Refresh();
            rhand.Refresh();
            lfoot.Refresh();
            rfoot.Refresh();
        }

        /// <summary>
        /// Случайный урон (НЕТ связи с характеристиками)
        /// </summary>
        /// <returns></returns>
        float RandomDamage()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(1, 3);
        }

        /// <summary>
        /// Урон по случайной части тела
        /// </summary>
        public void AttackToRandomPart()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(0,5);
            switch (index)
            {
                case 0:
                    head.Damage(RandomDamage());
                    break;
                case 1:
                    head.Damage(RandomDamage());
                    break;
                case 2:
                    lhand.Damage(RandomDamage());
                    break;
                case 3:
                    rhand.Damage(RandomDamage());
                    break;
                case 4:
                    lfoot.Damage(RandomDamage());
                    break;
                case 5:
                    rfoot.Damage(RandomDamage());
                    break;
            }
        }

        /// <summary>
        /// Показать все детали
        /// </summary>
        internal void ShowDetals()
        {
            Console.WriteLine();
            Console.WriteLine($"Торс:   {body.ToString()}\tБроня: {body.Armor}");
            Console.WriteLine($"Голова: {head.ToString()}\tБроня: {head.Armor}");
            Console.WriteLine($"Л.Рука: {lhand.ToString()}\tБроня: {lhand.Armor}");
            Console.WriteLine($"П.Рука: {rhand.ToString()}\tБроня: {rhand.Armor}");
            Console.WriteLine($"Л.Нога: {lfoot.ToString()}\tБроня: {lfoot.Armor}");
            Console.WriteLine($"П.Нога: {rfoot.ToString()}\tБроня: {rfoot.Armor}");
            Console.WriteLine();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
