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
        public bool ok; // Существование
        public string MainName; // Наименование

        // Характеристики
        public int Strength
        {
            get { 
                if (bodyNode == null) 
                    return 0; 

                return strength; }
            set
            {
                if (bodyNode != null)
                {
                    strength = bodyNode.SumStrength + value;
                }
                else
                    strength = value; }
        }
        public int Agility
        {
            get { 
                if (bodyNode == null) 
                    return 0;

                return agility; }
            set
            {
                if (bodyNode != null)
                {
                    agility = value + bodyNode.SumAgility;
                }else
                agility = value; }
        }
        public int Intelligance
        {
            get { 
                if (bodyNode == null) 
                    return 0;

                return intelligance; }
            set
            {
                if (bodyNode != null)
                {
                    intelligance = value+ bodyNode.SumIntelligance;
                }else
                intelligance = value; }
        }
        int strength; // Сила
        int agility; // Ловкость    
        int intelligance; // Интеллект
        public int Leadership; // Лидерство

        public float Accuaracy { get { return Agility * 0.2f; } } // Точность

        //
        public float MaxEndurance { get { return maxEnduranceValue ; } set { maxEnduranceValue = value ; } } //+ (agility * 0.2f) + (Leadership * 0.5f)
        public float Endurance
        { 
            get { if (bodyNode !=null) return endurance; else return endurance; }
            set { if (endurance > MaxEndurance) endurance = MaxEndurance; else endurance = value; }
        } 
        float endurance; // Выносливость
        float maxEnduranceValue;

        //
        public float MaxHealth { 
            get { 
                return maxHealthValue ; 
            } 
            set
            {
                maxHealthValue = value; 
            } 
        }
        public float Health {
            get 
            {
                if (bodyNode != null)
                    return bodyNode.SumStatus; //  + (strength * 0.5f) + (Leadership * 1)
                else
                    return health;
            } 
            set 
            {
                health = value; 
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                    ok = true;
                }
                else if (health < 0)
                {
                    health = 0;
                    ok = false;
                    Kill();
                }
            } 
        }   
        float health; // Здоровье  
        float maxHealthValue;

        public float ArmorValue { get { return bodyNode.SumArmor; } } // Общая защита

        internal float AverageAttack { get { return ((minAttack + maxAttack) / 2); } } // Атака
        internal float minAttack { get { return (weaponNode.minDamage + ((agility + strength) * 0.01f)); } } // Мин. Атака
        internal float maxAttack { get { return (weaponNode.maxDamage + ((agility + strength) * 0.01f)); } } // Макс. Атака

        // Другое
        public int Level 
        {
            get
            {
                return level;
            }
            set
            {
                if (XP > (level * XP))
                    level++;
                else
                    level = value;
            }
        } // Уровень
        int level;
        public int XP; // Опыт
        public Weapon weaponNode;
        public PartBodyNode bodyNode; // Узел для частей тела

        public Character()
        {
            MainName = null;          
            Level = 0;
            XP = 0;
            weaponNode = null;
            Strength = 0;
            ok = true;
            Agility = 0;
            Intelligance = 0;
            Leadership = 0;
            Endurance = MaxEndurance = 0;
            Health = 0;
            MaxHealth = Health;
        }
        public Character(string _name, float _health, float _maxEndurance, int s, int a, int i)
        {
            Strength = s;
            Agility = a;
            Intelligance = i;
            Leadership = 0;
            Level = 1;
            XP = 1;
            MainName = _name;
            ok = true;
            bodyNode = new PartBodyNode(_health);
            weaponNode = new Weapon();
            MaxEndurance = _maxEndurance;
            Endurance = MaxEndurance;

            Health = _health;
            MaxHealth = Health;
        }

        public void ToTake(Item _item)
        {
            if (_item is Weapon)
            {
                weaponNode = _item as Weapon;
            }
            if (_item is Armor)
            {
                switch (_item.type)
                {
                    case "Шлем":
                        bodyNode.head.armorSet = _item as Armor;
                        break;
                    case "Туловище":
                        bodyNode.body.armorSet = _item as Armor;
                        break;
                    case "Левая Рука":
                        bodyNode.lhand.armorSet = _item as Armor;
                        break;
                    case "Правая Рука":
                        bodyNode.rhand.armorSet = _item as Armor;
                        break;
                    case "Левая Нога":
                        bodyNode.lfoot.armorSet = _item as Armor;
                        break;
                    case "Правая Нога":
                        bodyNode.rfoot.armorSet = _item as Armor;
                        break;
                }
            }
        }

        /// <summary>
        /// Нанести общее повреждение
        /// </summary>
        /// <param name="value">Значение</param>
        public void ToDamage(float value)
        {
            if (ok)
            {
                 bodyNode.DamageToRandomPart(value);
            }
        }

        public void ToAttack(Character enemy)
        {
            if (enemy.ok)
            {              
                this.Endurance -= 10;
                if (ProbabilityClass.ChanceToHit(this))
                    enemy.bodyNode.DamageToRandomPart(this);
                else
                    Console.WriteLine("Промах!");
            }
        }


        /// <summary>
        /// Восстановить здоровье распределительно
        /// </summary>
        /// <param name="value">Значение</param>
        public void ToHeal(float value)
        {
            if (ok)
            {
                bodyNode.DistributeHealth(value);
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
            Endurance = MaxEndurance;
            Health = MaxHealth;
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {

            // Отсутствие головы или тела - мгновенная смерть
            if ((bodyNode.head.ok == false) || (bodyNode.body.ok == false))
                Kill();
                       
            if (Endurance > MaxEndurance)
                Endurance = MaxEndurance;
            else if (Endurance < MaxEndurance)
                Endurance += agility * 0.001f;

            if (Health > 0)
            {
                if (ok)
                {
                    ToHeal(Strength * 0.001f);
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
                    $"{MainName} ({Level} ур.) {XP} xp.\n" +
                    $"Здоровье: {Health} / {MaxHealth}\n" +
                    $"Выносливость: {Endurance} / {MaxEndurance}\n" +
                    $"Сила: {Strength} (+ {bodyNode.SumStrength})\n" +
                    $"Ловкость: {Agility} (+ {bodyNode.SumAgility})\n" +
                    $"Интеллект: {Intelligance} (+ {bodyNode.SumIntelligance})\n" +
                    $"Ср.Урон: {AverageAttack}\n" +
                    $"Точность: {Accuaracy}\n" +
                    $"Защита: {ArmorValue}\n" +
                    $"Лидерство: {Leadership}\n" +
                    $"{weaponNode.ToString()}\n" +
                    $"{bodyNode.ToString()}");
        }
        }
    }
