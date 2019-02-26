using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    public enum Race
    {
        Humans,
        Trolls,
        Ogres,
        Dead,
        Neutral,
        Animals,
        Gnomes,
        Elves
    }

    class Character
    {
        // Наименование
        public string MainName;

        // Тип объекта
        readonly string TypeObj = "Персонаж";

        // Здоровье
        internal float MaxHealth;
        internal float Health;
        internal float PercentHealth { get { return Health * 100 / MaxHealth; } }
        internal float HealthRegenPercent;

        // Общая защита
        internal float Armor;

        // Атака
        internal float Attack;
        internal float AttackPercent;

        // Уровень
        internal float Level;
        internal int XP;

        internal bool ok;

        // Узлы
        public PartBodyNode bodyNode;

        public Character()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
            Level = 0;
            XP = 0;
        }
        public Character(string _name, float _maxHealth)
        {
            
            Level = 1;
            XP = 20;
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
            ok = true;
            bodyNode = new PartBodyNode(_maxHealth);
            Armor = bodyNode.SumArmor;
        }

        /// <summary>
        /// Атаковать противника
        /// </summary>
        /// <param name="person">Противник</param>
        public virtual void ToAttack(Character person)
        {
            if ( (ok) && (person.ok) )
            {                           
                string text = $"\n{MainName} нанес урон {person.MainName}";
                Console.Write(text);
                bodyNode.RezisitArmor = 0.04f;
                person.bodyNode.AttackToRandomPart();
                bodyNode.RezisitArmor = 0.02f;   
                Console.ForegroundColor = ConsoleColor.Gray;

                Refresh();
                person.Refresh();

                // Убийство противника
                if (person.ok == false)
                    Console.Write($"{MainName} прикончил {person.MainName} | {XP += 20} xp");
            }

        }

        /// <summary>
        /// Защищаться от атаки (Увеличение сопротивления урона за ед. защиты)
        /// </summary>
        /// <param name="person">Нападающий</param>
        public virtual void ToDefend(Character person)
        {
            if ((ok) && (person.ok))
            {
                bodyNode.RezisitArmor = 0.04f;                         
                person.ToAttack(this);
                bodyNode.RezisitArmor = 0.02f;

                Refresh();
                person.Refresh();

                // Убийство противника
                if (ok == false)
                    Console.Write($"{MainName} прикончил {person.MainName} | {XP += 20}");           
            }
        }

        /// <summary>
        /// Убить персонажа
        /// </summary>
        public virtual void Kill()
        {
            Health = 0;
            Level = 0;
            XP = 0;
            bodyNode.Dead();
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        public virtual void Refresh()
        {
            
            if ((bodyNode.head.ok == false) || (bodyNode.body.ok == false))
                ok = false;
            if (ok)
            {
                bodyNode.DistributeHealth(MaxHealth * HealthRegenPercent);
                Health = bodyNode.SumStatus;
                if (Health > MaxHealth)
                    Health = MaxHealth;
                Armor = bodyNode.SumArmor;
            }
            else
                Kill();
        }
        /// <summary>
        /// Показать всю информацию информацию
        /// </summary>
        public void ShowAllInfo()
        {
            Console.WriteLine("\n___________________________________");
            Console.WriteLine("               Профиль\n");
            Console.WriteLine(ToString());
            bodyNode.ShowDetals();
            Console.WriteLine("___________________________________\n");
        }


        public override string ToString()
        {
            return (
                $"{MainName} ({Level} ур.) {XP} xp.\n" +
                $"Здоровье:\t{Health} / {MaxHealth} ({PercentHealth}% / 100%)\n");
        }
    }
}
