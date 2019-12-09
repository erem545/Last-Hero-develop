using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastHero
{
    class ProbabilityClass
    {
        /// <summary>
        /// Симуляция промаха во время удара
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="value">Статичный шанс попадания</param>
        public static bool ChanceToHit(Character player, int value)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int x = rnd.Next(0, 100);
            if (x < value + player.Accuaracy)
                return true;
            return false;
        }
    }
}
