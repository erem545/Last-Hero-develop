using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
namespace LastHero
{
    [Serializable]
    public class Item
    {
        public string Name;
        public int Quality;
        public int Level;
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        int state;       
        public string d_path;
        public string d_icon;
        public string type;
        public Item()
        {
            Name = "Пусто";
            Quality = 0;
            Level = 0;
            State = 0;
            type = "Предмет";
            d_path = "";
            d_icon = "";
        }

        public Item(string _name, int _quality, int _level, int _state)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;
            type = "Предмет"; 
            d_path = "";
            d_icon = "";
        }
        public Item(string _name, int _quality, int _level, int _state, string _type)
        {
            Name = _name;
            Quality = _quality;
            Level = _level;
            State = _state;
            type = _type;
            d_path = "";
            d_icon = "";
        }

        public override string ToString()
        {
            try
            {
                return ($"{Name} Уровень:{Level} Качество:{Quality} Состояние:{State}%");
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
