using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    public class Character
    {
        public bool isAdmin; // Является админом
        public bool ok; // Существование
        public string MainName; // Наименование

        // Характеристики
        public int Strength
        {
            get { if (bodyNode == null) return 0; return strength + bodyNode.SumStrength; }
            set { strength = value; }
        }
        int strength; // Сила
        public int Agility
        {
            get { if (bodyNode == null) return 0; return agility + bodyNode.SumAgility; }
            set { agility = value; }
        }
        int agility; // Ловкость    
        public int Intelligance
        {
            get { if (bodyNode == null) return 0; return intelligance + bodyNode.SumIntelligance; }
            set { intelligance = value; }
        }
        int intelligance; // Интеллект

        public int Communication;
        public int Karma;
        public int Leadership;
        public float Accuaracy { get { return Agility * 0.2f; } }

        // Выносливость
        public float MaxEndurance { get { return maxEnduranceValue + (Agility * 0.2f) + (Leadership * 0.5f); } set { maxHealthValue = value; } } // Макс. выносливость
        float maxEnduranceValue;
        internal float Endurance; // Выносливость
        internal float PercentEndurance { get { return Endurance * 100 / MaxEndurance; } } // Процент от максимального запаса

        // Здоровье
        public float MaxHealth { get { return maxHealthValue + (Strength * 0.5f) + (Leadership * 1); } set { maxHealthValue = value; } } // Макс. Здоровье
        float maxHealthValue;
        internal float Health; // Здоровье      
        internal  float PercentHealth { get { return Health * 100 / MaxHealth; } } // Процент от максимального запаса
        internal float HealthRegenPercent { get { return ((MaxHealth / Strength) * 0.05f); } } // Реген. Здоровья

        // Защита
        internal float ArmorValue { get { return bodyNode.ArmorValue + Agility * 0.2f; } } // Общая защита

        // Атака
        float AverageAttack { get { return (minAttack + maxAttack) / 2; } }// Атака
        internal float minAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.minDamage; } } // Мин. Атака
        internal float maxAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.maxDamage; } } // Макс. Атака

        // Другое
        public int Level; // Уровень
        public int XP; // Опыт
        internal Weapon weaponNode;
        internal PartBodyNode bodyNode; // Узел для частей тела

        public Character()
        {
            CreateStartСharacteristics();
            MainName = null;
            MaxHealth = 0;
            Health = MaxHealth;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            weaponNode = null;
            Strength = 0;
            Agility = 0;
            Intelligance = 0;
            Leadership = 0;
            Karma = 0;

        }
        public Character(string _name, float _health, float _maxEndurance, int s, int a, int i)
        {
            Strength = s;
            Agility = a;
            Intelligance = i;
            Leadership = 0;
            Karma = 0;
            Level = 1;
            XP = 1;
            MainName = _name;
            ok = true;
            bodyNode = new PartBodyNode(_health);
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            weaponNode = null;
            MaxHealth = _health;
            Health = MaxHealth;
        }

        internal void ToTake(Item _item)
        {
            if (_item is Weapon)
            {
                weaponNode = _item as Weapon;
            }
        }
         
        public void DisplayMessage(string str)
        {
            Console.WriteLine(str);
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
                Refresh();
                person.Refresh();
                // Убийство противника
                if (person.ok == false)
                {
                    XP += ((person.Level + 1) * 10);
                    Console.WriteLine($"{DateTime.Now.ToString()} | {MainName} прикончил {person.MainName}. Получено опыта: {(person.Level + 1) * 10} xp");
                }
            }
        }
        /// <summary>
        /// Нанести общее повреждение
        /// </summary>
        /// <param name="value">Значение</param>
        internal void ToDamage(float value)
        {
            //Console.WriteLine($"Получен урон: {value} ед.");
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
            //Console.WriteLine($"Восстановление здоровья: {value} ед.");
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
        }
        
        private void CreateStartСharacteristics(int s, int a, int i, int c, int k, int l) 
        {
            Strength = s;
            Agility = a;
            Intelligance = i;
            Communication = c;
            Karma = k;
            Leadership = l;
        }
        private void CreateStartСharacteristics()
        {
            Strength = 1;
            Agility = 1;
            Intelligance = 1;
            Communication = 0;
            Karma = 0;
            Leadership = 0;
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

            if (Endurance > MaxEndurance)
                Endurance = MaxEndurance;
            else if (Endurance < 0)
                Endurance = 0;

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
                    $"\nОбщее:\n" +
                    $"{MainName} ({Level} ур.) {XP} xp.\n" +
                    $"Здоровье:\t{Health} / {MaxHealth} ({PercentHealth}%)\n" +
                    $"Выносливость:\t{Endurance} / {MaxEndurance} ({PercentEndurance}%)\n" +
                    $"Защита:\t{ArmorValue}\n" +
                    $"Атака:\t{minAttack} - {maxAttack}\n" +
                    $"Оружие:\t{weaponNode.ToString()}\n" +
                    $" | Характеристики:\n" +
                    $" | Сила:\t{Strength}\n" +
                    $" | Ловкость:\t{Agility}\n" +
                    $" | Интеллект:\t{Intelligance}\n" +
                    $" | Лидерство:\t{Leadership}\n" +
                    $" | Карма:\t{Karma}\n");

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
