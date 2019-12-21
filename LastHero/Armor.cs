using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class Armor : Item
    {
        public int strengthValue;
        public int agilityValue;
        public int intelliganceValue;
        public int armorValue;
        public Armor() : base()
        {
            strengthValue = 0;
            agilityValue = 0;
            intelliganceValue = 0;
            armorValue = 0;
            type = "Защитное снаряжение";
        }
        public Armor(string _name, int _quality, int _level, int _state) : base(_name, _quality, _level, _state)
        {
            strengthValue = 0;
            agilityValue = 0;
            intelliganceValue = 0;
            armorValue = 0;
            type = "Защитное снаряжение";
        }

        public Armor(string _name, int _quality, int _level, int _state, int _strengthValue, int _agilityValue, int _intelliganceValue, int armorvalue, string _type) : base(_name, _quality, _level, _state)
        {
            strengthValue = _strengthValue;
            agilityValue = _agilityValue;
            intelliganceValue = _intelliganceValue;
            armorValue = armorvalue;
            type = _type;
        }

        public override string ToString()
        {
            try
            {
                return ($"{Name} ({strengthValue}-{agilityValue}-{intelliganceValue}) Защита: {armorValue} ");
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
