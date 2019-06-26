using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary;

namespace MainConsole.NPS
{
    class Character 
    {
        public bool         isAdmin; // Является админом
        internal bool       ok; // Существование
        public string       MainName; // Наименование

        // Выносливость
        protected float     MaxEndurance; // Макс. выносливость
        float               Endurance; // Выносливость
        internal float      PercentEndurance { get { return Endurance * 100 / MaxEndurance; } }
        protected float     EnduranceRegenPercent; // Реген. выносливости

        // Здоровье
        protected float     MaxHealth; // Макс. Здоровье
        float               Health; // Здоровье
        internal  float     PercentHealth { get { return Health * 100 / MaxHealth; } }
        protected float     HealthRegenPercent;// Реген. Здоровья

        // Защита
        float               Armor; // Общая защита

        // Атака
        float Attack { get { return (minAttack + maxAttack) / 2; } }// Атака
        internal float     minAttack; // Мин. Атака
        internal float     maxAttack; // Макс. Атака

        // Другое
        int                Level; // Уровень
        int                XP; // Опыт
        
        public PartBodyNode bodyNode; // Узел для частей тела

        public Character()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
            HealthRegenPercent = 0;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            EnduranceRegenPercent = 0;
        }
        public Character(string _name, float _maxHealth, float _maxEndurance, bool isAdm)
        {  
            Level = 1;
            XP = 1;
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
            HealthRegenPercent = 5;
            ok = true;
            bodyNode = new PartBodyNode(_maxHealth);
            Armor = bodyNode.SumArmor;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            EnduranceRegenPercent = 5;
            isAdmin = isAdm;
        }
        /// <summary>
        /// Атаковать противника person
        /// </summary>
        /// <param name="person">Противник</param>
        internal void ToAttack(Character person)
        {
            

        }

        internal void ToAttack(Character person, PartBody node)
        {
            if ((ok) && (person.ok))
            {
                node.RandomDamage(this.minAttack, this.maxAttack);
                Console.Write($"{DateTime.Now.ToString()} | {MainName} нанес урон по {node.Name} {person.MainName}\n");
                Refresh();
                person.Refresh();
                // Убийство противника
                if (person.ok == false)
                {
                    XP += ((person.Level + 1) * 10);
                    Console.Write($"{DateTime.Now.ToString()} | {MainName} прикончил {person.MainName}. Получено опыта: {(person.Level + 1) * 10} xp");
                }
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
            if (isAdmin)
                return;
            ok = false;
            Health = 0;
            Level = 0;
            XP = 0;
            bodyNode.Dead();
            Endurance = 0;
            HealthRegenPercent = 0;
            EnduranceRegenPercent = 0;
            Armor = 0;
        }
        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            // Отсутствие головы или тела - мгновенная смерть
            if (!isAdmin) 
                if ((bodyNode.head.ok == false) || (bodyNode.body.ok == false))
                    Kill();

            Endurance += ((MaxEndurance * EnduranceRegenPercent) / 100);
            if (Endurance > MaxEndurance)
                Endurance = MaxEndurance;
            else if (Endurance < 0)
                Endurance = 0;

            // Снижение урона 
            if (!isAdmin)
                if ((bodyNode.lhand.ok == false) || (bodyNode.rhand.ok == false)) // При отсутствии одной из рук
                { 
                    minAttack /= 2;
                    maxAttack /= 2;
                    if((bodyNode.lhand.ok == false) && (bodyNode.rhand.ok == false))// При отсутствии обеих рук
                    {
                        minAttack = 0;
                        maxAttack = 0;
                    }
                }

            // Если здоровье положительное
            if (Health > 0)
            {
                if (ok)
                {
                    // Console.WriteLine($"{DateTime.Now.ToString()} : {this.MainName} | Регенерация здоровья: {MaxHealth * HealthRegenPercent} ед.");
                    bodyNode.DistributeHealth(MaxHealth * HealthRegenPercent);
                    Health = bodyNode.SumStatus;
                    if (Health > MaxHealth)
                        Health = MaxHealth;
                    Armor = bodyNode.SumArmor;
                }
                else
                {
                    if (isAdmin)
                        return;
                    else
                        Kill();
                }
            }
            else
            {
                if (isAdmin)
                    return;
                else
                    Kill();
            }
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
                    $"Здоровье:\t{Health} / {MaxHealth} ({PercentHealth}%)\n" +
                    $"Выносливость:\t{Endurance} / {MaxEndurance} ({PercentEndurance}%)\n" +
                    $"Защита:\t{Armor}\n" +
                    $"Атака:\t{minAttack} - {maxAttack}\n");

            //return (
            //    $"\n{MainName} ({Level} ур.) {XP} xp.\n" +
            //    $"Здоровье:\t{Health} / {MaxHealth} ({PercentHealth}%)\n" +           
            //    $"Выносливость:\t{Endurance} / {MaxEndurance} ({PercentEndurance}%)\n" +
            //    $"Защита:\t{Armor}\n" +
            //    $"Атака:\t{minAttack} - {maxAttack}\n" +            
            //    $"Раса:\t{this.GetType()}\n" +
            //    $"Рег. здоровья:\t{HealthRegenPercent * MaxHealth} (за ход) / {HealthRegenPercent * 100} %\n" +
            //    $"Рег. выносливости:\t{EnduranceRegenPercent * MaxEndurance} (за ход) / {EnduranceRegenPercent * 100} %\n");
        }
    }
}
