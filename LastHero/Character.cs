using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;

namespace LastHero
{
    [Serializable]
    public class Character
    {
        internal bool ok; // Существование
        public string MainName; // Наименование

        // Характеристики
        public int Strength
        {
            get { if (bodyNode == null) return 0; return strength; }
            set { strength = value; }
        }
        public int Agility
        {
            get { if (bodyNode == null) return 0; return agility; }
            set { agility = value; }
        }
        public int Intelligance
        {
            get { if (bodyNode == null) return 0; return intelligance; }
            set { intelligance = value; }
        }
        int strength; // Сила
        int agility; // Ловкость    
        int intelligance; // Интеллект

        public int Communication; // Общение
        public int Karma; // Карма
        public int Leadership; // Лидерство

        public float Accuaracy { get { return Agility * 0.2f; } }// Точность

        public float MaxEndurance { get { return maxEnduranceValue + (Agility * 0.2f) + (Leadership * 0.5f); } set { maxEnduranceValue = value; } }
        public float Endurance; // Выносливость
        float maxEnduranceValue; 

        public float MaxHealth { get { return maxHealthValue + (Strength * 0.5f) + (Leadership * 1); } set { maxHealthValue = value; } }
        public float Health; // Здоровье    
        float maxHealthValue;

        public float ArmorValue { get { return Agility * 0.2f; } } // Общая защита

        public float AverageAttack { get { return (((minAttack + maxAttack) / 2) + ((agility + strength)*0.01f)); } } // Атака
        internal float minAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.minDamage; } } // Мин. Атака
        internal float maxAttack { get { if (weaponNode == null) weaponNode = new Weapon("Кулаки", 1, 1, 1); return weaponNode.maxDamage; } } // Макс. Атака

        // Другое
        public int Level; // Уровень
        public int XP; // Опыт
        internal Weapon weaponNode;
        internal PartBodyNode bodyNode; // Узел для частей тела

        public Character()
        {
            MainName = null;          
            Level = 0;
            XP = 0;
            weaponNode = null;
            Strength = 0;
            Agility = 0;
            Intelligance = 0;
            Leadership = 0;
            Karma = 0;
            Endurance = MaxEndurance = 0;
            Health = MaxHealth = 0;
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
            weaponNode = null;
            MaxEndurance = _maxEndurance;
            Endurance = MaxEndurance;
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
                }
            }
        }
        /// <summary>
        /// Нанести общее повреждение
        /// </summary>
        /// <param name="value">Значение</param>
        internal void ToDamage(float value)
        {
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
            if (ok)
            {
                bodyNode.DistributeHealth(value);
                Refresh();
            }

        }

        /// <summary>
        /// Убить персонажа
        /// </summary>
        public void Kill()
        {
            ok = false;
            Health = 0;
            Level = 0;
            XP = 0;
            bodyNode.Dead();
            Endurance = 0;
        }

        public void UpdateAllValues()
        {
            bodyNode = new PartBodyNode();
            Endurance = MaxEndurance;
            Health = MaxHealth;
            Strength = strength;
            Agility = agility;
            Intelligance = intelligance;
            
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            // Отсутствие головы или тела - мгновенная смерть
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
                    Health = bodyNode.SumStatus;
                    if (Health > MaxHealth)
                        Health = MaxHealth;
                }
                else
                {
                    Kill();
                }
            }
            else
            {
                Kill();
            }
        }

        public override string ToString()
        {
            return (
                    $"\nОбщее:\n" +
                    $"{MainName} ({Level} ур.) {XP} xp.\n" +
                    $"Здоровье:\t{Health} / {MaxHealth} ({(Health * 100 / MaxHealth)}%)\n" +
                    $"Выносливость:\t{Endurance} / {MaxEndurance} ({(Endurance * 100 / MaxEndurance)}%)\n" +
                    $"Защита:\t{ArmorValue}\n" +
                    $"Атака:\t{minAttack} - {maxAttack}\n" +
                    $"Оружие:\t{weaponNode.ToString()}\n" +
                    $" | Характеристики:\n" +
                    $" | Сила:\t{Strength}\n" +
                    $" | Ловкость:\t{Agility}\n" +
                    $" | Интеллект:\t{Intelligance}\n" +
                    $" | Лидерство:\t{Leadership}\n" +
                    $" | Карма:\t{Karma}\n");
        }
        }
    }
