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

        // Раса
        Race CharacterRace;    

        //Выносливость
        float MaxEndurance;
        internal float Endurance;
        float PercentEndurance { get { return Endurance * 100 / MaxEndurance; } }
        public float EnduranceRegenPercent;

        public Player() : base()
        {
            CharacterRace = Race.Neutral;
            Endurance = 0;
            MaxEndurance = 0;
        }
        public Player(string _name, float _maxHealth, float _maxEndurance, Race race) : base(_name,_maxHealth)
        {
            CharacterRace = race;
            Endurance = _maxEndurance;
            MaxEndurance = _maxEndurance;
            switch (race)
            {
                case Race.Humans:
                    NewHumansRace(_name, _maxHealth, _maxEndurance);
                    break;

                case Race.Ogres:
                    NewOgresRace(_name, _maxHealth, _maxEndurance);
                    break;
            }
        }



        /// <summary>
        /// Присоединение к расе людей
        /// </summary>
        /// <param name="_name">Имя объекта</param>
        /// <param name="_maxHealth">Максимальный запас здоровья</param>
        /// <param name="_maxEndurance">Максимальный запас выносливости</param>
        /// <returns></returns>
        public Player NewHumansRace(string _name, float _maxHealth, float _maxEndurance)
        {
            EnduranceRegenPercent = 0.041f;
            HealthRegenPercent = 0.002f;
            bodyNode = new PartBodyNode(_maxHealth);
            return null;
        }
        /// <summary>
        /// Присоединение к расе огров
        /// </summary>
        /// <param name="_name">Имя объекта</param>
        /// <param name="_maxHealth">Максимальный запас здоровья</param>
        /// <param name="_maxEndurance">Максимальный запас выносливости</param>
        /// <returns></returns>
        public Player NewOgresRace(string _name, float _maxHealth, float _maxEndurance)
        {
            EnduranceRegenPercent = 0.03f;
            HealthRegenPercent = 0.003f;
            bodyNode = new PartBodyNode(_maxHealth);
            return null;
        }



        /// <summary>
        /// Атаковать противника
        /// </summary>
        /// <param name="person">Противник</param>
        public override void ToAttack(Character person)
        {
            if (Endurance - 5 >= 0)
                if ((ok) && (person.ok))
                {
                    base.ToAttack(person);
                    Endurance -= 5; 
                }
                else
                    Console.WriteLine("Противник мертв!");
            else
                Console.WriteLine("\nНедостаточно выносливости!");
        }
        /// <summary>
        /// Защищаться от атаки (Увеличение сопротивления урона за ед. защиты)
        /// </summary>
        /// <param name="person">Нападающий</param>
        public override void ToDefend(Character person)
        {
            if ((ok) && (person.ok))
            {
                base.ToDefend(person);                
                
            }
            else
                Console.WriteLine("Противник мертв!");
        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            Endurance += MaxEndurance * EnduranceRegenPercent;
            if (Endurance > MaxEndurance)
                Endurance = MaxEndurance;
            else if (Endurance < 0)
                Endurance = 0;
        }

        /// <summary>
        /// Убить персонажа
        /// </summary>
        public override void Kill()
        {
            base.Kill();
            Endurance = 0;
        }

        public override string ToString()
        {
            return (base.ToString() +
                $"Выносливость:\t{Endurance} / {MaxEndurance} ({PercentEndurance}% / 100%)\n" + "Защита: " + Armor);
            //return (base.ToString() + $"Выносливость: {Endurance} / {MaxEndurance}\n");
        }
    }
}
