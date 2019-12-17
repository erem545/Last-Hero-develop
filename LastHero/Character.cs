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

        public float Accuaracy { get { return Agility * 0.2f; } } // Точность

        public float MaxEndurance { get { return (float)Math.Round(maxEnduranceValue + (Agility * 0.2f) + (Leadership * 0.5f)); } set { maxEnduranceValue = value; } }
        public float Endurance
        { 
            get { if (endurance > MaxEndurance) return MaxEndurance; else return endurance; }
            set { if (endurance > MaxEndurance) endurance = MaxEndurance; else endurance = value; }
        } 
        float endurance; // Выносливость
        float maxEnduranceValue; 

        public float MaxHealth { get { return maxHealthValue + (Strength * 0.5f) + (Leadership * 1); } set { maxHealthValue = value; } }
        public float Health {
            get 
            {
                if (health > MaxHealth)
                    return MaxHealth;
                else
                    return bodyNode.SumStatus;
            } 
            set 
            {
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
                else health = value;
            } 
        }   
        float health; // Здоровье  
        float maxHealthValue;

        public float ArmorValue { get { return (Agility + Strength) * 0.1f; } } // Общая защита

        public float AverageAttack { get { return (((minAttack + maxAttack) / 2) + ((agility + strength)*0.01f)); } } // Атака
        internal float minAttack { get { if (weaponNode == null) weaponNode = new Weapon(); return weaponNode.minDamage; } } // Мин. Атака
        internal float maxAttack { get { if (weaponNode == null) weaponNode = new Weapon(); return weaponNode.maxDamage; } } // Макс. Атака

        // Другое
        public int Level; // Уровень
        public int XP; // Опыт
        [NonSerialized]
        public Weapon weaponNode;
        [NonSerialized]
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
                    case "Броня Туловище":
                        bodyNode.body.armorSet = _item as Armor;
                        break;
                    case "Броня Левая Рука":
                        bodyNode.lhand.armorSet = _item as Armor;
                        break;
                    case "Броня Правая Рука":
                        bodyNode.rhand.armorSet = _item as Armor;
                        break;
                    case "Броня Левая Нога":
                        bodyNode.lfoot.armorSet = _item as Armor;
                        break;
                    case "Броня Правая Нога":
                        bodyNode.rfoot.armorSet = _item as Armor;
                        break;
                }
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
        public void ToDamage(float value)
        {
            if (ok)
            {
                bodyNode.DistributedDamage(value);
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
                    $"Здоровье: {MaxHealth - ((Strength * 0.5f) + (Leadership * 1))} + {((Strength * 0.5f) + (Leadership * 1))}\n" +
                    $"Выносливость: {MaxEndurance - ((Agility * 0.2f) + (Leadership * 0.5f))} + {((Agility * 0.2f) + (Leadership * 0.5f))}\n" +
                    $"Сила: {Strength}\n" +
                    $"Ловкость: {Agility}\n" +
                    $"Интеллект: {Intelligance}\n" +
                    $"Атака: {AverageAttack}\n" +
                    $"Защита: {ArmorValue}\n" +
                    $"Лидерство: {Leadership}\n");
        }
        }
    }
