using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Player : Character
    {
        // Тип объекта
        readonly string TypeObj = "Игрок";

        //Выносливость
        float MaxEndurance;
        float Endurance;
        float PercentEndurance { get { return Endurance * 100 / MaxEndurance; } }

        public Player() : base()
        {
            Endurance = 0;
            MaxEndurance = 0;
        }
        public Player(string _name, float _maxHealth, float _maxEndurance) : base(_name,_maxHealth)
        {            
            MainName = _name;
            MaxHealth = _maxHealth;    
            Health = _maxHealth;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            bodyNode = new PartBodyNode(_maxHealth);
        }

        public override string ToString()
        {
            return (base.ToString() + $"Выносливость:\t{Endurance} / {MaxEndurance} ({Math.Round(PercentEndurance)}% / 100%)");
            //return (base.ToString() + $"Выносливость: {Math.Round(Endurance)} / {Math.Round(MaxEndurance)}\n");
        }
    }
}
