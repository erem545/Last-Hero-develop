using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Player
    {
        string name;
        float health = (head.status + lhand.status + rhand.status + lfoot.status + rfoot.status + body.status); // Показатель здоровья
        float armor;
        float endurance; // Показатель выносливости

        static PartBody body = new PartBody();
        static PartBody head  = new PartBody();
        static PartBody lhand = new PartBody();
        static PartBody rhand = new PartBody();
        static PartBody lfoot = new PartBody();
        static PartBody rfoot = new PartBody();

        public Player(string _name)
        {
            name = _name;
            health = 0;
            endurance = 0;
            armor = 0;
            CreateBodyPart();
        }
        void CreateBodyPart()
        {
            body = new PartBody("Тело", null, true);
            head  = new PartBody("Голова", body, true);
            lhand = new PartBody("Л.Рука", body, true);
            rhand = new PartBody("п.Рука", body, true);
            lfoot = new PartBody("Л.Нога", body, true);
            rfoot = new PartBody("П.Нога", body, true);
            health = (head.status + lhand.status + rhand.status + lfoot.status + rfoot.status + body.status);
            Console.WriteLine(this.ToString());
            Console.WriteLine(body.ToString() + head.ToString() + lhand.ToString() + rhand.ToString() + lfoot.ToString() + rfoot.ToString());
        }
        public override string ToString()
        {
            return $"*{name}*\nЗдоровье:\t{health}\nВыносливость:\t{endurance}";
        }
    }
}
