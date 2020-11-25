using System;
using System.Collections.Generic;
using System.Text;

namespace LastHero
{
    class ProbabilityClass
    {
        /// <summary>
        /// Веруть попадание в зависимости от точности
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="value">Статичный шанс попадания</param>
        public static bool ChanceToHit(Character player)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int x = rnd.Next(0, (int)(100 + (player.Accuaracy * 0.25f)));
            if (x < player.Accuaracy + 50)
                return true;
            return false;
        }
    }
}
