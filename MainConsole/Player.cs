using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Player : PartBody
    {
        // Наименование
        public static string mainName;

        // Макс. здоровье
        internal float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        float maxHealth;

        // Здоровье
        internal float Health { get { return health; } set { health = body.GetStatus + head.GetStatus + lhand.GetStatus + rhand.GetStatus +  lfoot.GetStatus + rfoot.GetStatus; } }
        float health;
        float PercentHealth { get { return health * 100 / maxHealth; } }

        float armor; // Показатель брони
        float endurance; // Показатель выносливости

        static PartBody body  = new PartBody();
        static PartBody head  = new PartBody();
        static PartBody lhand = new PartBody();
        static PartBody rhand = new PartBody();
        static PartBody lfoot = new PartBody();
        static PartBody rfoot = new PartBody();
        

        public Player(string _name)
        {
            CreateBodyPart();
            MaxHealth = Health;
            mainName = _name;
            endurance = 100;
            armor = 0;
            
        }
        /// <summary>
        /// Создание частей тела
        /// </summary>
        void CreateBodyPart()
        {
            body  = new PartBody("Body",       null, true, 150);
            head  = new PartBody("Head",       body, true, 150);
            lhand = new PartBody("Left Hand",  body, true, 150);
            rhand = new PartBody("Right Hand", body, true, 150);
            lfoot = new PartBody("Left Foot",  body, true, 150);
            rfoot = new PartBody("Right Foot", body, true, 150);
            Health += 1;
        }
        public override string ToString()
        {
            
            Health += 1;
            return ($"*{mainName}*\nЗдоровье:\t{Health} / {MaxHealth}\t({PercentHealth}% / 100%)\nВыносливость:\t{endurance}\nБроня:\t\t{armor}");
        }
        public void run()
        {
            body.PartBodyManager("Damage",20);

            Console.WriteLine(body.ToString());
            Console.WriteLine(head.ToString());
            Console.WriteLine(lhand.ToString());
            Console.WriteLine(rhand.ToString());
            Console.WriteLine(lfoot.ToString());
            Console.WriteLine(rfoot.ToString());
            Console.WriteLine(this.ToString());
        }
    }
}
