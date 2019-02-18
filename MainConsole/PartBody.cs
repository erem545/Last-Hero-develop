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
        float MaxStatus { get { return maxStatus; } set { maxStatus = value; } }
        float maxStatus;
        // Состояние
        float Status { get { return status; } set { status = value; } }
        float status;
        float PercentStatus { get { return status * 100 / maxStatus; } }
        internal float GetStatus { get { return Status * multiplayOut; } }

        float multiplayDamage; // Мультипликатор урона
        float multiplayOut; // Мультипликатор на выход
        
        bool ok; // Наличие
        PartBody parent; // Родитель
        public PartBody()
        {
            Name = null;
            MaxStatus = 0;
            Status = 0;
            ok = false;
            parent = null;
        }
        public PartBody(string _name)
        {
            Name = _name;
            CreateConstants();
            MaxStatus = 0;
            Status = 0;
            ok = false;
            parent = null;
        }

        /// <summary>
        /// При создании новой части, для успешной работы, параметр _name 
        /// задается одним из названий: Body, Head, Left Hand, Right Hand, Left Foot, Right Foot  
        /// </summary>                                                                                      
        /// <param name="_name"></param>                                                               
        /// <param name="_parent"></param>                                                           
        /// <param name="_ok"></param>                                                                 
        /// <param name="_maxStatus"></param>                                                         
        public PartBody(string _name, PartBody _parent, bool _ok, float _maxStatus)
        {
            // Обязательный порядок - сначала имя, потом объявление костант
            Name = _name;
            CreateConstants();
            MaxStatus = _maxStatus;
            Status = _maxStatus;
            ok = _ok;                
            parent = _parent;
        }

        /// <summary>
        /// Лечение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        public float Heal(float value)
        {
            if (ok)
                return value;
            return 0;
        }
        /// <summary>
        /// Повреждение части тела на value единиц
        /// </summary>
        /// <param Name="stat"></param>
        public float Damage(float value)
        {            
            if (ok)
                return value * multiplayDamage;
            return 0;
        }

        /// <summary>
        /// Значение параметра index определяет какая функция начнет свою работу: Damege, Heal
        /// </summary>
        public void PartBodyManager(string _name, float value)
        {
            switch (_name)
            {
                case "Damege":
                    Status += Damage(value*=-1);
                    break;
                case "Heal":
                    Status += Heal(value);
                    break;
                default: throw new Exception("Указанного инструмента не существует!");
            }          
        }
        /// <summary>
        /// Задать индивидульные значения для полей
        /// </summary>
        public void CreateConstants()
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
            return $"\n{Name}\n" +
                   $"Наличие:\t{ok}\n" +
                   $"Состояние:\t{Status} / {MaxStatus}\t({PercentStatus}% / 100%)\n";
        }
    }
    
}
