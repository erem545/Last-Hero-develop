using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Character
    {
        // Наименование
        public string MainName;

        // Тип объекта
        readonly string TypeObj = "Персонаж";
        // Здоровье
        internal float MaxHealth;
        internal float Health;
        internal float PercentHealth { get { return Health * 100 / MaxHealth; } }

        // Узлы
        PartBodyNode body;

        public Character()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
        }
        public Character(string _name, float _maxHealth)
        {
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        void Refresh()
        {
            Health = body.SumStatus;
        }
        /// <summary>
        /// Показать всю информацию информацию
        /// </summary>
        public virtual void  ShowAllInfo()
        {
            Console.WriteLine(ToString());
        }


        public override string ToString()
        {
            return (
                $"{MainName}\n" +
                $"Здоровье: {Health} / {MaxHealth} ({Math.Round(PercentHealth)}% / 100%)\n");
        }
    }
}
