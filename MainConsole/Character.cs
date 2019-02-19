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



        // Здоровье
        float MaxHealth { get; set; }
        float Health { get; set; }
        float PercentHealth { get { return Health * 100 / MaxHealth; } }
        
        // Броня
        float Armor { get { return armor; } set { armor = value; } }
        float armor;


        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Character()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
            Armor = 0;
        }
        public Character(string _name, float _maxHealth, float _armor)
        {
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
            Armor = _armor;
        }

        /// <summary>
        /// Вывести информацию
        /// </summary>
        public virtual void ShowCharacteristic()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return (
                $"*{MainName}*\n" +
                $"Здоровье:\t{Health} / {MaxHealth}\t({PercentHealth}% / 100%)\n" +
                $"Броня:\t\t{Armor}");
        }
    }
}
