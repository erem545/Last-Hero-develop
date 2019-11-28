using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Armor : Item
    {
        internal float strengthValue;
        internal float agilityValue;
        internal float intelliganceValue;
        internal string type;
        public Armor() : base()
        {
            Name = "Пусто";
            Quality = 0;
            Level = 0;
            State = 0;

            strengthValue = 0;
            agilityValue = 0;
            intelliganceValue = 0;
            type = "Пусто";
        }
        public Armor(string _name, int _quality, int _level, int _state) : base(_name, _quality, _level, _state)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;

            strengthValue = 0;
            agilityValue = 0;
            intelliganceValue = 0;
            type = "Пусто";
        }

        public Armor(string _name, int _quality, int _level, int _state, float _strengthValue, float _agilityValue, float _intelliganceValue, string _type) : base(_name, _quality, _level, _state)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;

            strengthValue = 0;
            agilityValue = 0;
            intelliganceValue = 0;
            type = "Пусто";
        }

        public override string ToString()
        {
            try
            {
                return ($"{Name} ({Level} ур., {Quality}*) {strengthValue} - {agilityValue} - {intelliganceValue}");
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
