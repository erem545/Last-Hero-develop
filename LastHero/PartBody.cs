using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class PartBody
    {

        public string Name; // Название 
        public float ArmorValue
        {
            get { if (armorSet != null)
                    return armorSet.armorValue;
            else
                    return armorValue; }
            set {  
                    armorValue = value;// + (armorSet.agilityValue * 0.25f)
            }

        } // Броня
        private float armorValue;
        public float Status
        {
            get { return status; }
            set { status = value; if (status <= 0) { status = 0; } if (status > MaxStatus) status = MaxStatus; }
        } 
        float status; // Состояние
        public float MaxStatus
        {
            get { return maxstatus; }
            set 
            { 
                if (armorSet == null)
                    maxstatus = value;
                else
                    maxstatus = value;
            }
        }
        float maxstatus; // Макс. Состояние
        bool paused; // Пауза внутриигровых процессов

        internal float multiplayDamage; // Мультипликатор урона
        internal float multiplayOut; // Мультипликатор части тела
        internal bool ok; // Наличие
        public Armor armorSet;
        public PartBody()
        {
            ok = true;
            Name = "";
            armorSet = new Armor();
            MaxStatus = 0;
            paused = false;
        }

        /// <summary>
        /// При создании новой части, для успешной работы, параметр _name 
        /// задается одним из названий: Body, Head, Left Hand, Right Hand, Left Foot, Right Foot  
        /// </summary>                                                                                      
        /// <param name="_name"></param>                                                               
        /// <param name="_parent"></param>                                                           
        /// <param name="_ok"></param>                                                                 
        /// <param name="_maxStatus"></param>                                                         
        public PartBody(string _name, float _maxStatus)
        {
            ok = true;
            // Обязательный порядок - сначала имя, потом объявление костант
            Name = _name;
            CreateMultyplay();
            MaxStatus = (float)Math.Round(_maxStatus * multiplayOut, 3); // ... * Процент от общего кол-ва здоровья
            Status = MaxStatus;
            armorSet = new Armor();
            paused = false;
        }

        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal void Heal(float value)
        {
            if (!paused)
                Status += value;
        }

        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal float Damage(float value)
        {
            if (ok)
            {
                Status -= ((value - (ArmorValue * 0.2f)));
                Console.Write($"Получено {((value - (ArmorValue * 0.2f)))} ({value}) урона по {Name} ");
            }
            Refresh();
            return ((value - (ArmorValue * 0.2f)));
        }

        /// <summary>
        /// Обновить информацию части тела
        /// </summary>
        internal void Refresh()
        {
            if (Status <= 0)
            {
                Status = 0;
                ok = false;
                paused = true;
            }

        }

        /// <summary>
        /// Задать индивидульные значения для полей
        /// </summary>
        internal void CreateMultyplay()
        {
            switch (Name)
            {
                case "Туловище":
                    multiplayDamage = 1.0f;
                    multiplayOut = 0.42f;
                    break;
                case "Голова":
                    multiplayDamage = 1.45f;
                    multiplayOut = 0.16f;
                    break;
                case "Левая Рука":
                    multiplayDamage = 1.22f;
                    multiplayOut = 0.105f;
                    break;
                case "Правая Рука":
                    multiplayDamage = 1.22f;
                    multiplayOut = 0.105f;
                    break;
                case "Левая Нога":
                    multiplayDamage = 1.14f;
                    multiplayOut = 0.105f;
                    break;
                case "Правая Нога":
                    multiplayDamage = 1.14f;
                    multiplayOut = 0.105f;
                    break;
            }
        }
        /// <summary>
        /// Нанести урон в промежутке от min до max
        /// </summary>
        /// <param name="min">Минимально допустимый предел урона</param>
        /// <param name="max">Максимально допустимый предел урона</param>
        /// <returns></returns>
        internal float RandomDamage(float min, float max)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            float value = (float)Math.Round(rnd.Next((int)min, (int)max) + (float)rnd.NextDouble(), 3);
            Damage(value);
            return value;
        }
        public override string ToString()
        {
            switch (Name)
            {
                case "Голова":      return ($"{Name}:          {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                case "Туловище":    return ($"{Name}:     {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                case "Левая Рука":  return ($"{Name}:   {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                case "Правая Рука": return ($"{Name}: {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                case "Левая Нога":  return ($"{Name}:   {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                case "Правая Нога": return ($"{Name}: {Math.Round(Status, 2)} {Math.Round(Status * 100 / MaxStatus)}%\t({MaxStatus})");
                default:
                    return ($"{Name}: {Math.Round(Status, 1)} {Math.Round(Status * 100 / MaxStatus)}% ({MaxStatus})");

            }
            //if (armorSet == null)
            //    return ($"{Name}: {Status} / {MaxStatus}");
            //else
            //    return ($"{Name}: {Status} / {MaxStatus}\n{armorSet.ToString()}");
            
        }
    }

    [Serializable]
    public class PartBodyNode : PartBody
    {
        //public float MaxSumStatus;
        internal float SumStatus { get { return body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status; } }
        internal float SumMaxStatus { get { return body.MaxStatus + head.MaxStatus + lhand.MaxStatus + rhand.MaxStatus + lfoot.MaxStatus + rfoot.MaxStatus; } }
        internal float SumArmor { get { return body.ArmorValue + head.ArmorValue + lhand.ArmorValue + rhand.ArmorValue + lfoot.ArmorValue + rfoot.ArmorValue; } }
        public int SumStrength
        {
            get
            {
                return
                      body.armorSet.strengthValue +
                      head.armorSet.strengthValue +
                      lhand.armorSet.strengthValue +
                      rhand.armorSet.strengthValue +
                      lfoot.armorSet.strengthValue +
                      rfoot.armorSet.strengthValue;
            }
        }
        public int SumAgility
        {
            get
            {
                return
                      body.armorSet.agilityValue +
                      head.armorSet.agilityValue +
                      lhand.armorSet.agilityValue +
                      rhand.armorSet.agilityValue +
                      lfoot.armorSet.agilityValue +
                      rfoot.armorSet.agilityValue;
            }
        }
        public int SumIntelligance
        {
            get
            {
                return
                      body.armorSet.intelliganceValue +
                      head.armorSet.intelliganceValue +
                      lhand.armorSet.intelliganceValue +
                      rhand.armorSet.intelliganceValue +
                      lfoot.armorSet.intelliganceValue +
                      rfoot.armorSet.intelliganceValue;
            }
        }

        public PartBody body;
        public PartBody head;
        public PartBody lhand;
        public PartBody rhand;
        public PartBody lfoot;
        public PartBody rfoot;


        public PartBodyNode()
        {
            body = new PartBody("Туловище", 0);
            head = new PartBody("Голова", 0);
            lhand = new PartBody("Левая Рука", 0);
            rhand = new PartBody("Правая Рука", 0);
            lfoot = new PartBody("Левая Нога", 0);
            rfoot = new PartBody("Правая Нога", 0);
        }
        public PartBodyNode(float _maxSum)
        {
            body = new PartBody("Туловище", _maxSum);
            head = new PartBody("Голова", _maxSum);
            lhand = new PartBody("Левая Рука", _maxSum);
            rhand = new PartBody("Правая Рука", _maxSum);
            lfoot = new PartBody("Левая Нога", _maxSum);
            rfoot = new PartBody("Правая Нога", _maxSum);
            //MaxSumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }

        public void UpdateMaxStatusAllParts(float value)
        {
            body.CreateMultyplay();
            head.CreateMultyplay();
            lhand.CreateMultyplay();
            rhand.CreateMultyplay();
            lfoot.CreateMultyplay();
            rfoot.CreateMultyplay();

            body.MaxStatus = value * body.multiplayOut;
            head.MaxStatus = value * head.multiplayOut;
            lhand.MaxStatus = value * lhand.multiplayOut;
            rhand.MaxStatus = value * rhand.multiplayOut;
            lfoot.MaxStatus = value * lfoot.multiplayOut;
            rfoot.MaxStatus = value * rfoot.multiplayOut;
            DistributeHealth(value);

        }
        /// <summary>
        /// Распределительное лечение
        /// </summary>
        /// <param name="value">Значение лечения</param>
        public void DistributeHealth(float value)
        {
            CreateMultyplay();
            //lfoot.Status += value * lfoot.multiplayOut;
            if (body != null)
                body.Heal(value);
            if (head != null)
                head.Heal(value);
            if (lhand != null)
                lhand.Heal(value);
            if (rhand != null)
                rhand.Heal(value);
            if (lfoot != null)
                lfoot.Heal(value);
            if (rfoot != null)
                rfoot.Heal(value);
        }
        /// <summary>
        /// Получить урон по случайной части тела от enemy
        /// </summary>
        /// <param name="enemy"></param>
        public float DamageToRandomPart(Character enemy)
        {
            float min = enemy.minAttack;
            float max = enemy.maxAttack;
            float dmg = 0;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(0, 6);
            switch (index)
            {
                case 0:
                    if (body.ok)
                        dmg = body.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 1:
                    if (head.ok)
                        dmg = head.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 2:
                    if (lhand.ok)
                        dmg = lhand.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 3:
                    if (rhand.ok)
                        dmg = rhand.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 4:
                    if (lfoot.ok)
                        dmg = lfoot.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 5:
                    if (rfoot.ok)
                        dmg = rfoot.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
            }
            Console.WriteLine($"от {enemy.MainName}");
            return dmg;
        }

        public void DamageToRandomPart(float damage)
        {
            float min = damage;
            float max = damage;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(0, 6);
            switch (index)
            {
                case 0:
                    if (body.ok)
                        body.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 1:
                    if (head.ok)
                        head.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 2:
                    if (lhand.ok)
                        lhand.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 3:
                    if (rhand.ok)
                        rhand.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 4:
                    if (lfoot.ok)
                        lfoot.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
                case 5:
                    if (rfoot.ok)
                        rfoot.Damage(RandomDamage(min, max));
                    else
                        DamageToRandomPart(min);
                    break;
            }
        }
        /// <summary>
        /// Распределительный дамаг
        /// </summary>
        /// <param name="value">Значение урона</param>
        public void DistributedDamage(float value)
        {
            body.Damage(value * body.multiplayOut);
            head.Damage(value * head.multiplayOut);
            lhand.Damage(value * lhand.multiplayOut);
            rhand.Damage(value * rhand.multiplayOut);
            lfoot.Damage(value * lfoot.multiplayOut);
            rfoot.Damage(value * rfoot.multiplayOut);
            Refresh();
        }
        /// <summary>
        /// Смерть
        /// </summary>
        internal void Dead()
        {
            ok = false;
            body.ok = false;
            head.ok = false;
            lhand.ok = false;
            rhand.ok = false;
            lfoot.ok = false;
            rfoot.ok = false;
            body.Refresh();
            head.Refresh();
            lhand.Refresh();
            rhand.Refresh();
            lfoot.Refresh();
            rfoot.Refresh();
        }

        public override string ToString()
        {
            try
            {
                return (
                    $"{head.ToString()}\n" +
                    $"{body.ToString()}\n" +
                    $"{lhand.ToString()}\n" +
                    $"{rhand.ToString()}\n" +
                    $"{lfoot.ToString()}\n" +
                    $"{rfoot.ToString()}");
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }
    }
}
