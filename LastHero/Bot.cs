using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastHero
{
    [Serializable]
    public class Bot : Character
    {
        public Bot()
        {
            MainName = null;
            MaxHealth = 0;
            Health = MaxHealth;
            Level = 0;
            XP = 0;
            Endurance = 0;
            MaxEndurance = 0;
            Strength = 0;
            Agility = 0;
            Intelligance = 0;
        }
        public Bot(string _name, float _health, float _endurance, int s, int a, int i)
        {
            MainName = _name;
            MaxHealth = _health;
            Health = MaxHealth;
            Level = 1;
            XP = 1;
            Endurance = _endurance;
            MaxEndurance = _endurance;
            Strength = s;
            Agility = a;
            Intelligance = i;
        }
    }
}