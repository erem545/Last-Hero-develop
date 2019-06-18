using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole.NPS
{
    class BattleClass
    {
        void Calculation(Character c1, Character c2)
        {

        }
        public static void CreateBattle(Character c1, Character c2)
        {
            do
            {
                switch (GUI.ShowMenu(c1, c2.ToString(),
                    $"Голова  ({c1.minAttack * c2.bodyNode.head.multiplayDamage} / {c1.maxAttack * c2.bodyNode.head.multiplayDamage}) {c2.bodyNode.head.Status}",
                    $"Тело    ({c1.minAttack * c2.bodyNode.body.multiplayDamage} / {c1.maxAttack * c2.bodyNode.body.multiplayDamage}) {c2.bodyNode.body.Status}",
                    $"Л. Рука ({c1.minAttack * c2.bodyNode.lhand.multiplayDamage} / {c1.maxAttack * c2.bodyNode.lhand.multiplayDamage}){c2.bodyNode.lhand.Status}",
                    $"П. Рука ({c1.minAttack * c2.bodyNode.rhand.multiplayDamage} / {c1.maxAttack * c2.bodyNode.rhand.multiplayDamage}){c2.bodyNode.rhand.Status} ",
                    $"Л. Нога ({c1.minAttack * c2.bodyNode.lfoot.multiplayDamage} / {c1.maxAttack * c2.bodyNode.lfoot.multiplayDamage}){c2.bodyNode.lfoot.Status} ",
                    $"П. Нога ({c1.minAttack * c2.bodyNode.rfoot.multiplayDamage} / {c1.maxAttack * c2.bodyNode.rfoot.multiplayDamage}){c2.bodyNode.rfoot.Status} "))
                {
                    case 1:
                        c1.ToAttack(c2, c2.bodyNode.head);
                        break;
                    case 2:
                        c1.ToAttack(c2, c2.bodyNode.body);
                        break;
                    case 3:
                        c1.ToAttack(c2, c2.bodyNode.lhand);
                        break;
                    case 4:
                        c1.ToAttack(c2, c2.bodyNode.rhand);
                        break;
                    case 5:
                        c1.ToAttack(c2, c2.bodyNode.lfoot);
                        break;
                    case 6:
                        c1.ToAttack(c2, c2.bodyNode.rfoot);
                        break;
                }
                Console.ReadKey();
                } while ((c1.ok) || (c2.ok));
        }
    }
}
