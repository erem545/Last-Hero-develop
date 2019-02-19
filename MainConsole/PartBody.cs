using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class PartBody
    {
        public string Name { get; set; } // Название 

        // Макс. состояние      
        float MaxStatus { get; set; }

        // Состояние
        internal float Status { get { return status; } set { if (value + status > MaxStatus) status = MaxStatus; status = value; } }
        float status;
        float PercentStatus { get { return Status * 100 / MaxStatus; } }

        float multiplayDamage; // Мультипликатор урона
        float multiplayOut; // Мультипликатор на выход
        
        bool ok; // Наличие
        public PartBody()
        {

        }
        public PartBody(string _name)
        {
            Name = _name;
            CreateConstants();
            MaxStatus = 0;
            Status = 0;
            ok = false;
        }

        /// <summary>
        /// При создании новой части, для успешной работы, параметр _name 
        /// задается одним из названий: Body, Head, Left Hand, Right Hand, Left Foot, Right Foot  
        /// </summary>                                                                                      
        /// <param name="_name"></param>                                                               
        /// <param name="_parent"></param>                                                           
        /// <param name="_ok"></param>                                                                 
        /// <param name="_maxStatus"></param>                                                         
        public PartBody(string _name, bool _ok, float _maxStatus)
        {
            // Обязательный порядок - сначала имя, потом объявление костант
            Name = _name;
            CreateConstants();

            MaxStatus = _maxStatus * multiplayOut;
            Status = MaxStatus;           
            ok = _ok;                
        }

        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        float Heal(float value)
        {
            if (Status < MaxStatus)
                if (ok)
                    return value;
            return 0;
        }
        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        float Damage(float value)
        {            
            if (ok)
                return value * multiplayDamage / (1-multiplayOut);
            return 0;
        }

        /// <summary>
        /// Значение параметра index определяет какая функция начнет свою работу: Damege, Heal
        /// </summary>
        public void PartBodyManager(string name, float value)
        {
            switch (name)
            {
                case "Damage":
                    Status += Damage(value*=-1);
                    break;
                case "Heal":
                    Status += Heal(value);
                    if (Status > MaxStatus) Status = MaxStatus;
                    break;
                default: return;
            }
            
        }

        /// <summary>
        /// Задать индивидульные значения для полей
        /// </summary>
        internal void CreateConstants()
        {
            switch (Name)
            {
                case "Body":
                    multiplayDamage = 1.0f;
                    multiplayOut = 0.42f;
                    break;
                case "Head":
                    multiplayDamage = 2.0f;
                    multiplayOut = 0.16f;
                    break;
                case "Left Hand":
                    multiplayDamage = 1.3f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Hand":
                    multiplayDamage = 1.3f;
                    multiplayOut = 0.105f;
                    break;
                case "Left Foot":
                    multiplayDamage = 1.4f;
                    multiplayOut = 0.105f;
                    break;
                case "Right Foot":
                    multiplayDamage = 1.4f;
                    multiplayOut = 0.105f;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Name}\n" +
                   $"Наличие:\t{ok}\n" +
                   $"Состояние:\t{Status} / {MaxStatus} \t\t({PercentStatus}% / 100%)\n";
        }
    }
    
}
