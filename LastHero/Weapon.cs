using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastHero
{
    [Serializable]
    class Weapon : Item
    {

        internal float minDamage;
        internal float maxDamage;
        internal string type;

        public Weapon(string _name, int _quality, int _level, int _state) : base(_name, _quality, _level, _state)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;

            minDamage = 1;
            maxDamage = 2;
            type = "Ближний бой";
        }

        public Weapon(string _name, int _quality, int _level, int _state, float _minDamage, float _maxDamage, string _type) : base(_name, _quality, _level, _state)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;

            minDamage = _minDamage;
            maxDamage = _maxDamage;
            type = _type;
        }

        public override string ToString()
        {
            if (this == null)
                return "";
            return (
                $"{Name} ({Level} ур., {Quality}*) {minDamage} - {maxDamage}"
                );

        }
    }
}
