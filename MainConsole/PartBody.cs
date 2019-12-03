using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MainConsole
{
    class PartBody
    { 

        public string  Name; // Название 
        internal float ArmorValue 
        {
            get { return armorValue; }
            private set { armorValue = armorSet.agilityValue; }
        
        } // Броня
        private float armorValue;
        internal float Status; // Состояние
        internal float MaxStatus; // Макс. Состояние
        internal float PercentStatus { get { return Status * 100 / MaxStatus; } }
        internal float multiplayDamage; // Мультипликатор урона
        internal float multiplayOut; // Мультипликатор части тела
        internal bool  ok; // Наличие
        internal float RezisitArmor = 0.02f; // Сопротивление урону от брони
        internal Armor armorSet;
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
            armorSet = new Armor();
        }

        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal void Heal(float value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (ok)
            {
                if (Status + value < MaxStatus)
                    Status += value;
                else
                    Status = MaxStatus;
                //Console.WriteLine($"Лечение на {(value)} единиц {ToString()}");
            }
            else
            {
                //Console.WriteLine($"Отсутствие части тела: {ToString()}");
            }
            Refresh();
        }

        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal void Damage(float value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (ok)
            {
                Status += ( (value * multiplayDamage) * (1 - (RezisitArmor * ArmorValue)) ) * -1;
                if (Status < 0)
                    Status = 0;
                Console.WriteLine($"{DateTime.Now.ToString()} | {((value * multiplayDamage) * (1 - (RezisitArmor * ArmorValue)))}({value}) урона по {ToString()}");
                if (Status < 1)
                {
                    ok = false;
                    Console.WriteLine($"{DateTime.Now.ToString()} | Уничтожено {ToString()}");
                }
            }
            Refresh();
        }

        /// <summary>
        /// Обновить информацию части тела
        /// </summary>
        internal void Refresh()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (!ok)
                Status = 0;
            else
                if (Status > MaxStatus)
                Status = MaxStatus;            
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
        /// <summary>
        /// Нанести урон в промежутке от min до max
        /// </summary>
        /// <param name="min">Минимально допустимый предел урона</param>
        /// <param name="max">Максимально допустимый предел урона</param>
        /// <returns></returns>
        internal float RandomDamage(float min, float max)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            float value = rnd.Next((int)min, (int)max);
            Damage(value);
            return value;
        }
        public override string ToString()
        {
            Refresh();
            if (PercentStatus <= 30)
                Console.ForegroundColor = ConsoleColor.Red;

            if ( (PercentStatus > 30) && (PercentStatus < 75) )
                Console.ForegroundColor = ConsoleColor.Yellow;

            if (PercentStatus >= 75)
                Console.ForegroundColor = ConsoleColor.Green;

            return $"{Name} ({Status} / {MaxStatus})";
            //return $"{Name} ({Math.Round(PercentStatus)}% / 100%) ";
        }
    }
    class PartBodyNode : PartBody
    {
        public float MaxSumStatus;
        public float SumStatus { get { return body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status; } }
        public float SumArmor { get { return body.ArmorValue + head.ArmorValue + lhand.ArmorValue + rhand.ArmorValue + lfoot.ArmorValue + rfoot.ArmorValue; } }
        public int SumStrength { 
            get 
            { return 
                    body.armorSet.strengthValue  + 
                    head.armorSet.strengthValue  + 
                    lhand.armorSet.strengthValue + 
                    rhand.armorSet.strengthValue + 
                    lfoot.armorSet.strengthValue + 
                    rfoot.armorSet.strengthValue; 
            } 
        }
        public int SumAgility
        {
            get
            {
                return
                      body.armorSet.agilityValue +
                      head.armorSet.agilityValue +
                      lhand.armorSet.agilityValue +
                      rhand.armorSet.agilityValue +
                      lfoot.armorSet.agilityValue +
                      rfoot.armorSet.agilityValue;
            }
        }
        public int SumIntelligance
        {
            get
            {
                return
                      body.armorSet.intelliganceValue +
                      head.armorSet.intelliganceValue +
                      lhand.armorSet.intelliganceValue +
                      rhand.armorSet.intelliganceValue +
                      lfoot.armorSet.intelliganceValue +
                      rfoot.armorSet.intelliganceValue;
            }
        }

        public PartBody body;
        public PartBody head;
        public PartBody lhand;
        public PartBody rhand;
        public PartBody lfoot;
        public PartBody rfoot;
        

        public PartBodyNode()
        {

        }
        public PartBodyNode(float _maxSum)
        {
            body = new PartBody("Body", _maxSum);
            head = new PartBody("Head", _maxSum);
            lhand = new PartBody("Left Hand", _maxSum);
            rhand = new PartBody("Right Hand", _maxSum);
            lfoot = new PartBody("Left Foot", _maxSum);
            rfoot = new PartBody("Right Foot", _maxSum);
            MaxSumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }

        public void WearArmor(Armor armor)
        {
            switch (armor.type)
            {
                case "Голова":
                    head.armorSet = armor;
                    break;
                case "Торс":
                    body.armorSet = armor;
                    break;
                case "Л.Рука":
                    lhand.armorSet = armor;
                    break;
                case "П.Рука":
                    rhand.armorSet = armor;
                    break;
                case "Л.Нога":
                    lfoot.armorSet = armor;
                    break;
                case "П.Нога":
                    rfoot.armorSet = armor;
                    break;
            }
        }
        /// <summary>
        /// Распределительное лечение
        /// </summary>
        /// <param name="value">Значение лечения</param>
        public void DistributeHealth(float value)
        {
            //lfoot.Status += value * lfoot.multiplayOut;
            body.Heal(value * body.multiplayOut);
            head.Heal(value * head.multiplayOut);
            lhand.Heal(value * lhand.multiplayOut);
            rhand.Heal(value * rhand.multiplayOut);
            lfoot.Heal(value * lfoot.multiplayOut);
            rfoot.Heal(value * rfoot.multiplayOut);
            Refresh();
        }
        /// <summary>
        /// Распределительный дамаг
        /// </summary>
        /// <param name="value">Значение урона</param>
        public void DistributedDamage(float value)
        {
            //lfoot.Status -= value * lfoot.multiplayOut;
            body.Damage(value * body.multiplayOut);
            head.Damage(value * head.multiplayOut);
            lhand.Damage(value * lhand.multiplayOut);
            rhand.Damage(value * rhand.multiplayOut);
            lfoot.Damage(value * lfoot.multiplayOut);
            rfoot.Damage(value * rfoot.multiplayOut);
            Refresh();
        }
        /// <summary>
        /// Смерть
        /// </summary>
        internal void Dead()
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
        /// Урон по случайной части тела
        /// </summary>
        public void AttackToRandomPart(float min, float max)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(0, 6);
            switch (index)
            {
                case 0:
                    if (body.ok)
                        body.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
                case 1:
                    if (head.ok)
                        head.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
                case 2:
                    if (lhand.ok)
                        lhand.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
                case 3:
                    if (rhand.ok)
                        rhand.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
                case 4:
                    if (lfoot.ok)
                        lfoot.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
                case 5:
                    if (rfoot.ok)
                        rfoot.Damage(RandomDamage(min, max));
                    else
                        AttackToRandomPart(min, max);
                    break;
            }
        }

        /// <summary>
        /// Показать все детали
        /// </summary>
        internal void ShowDetals()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"\nТорс:  "); Console.Write($"\t{body.ToString() }"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {body.armorSet.ToString()} " + ArmorValue);
              Console.Write($"Голова:"); Console.Write($"\t{head.ToString() }"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {head.armorSet.ToString()} " + ArmorValue);
              Console.Write($"Л.Рука:"); Console.Write($"\t{lhand.ToString()}"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {lhand.armorSet.ToString()} " + ArmorValue);
              Console.Write($"П.Рука:"); Console.Write($"\t{rhand.ToString()}"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {rhand.armorSet.ToString()} " + ArmorValue);
              Console.Write($"Л.Нога:"); Console.Write($"\t{lfoot.ToString()}"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {lfoot.armorSet.ToString()} " + ArmorValue);
              Console.Write($"П.Нога:"); Console.Write($"\t{rfoot.ToString()}"); Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine($"\tБроня: {rfoot.armorSet.ToString()} " + ArmorValue);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
