using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LastHero
{
    [Serializable]
    public class Human : Character
    {
        public Human() : base()
        {
            MainName = null;
            MaxHealth = 0;
            Health = 0;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            Strength = 0;
            Agility = 0;
            Intelligance = 0;
        }
        public Human(string _name, float _maxHealth, float _maxEndurance, int s, int a, int i) :
            base(_name, _maxHealth, _maxEndurance, s, a, i)
        {
            MainName = _name;
            MaxHealth = _maxHealth;
            Health = _maxHealth;
            Level = 1;
            XP = 1;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            Strength = s;
            Agility = a;
            Intelligance = i;
        }
    }
}
