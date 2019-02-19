using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Player : Character
    {
        // Наименование
        new public string MainName;
        
        float Armor { get; set; }

        public PartBody body  = new PartBody();
        public PartBody head  = new PartBody();
        public PartBody lhand = new PartBody();
        public PartBody rhand = new PartBody();
        public PartBody lfoot = new PartBody();
        public PartBody rfoot = new PartBody();

        // Здоровье
        float MaxHealth { get; set; }
        float Health { get { return health; } set { health = SumStatusPart(); if (health > MaxHealth) health = MaxHealth; } }
        float health;
        float PercentHealth { get { return Health * 100 / MaxHealth; } }

        //Выносливость
        float MaxEndurance { get; set; }
        float Endurance { get; set; }
        float PercentEndurance { get { return Endurance * 100 / MaxEndurance; } }

        public Player() : base()
        {
            Endurance = 0;
            MaxEndurance = 0;
        }
        public Player(string _name, float _maxHealth, float _armor, float _maxEndurance) : base (_name, _maxHealth,_armor)
        {
            MainName = _name;
            MaxHealth = _maxHealth;

            body  = new PartBody("Body",       true, MaxHealth);
            head  = new PartBody("Head",       true, MaxHealth);
            lhand = new PartBody("Left Hand",  true, MaxHealth);
            rhand = new PartBody("Right Hand", true, MaxHealth);
            lfoot = new PartBody("Left Foot",  true, MaxHealth);
            rfoot = new PartBody("Right Foot", true, MaxHealth);
            Health = SumStatusPart();
            Armor = _armor;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
        }
        public float SumStatusPart()
        {
            return body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }

        /// <summary>
        /// Показать характеристики
        /// </summary>
        public override void ShowCharacteristic()
        {
            Health = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
            Console.WriteLine(ToString());
            Console.WriteLine(body.ToString());
            Console.WriteLine(head.ToString());
            Console.WriteLine(lhand.ToString());
            Console.WriteLine(rhand.ToString());
            Console.WriteLine(lfoot.ToString());
            Console.WriteLine(rfoot.ToString());
        }

        public override string ToString()
        {       
            return (
                $"*{MainName}*\n" +
                $"Здоровье:\t{Health} / {MaxHealth}\t({PercentHealth}% / 100%)\n" +
                $"Выносливость:\t{Endurance} / {MaxEndurance}\t({PercentEndurance}% / 100%)\n" +
                $"Броня:\t\t{Armor}");
        }
    }
}
