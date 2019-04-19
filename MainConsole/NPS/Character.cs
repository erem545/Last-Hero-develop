using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    class Character 
    {
        public bool         isAdmin; // Является админом
        bool                ok; // Существование
        public string       MainName; // Наименование

        // Выносливость
        protected float     MaxEndurance; // Макс. выносливость
        float               Endurance; // Выносливость
        float               PercentEndurance { get { return Endurance * 100 / MaxEndurance; } }
        protected float     EnduranceRegenPercent; // Реген. выносливости

        // Здоровье
        protected float     MaxHealth; // Макс. Здоровье
        float               Health; // Здоровье
        float               PercentHealth { get { return Health * 100 / MaxHealth; } }
        protected float     HealthRegenPercent;// Реген. Здоровья

        // Защита
        float               Armor; // Общая защита

        // Атака
        float               Attack { get { return (minAttack + maxAttack) / 2; } }// Атака
        protected float     minAttack; // Мин. Атака
        protected float     maxAttack; // Макс. Атака

        // Другое
        float               Level; // Уровень
        int                 XP; // Опыт
        
        public PartBodyNode bodyNode; // Узел для частей тела

        public Character()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            EnduranceRegenPercent = 0;
        }
        public Character(string _name, float _maxHealth, float _maxEndurance, bool isAdm)
        {  
            Level = 1;
            XP = 0;
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
            ok = true;
            bodyNode = new PartBodyNode(_maxHealth);
            Armor = bodyNode.SumArmor;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            isAdmin = isAdm;
        }
        /// <summary>
        /// Атаковать противника person
        /// </summary>
        /// <param name="person">Противник</param>
        internal void ToAttack(Character person)
        {
            if ( (ok) && (person.ok) )
            {                           
                string text = $"\n{MainName} нанес урон {person.MainName}";
                Console.Write(text);
                bodyNode.RezisitArmor = 0.04f;
                person.bodyNode.AttackToRandomPart(minAttack, maxAttack);
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
        /// Нанести общее повреждение
        /// </summary>
        /// <param name="value">Значение</param>
        internal void ToDamage(float value)
        {
            Console.WriteLine($"Получен урон: {value} ед.");
            if (ok)
            {
                if (value > Health)
                    Kill();
                bodyNode.DistributedDamage(value);
                Refresh();
            }
        }
        /// <summary>
        /// Восстановить здоровье распределительно
        /// </summary>
        /// <param name="value">Значение</param>
        internal void ToHeal(float value)
        {
            Console.WriteLine($"Восстановление здоровья: {value} ед.");
            if (ok)
            {
                bodyNode.DistributeHealth(value);
                Refresh();
            }
        }
        /// <summary>
        /// Защищаться от атаки person (Увеличение сопротивления урона за ед. защиты)
        /// </summary>
        /// <param name="person">Нападающий</param>
        internal void ToDefend(Character person)
        {
            if ((ok) && (person.ok))
            {
                bodyNode.RezisitArmor = 0.04f;                         
                person.ToAttack(this);
                bodyNode.RezisitArmor = 0.02f;

                Refresh();
                person.Refresh();

                //// Убийство противника
                //if (ok == false)
                //    Console.Write($"{MainName} прикончил {person.MainName} | {XP += 20}");           
            }
        }
        /// <summary>
        /// Убить персонажа
        /// </summary>
        public void Kill()
        {
            Health = 0;
            Level = 0;
            XP = 0;
            bodyNode.Dead();
            Endurance = 0;
        }
        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if ((bodyNode.head.ok == false) || (bodyNode.body.ok == false))
                ok = false;

            //Endurance += MaxEndurance * EnduranceRegenPercent;
            //if (Endurance > MaxEndurance)
            //    Endurance = MaxEndurance;
            //else if (Endurance < 0)
            //    Endurance = 0;
            // Снижение урона при отсутствии части тела

            if ((bodyNode.lhand.ok == false) || (bodyNode.rhand.ok == false))
            {
                minAttack /= 2;
                maxAttack /= 2;
                if((bodyNode.lhand.ok == false) && (bodyNode.rhand.ok == false))
                {
                    minAttack = 0;
                    maxAttack = 0;
                }
            }
            if (Health > 0)
            {
                if (ok)
                {
                    Console.WriteLine($"Регенерация здоровья: {MaxHealth * HealthRegenPercent} ед.");
                    bodyNode.DistributeHealth(MaxHealth * HealthRegenPercent);
                    Health = bodyNode.SumStatus;
                    if (Health > MaxHealth)
                        Health = MaxHealth;

                    Armor = bodyNode.SumArmor;
                }
                else
                    Kill();
            }
            else
                Kill();
        }
        /// <summary>
        /// Показать всю информацию информацию
        /// </summary>
        public void ShowAllInfo()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n___________________________________");
            Console.WriteLine("               Профиль");
            Console.WriteLine(ToString());
            bodyNode.ShowDetals();
            Console.WriteLine("___________________________________");
        }
        public override string ToString()
        {
            return (
                $"\n{MainName} ({Level} ур.) {XP} xp.\n" +
                $"Здоровье:\t{Health} / {MaxHealth} ({PercentHealth}% / 100%)\n" +           
                $"Выносливость:\t{Endurance} / {MaxEndurance} ({PercentEndurance}% / 100%)\n" +
                $"Защита:\t{Armor}\n" +
                $"Атака:\t{minAttack} - {maxAttack}\n" +            
                $"Раса:\t{this.GetType()}\n" +
                $"Регенерация здоровья:\t{HealthRegenPercent * MaxHealth} (за ход) / {HealthRegenPercent * 100} %\n" +
                $"Регенерация выносливости:\t{EnduranceRegenPercent * MaxEndurance} (за ход) / {EnduranceRegenPercent * 100} %\n");
        }
    }
}
