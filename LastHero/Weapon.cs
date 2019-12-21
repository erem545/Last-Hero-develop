using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class Weapon : Item
    {
        public float minDamage;
        public float maxDamage;
        public Weapon() : base()
        {
            Name = "Кулак";
            State = 100;

            minDamage = 1;
            maxDamage = 2;
            type = "Оружие";
        }

        public Weapon(string _name, int _quality, int _level, int _state) : base(_name, _quality, _level, _state)
        {
            minDamage = 1;
            maxDamage = 2;
            type = "Оружие";
        }

        public Weapon(string _name, int _quality, int _level, int _state, float _minDamage, float _maxDamage, string _type) : base(_name, _quality, _level, _state)
        {
            minDamage = _minDamage;
            maxDamage = _maxDamage;
            type = _type;
        }

        public override string ToString()
        {
            if (this == null)
                return "";
            return (
                $"{Name} ({minDamage}-{maxDamage})\nУровень:{Level} Качество:{Quality} Состояние:{State}%");

        }
    }
}
