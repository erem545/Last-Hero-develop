using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class Character
    {
        public bool ok; // Существование
        public string MainName; // Наименование
        public bool paused; // Остановка внутриигрового процесса

        // Характеристики
        public int Strength
        {
            get 
            { 
                if (bodyNode == null) 
                    return 0; 
                return bodyNode.SumStrength + StrengthStats; 
            }
        }
        public int Agility
        {
            get 
            { 
                if (bodyNode == null) 
                    return 0;
                return AgilityStats + bodyNode.SumAgility; 
            }
        }
        public int Intelligance
        {
            get 
            {              
                if (bodyNode == null) 
                    return 0;
                return IntelliganceStats + bodyNode.SumIntelligance; 
            }
        }

        public int StrengthStats; // Сила
        public int AgilityStats; // Ловкость    
        public int IntelliganceStats; // Интеллект
        public int Leadership; // Лидерство

        public float Accuaracy { get { return Agility * 0.2f; } } // Точность

        //
        public float MaxEndurance { get { return maxEnduranceValue + Agility * 0.15f + Leadership * 0.5f; } set { maxEnduranceValue = value; } } // 
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
                //if (bodyNode != null)
                //    return bodyNode.SumMaxStatus;
                //else
                return maxHealthValue + Strength * 0.25f + Leadership * 1; 
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
                    return bodyNode.SumStatus;
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
        internal float minAttack { get { return (weaponNode.minDamage + ((AgilityStats + StrengthStats) * 0.01f)); } } // Мин. Атака
        internal float maxAttack { get { return (weaponNode.maxDamage + ((AgilityStats + StrengthStats) * 0.01f)); } } // Макс. Атака

        // Другое
        public int Level 
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                if (level % 10 == 0)
                    Leadership=level/10;
            }
        } // Уровень
        int level;
        public int XP
        {
            get
            {
                if (xp > (Level * 1000))
                {
                    LevelUP();
                };
                return xp;
            }
            set
            {
                xp = value;
            }
        }// Опыт
        int xp;
        public Weapon weaponNode;
        public PartBodyNode bodyNode; // Узел для частей тела
        public string Info;

        public Character()
        {
            MainName = null;          
            Level = 0;
            XP = 0;
            Info = "";
            weaponNode = null;            
            ok = true;
            StrengthStats = 0;
            AgilityStats = 0;
            IntelliganceStats = 0;
            Leadership = 0;
            Endurance = MaxEndurance = 0;
            Health = 0;
            MaxHealth = Health;
            paused = false;
        }
        public Character(string _name, float _health, float _maxEndurance, int s, int a, int i, string info)
        {
            StrengthStats = s;
            AgilityStats = a;
            IntelliganceStats = i;
            Leadership = 0;
            Level = 1;
            Info = info;
            XP = 0;
            MainName = _name;
            ok = true; 
            MaxHealth = _health + Strength * 0.5f;
            bodyNode = new PartBodyNode(MaxHealth);
            weaponNode = new Weapon();
            MaxEndurance = _maxEndurance;
            Endurance = MaxEndurance;
            UpdateAll();
            paused = false;
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
                 bodyNode.DistributedDamage(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns>Возвращает кол-во урона (-1 - промах, -2 - нет энергии, -3 - Убийство противника)</returns>
        public float ToAttack(Character enemy)
        {
            if (enemy.ok)
            {              
                if (Endurance >= 5) 
                { 
                    this.Endurance -= 5;
                    if (ProbabilityClass.ChanceToHit(this))
                    {
                        float d = enemy.bodyNode.DamageToRandomPart(this);
                        if (enemy.ok)
                            return d;
                        else
                        {
                            Console.WriteLine("Убийство!");
                            return -3;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Промах!");
                        return -1;
                    }
                }
                else
                {
                    Console.WriteLine("Устал!");
                    return -2;
                }
            }
            return -1;
        }

        /// <summary>
        /// Восстановить здоровье распределительно
        /// </summary>
        /// <param name="value">Значение</param>
        public void ToHeal(float value)
        {
            bodyNode.DistributeHealth(value);
            
        }


        public void ChangeStrength(int value)
        {
            StrengthStats += value;
        }
        public void ChangeAgility(int value)
        {
            AgilityStats += value;
        }
        public void ChangeIntelligance(int value)
        {
            IntelliganceStats += value;
        }
        public int CalcReward()
        {
            return ((Strength * Agility * Intelligance) / (Strength + Agility + Intelligance));
        }

        public void GetReward(int value)
        {
            XP += value;
        }

        public void LevelUP()
        {
            Level++;
            StrengthStats+=3;
            AgilityStats += 3;
            IntelliganceStats += 3;
            UpdateAll();
        }

        public void UpdateAll()
        {
            bodyNode.CreateMultyplay();
            bodyNode.UpdateMaxStatusAllParts(MaxHealth);
            Endurance = MaxEndurance;
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

        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {
            bodyNode.Refresh();
            // Отсутствие головы или тела - мгновенная смерть
            if ((bodyNode.head.ok == false) || (bodyNode.body.ok == false))
                Kill();

            if (!paused)
            {

                if (Endurance > MaxEndurance)
                    Endurance = MaxEndurance;
                else if (Endurance < MaxEndurance)
                    Endurance += Agility * 0.0006f;

                if (Health > 0)
                {
                    if (ok)
                    {
                        ToHeal(Strength * 0.00001f);
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
                    $"\n{weaponNode.ToString()}\n" +
                    $"\n{bodyNode.ToString()}");
        }
        }
    }
