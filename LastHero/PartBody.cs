using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class PartBody
    {

        public string Name; // Название 
        public float ArmorValue
        {
            get { return armorValue; }
            set {  if (armorSet != null)
                    armorValue = armorSet.armorValue + (armorSet.agilityValue * 0.25f);
                else
                    armorValue = value;
            }

        } // Броня
        private float armorValue;
        public float Status
        {
            get { return status; }
            set { status = value; if (status <= 0) { ok = false; } if (status > MaxStatus) status = MaxStatus; }
        } 
        float status; // Состояние
        public float MaxStatus; // Макс. Состояние
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
        }

        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal void Heal(float value)
        {
            if (ok)
            {
                if (Status + value < MaxStatus)
                    Status += value;
                else
                    Status = MaxStatus;
            }
            Refresh();
        }

        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        internal void Damage(float value)
        {
            if (ok)
            {
                Status -= ((value - (ArmorValue * 0.2f)) * multiplayDamage);
                Console.Write($"Получено {(value - (ArmorValue * 0.2f))} урона по {Name} ");
            }
            Refresh();
        }

        /// <summary>
        /// Обновить информацию части тела
        /// </summary>
        internal void Refresh()
        {
            if (Status <= 0)
                Status = 0;
        }

        /// <summary>
        /// Задать индивидульные значения для полей
        /// </summary>
        void CreateMultyplay()
        {
            switch (Name)
            {
                case "Body":
                    multiplayDamage = 1.0f;
                    multiplayOut = 0.42f;
                    break;
                case "Head":
                    multiplayDamage = 1.45f;
                    multiplayOut = 0.16f;
                    break;
                case "Left Hand":
                    multiplayDamage = 1.22f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Hand":
                    multiplayDamage = 1.22f;
                    multiplayOut = 0.105f;
                    break;
                case "Left Foot":
                    multiplayDamage = 1.14f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Foot":
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
            if (armorSet == null)
                return ($"{Name}: {Status} / {MaxStatus}");
            else
                return ($"{Name}: {Status} / {MaxStatus}\n{armorSet.ToString()}");
        }
    }

    [Serializable]
    public class PartBodyNode : PartBody
    {
        //public float MaxSumStatus;
        internal float SumStatus { get { return body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status; } }
        internal float SumArmor { get { return body.ArmorValue + head.ArmorValue + lhand.ArmorValue + rhand.ArmorValue + lfoot.ArmorValue + rfoot.ArmorValue; } }
        internal int SumStrength
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
        internal int SumAgility
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
        internal int SumIntelligance
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
            body = new PartBody("Body", 0);
            head = new PartBody("Head", 0);
            lhand = new PartBody("Left Hand", 0);
            rhand = new PartBody("Right Hand", 0);
            lfoot = new PartBody("Left Foot", 0);
            rfoot = new PartBody("Right Foot", 0);
        }
        public PartBodyNode(float _maxSum)
        {
            body = new PartBody("Body", _maxSum);
            head = new PartBody("Head", _maxSum);
            lhand = new PartBody("Left Hand", _maxSum);
            rhand = new PartBody("Right Hand", _maxSum);
            lfoot = new PartBody("Left Foot", _maxSum);
            rfoot = new PartBody("Right Foot", _maxSum);
            //MaxSumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }

        internal void WearArmor(Armor armor)
        {
            switch (armor.type)
            {
                case "Голова":
                    head.armorSet = armor;
                    break;
                case "Торс":
                    body.armorSet = armor;
                    break;
                case "Л.Рука":
                    lhand.armorSet = armor;
                    break;
                case "П.Рука":
                    rhand.armorSet = armor;
                    break;
                case "Л.Нога":
                    lfoot.armorSet = armor;
                    break;
                case "П.Нога":
                    rfoot.armorSet = armor;
                    break;
            }
        }
        /// <summary>
        /// Распределительное лечение
        /// </summary>
        /// <param name="value">Значение лечения</param>
        public void DistributeHealth(float value)
        {
            //lfoot.Status += value * lfoot.multiplayOut;
            if (body != null)
                body.Heal(value * body.multiplayOut);
            if (head != null)
                head.Heal(value * head.multiplayOut);
            if (lhand != null)
                lhand.Heal(value * lhand.multiplayOut);
            if (rhand != null)
                rhand.Heal(value * rhand.multiplayOut);
            if (lfoot != null)
                lfoot.Heal(value * lfoot.multiplayOut);
            if (rfoot != null)
                rfoot.Heal(value * rfoot.multiplayOut);

            Refresh();
        }
        /// <summary>
        /// Получить урон по случайной части тела от enemy
        /// </summary>
        /// <param name="enemy"></param>
        public void DamageToRandomPart(Character enemy)
        {
            float min = enemy.minAttack;
            float max = enemy.maxAttack;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(0, 6);
            switch (index)
            {
                case 0:
                    if (body.ok)
                        body.Damage(RandomDamage(min, max));
                    break;
                case 1:
                    if (head.ok)
                        head.Damage(RandomDamage(min, max));
                    break;
                case 2:
                    if (lhand.ok)
                        lhand.Damage(RandomDamage(min, max));
                    break;
                case 3:
                    if (rhand.ok)
                        rhand.Damage(RandomDamage(min, max));
                    break;
                case 4:
                    if (lfoot.ok)
                        lfoot.Damage(RandomDamage(min, max));
                    break;
                case 5:
                    if (rfoot.ok)
                        rfoot.Damage(RandomDamage(min, max));
                    break;
            }
            Console.WriteLine($"от {enemy.MainName}");
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
                    break;
                case 1:
                    if (head.ok)
                        head.Damage(RandomDamage(min, max));
                    break;
                case 2:
                    if (lhand.ok)
                        lhand.Damage(RandomDamage(min, max));
                    break;
                case 3:
                    if (rhand.ok)
                        rhand.Damage(RandomDamage(min, max));
                    break;
                case 4:
                    if (lfoot.ok)
                        lfoot.Damage(RandomDamage(min, max));
                    break;
                case 5:
                    if (rfoot.ok)
                        rfoot.Damage(RandomDamage(min, max));
                    break;
            }
        }
        /// <summary>
        /// Распределительный дамаг
        /// </summary>
        /// <param name="value">Значение урона</param>
        public void DistributedDamage(float value)
        {
            //lfoot.Status -= value * lfoot.multiplayOut;
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
                    $"- - - -\n" +
                    $"{head.ToString()}\n" +
                    $"- - - -\n" +
                    $"{body.ToString()}\n" +
                    $"- - - -\n" +
                    $"{lhand.ToString()}\n" +
                    $"- - - -\n" +
                    $"{rhand.ToString()}\n" +
                    $"- - - -\n" +
                    $"{lfoot.ToString()}\n" +
                    $"- - - -\n" +
                    $"{rfoot.ToString()}\n" +
                    $"- - - -\n");
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }
    }
}
