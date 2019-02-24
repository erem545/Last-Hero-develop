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
        public PartBodyNode bodyNode;

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
        /// Атаковать противника person
        /// </summary>
        /// <param name="person"></param>
        public void ToAttack(Character person)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string text = $"{MainName}: Нанес урон {person.MainName}";
            Console.Write(text);
            bodyNode.AttackToRandomPart();
            Refresh();
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        public void Refresh()
        {
            Health = bodyNode.SumStatus;
        }
        /// <summary>
        /// Показать всю информацию информацию
        /// </summary>
        public void ShowAllInfo()
        {
            Console.WriteLine("___________________________________");
            Console.WriteLine(ToString());
            bodyNode.ShowDetals();
        }


        public override string ToString()
        {
            return (
                $"{MainName}\n" +
                $"Здоровье:\t{Health} / {MaxHealth} ({Math.Round(PercentHealth)}% / 100%)\n");
        }
    }
}
