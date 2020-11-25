using System;
using System.Collections.Generic;
using System.Text;

namespace LastHero
{
    public class QuestClass
    {
        public string Title;        // Заголовок
        public string Description;  // Описание
        public int GoldReward;      // Вознаграждение Золото
        public int ExpReward;       // Вознаграждение Опыт

        public QuestClass(string _title, string _descr, int _gold, int _exp)
        {
            Title = _title;
            Description = _descr;
            GoldReward = _gold;
            ExpReward = _exp;
        }
        public override string ToString()
        {
            return (
                "<==== " + Title + " ====>" + "\n\n" +
                Description + "\n" +
                "<==== Вознаграждение: $" + GoldReward + "Exp: " + ExpReward);
        }
    }
}
