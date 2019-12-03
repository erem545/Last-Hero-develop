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
        internal bool       ok; // Существование
        public string       MainName; // Наименование

        // Характеристики
        internal int Strength
        { 

            get { if (bodyNode == null) return 0; return strength + bodyNode.SumStrength; }
            set { strength = value; }
        }
        int strength; // Сила
        internal int Agility
        {
            get { if (bodyNode == null) return 0; return agility + bodyNode.SumAgility; }
            set { agility = value; }
        }
        int agility; // Ловкость    
        internal int Intelligance
        {
            get { if (bodyNode == null) return 0; return intelligance + bodyNode.SumIntelligance; }
            set { intelligance = value; }
        }
        int intelligance; // Интеллект

        internal int Communication;
        internal int Karma;
        internal int Leadership;

        // Выносливость
        internal float MaxEndurance; // Макс. выносливость
        internal float Endurance; // Выносливость
        internal float      PercentEndurance { get { return Endurance * 100 / MaxEndurance; } } // Процент от максимального запаса
        protected float     EnduranceRegenPercent; // Реген. выносливости
        // Здоровье
        internal float MaxHealth { get { return maxHealthValue + (Strength * 0.5f) + (Leadership * 1); } set { maxHealthValue = value; } } // Макс. Здоровье
        float maxHealthValue;
        internal float Health; // Здоровье      
        internal  float     PercentHealth { get { return Health * 100 / MaxHealth; } } // Процент от максимального запаса
        internal float     HealthRegenPercent;// Реген. Здоровья

        // Защита
        internal float ArmorValue; // Общая защита

        // Атака
        float Attack { get { return (minAttack + maxAttack) / 2; } }// Атака
        internal float minAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.minDamage; } } // Мин. Атака
        internal float maxAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.maxDamage; } } // Макс. Атака

        // Другое
        internal int Level; // Уровень
        internal int XP; // Опыт
        internal Weapon weaponNode;
        
        public PartBodyNode bodyNode; // Узел для частей тела



        public Character()
        {
            CreateStartСharacteristics();
            MainName = null;
            MaxHealth = 0;
            Health = MaxHealth;
            HealthRegenPercent = 0;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            EnduranceRegenPercent = 0;
            weaponNode = null;
            Strength = 0;
            Agility = 0;
            Intelligance = 0;
            Leadership = 0;
            Karma = 0;

        }
        public Character(string _name, float _health, float _maxEndurance, bool isAdm, int s, int a, int i)
        {
            Strength = s;
            Agility = a;
            Intelligance = i;
            Leadership = 0;
            Karma = 0;
            Level = 1;
            XP = 1;
            MainName = _name;

            HealthRegenPercent = 5;
            ok = true;
            bodyNode = new PartBodyNode(_health);
            ArmorValue = bodyNode.SumArmor;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            EnduranceRegenPercent = 5;
            isAdmin = isAdm;
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
                //Console.WriteLine($"{DateTime.Now.ToString()} | {MainName} нанес урон по {node.Name} {person.MainName}\n");
                Refresh();
                person.Refresh();
                // Убийство противника
                if (person.ok == false)
                {
                    XP += ((person.Level + 1) * 10);
                    //Console.WriteLine($"{DateTime.Now.ToString()} | {MainName} прикончил {person.MainName}. Получено опыта: {(person.Level + 1) * 10} xp");
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
            HealthRegenPercent = 0;
            EnduranceRegenPercent = 0;
            ArmorValue = 0;
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

            Endurance += ((MaxEndurance * EnduranceRegenPercent) / 100);
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
                    ArmorValue = bodyNode.SumArmor;
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
