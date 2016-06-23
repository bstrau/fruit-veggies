using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    class FightManager
    {
        Random rand;
        Map map;

        public FightManager()
        {
            rand = new Random();
        }

        public void Fight (AttackUnit attacking, AttackUnit defender)
        {
            double attackpoints;
            double randomfactor;
            
            attackpoints = attacking.GetAttack();
            randomfactor = rand.NextDouble() * 0.2 + 1;
            attackpoints = attackpoints * randomfactor;
            defender.Defend((int)attackpoints);

            map.Update();

            if (defender != null || defender.IsDead() == false)
            {
                attackpoints = defender.GetRiposte();
                randomfactor = rand.NextDouble() * 0.2 + 1;
                attackpoints = attackpoints * randomfactor;
                attacking.Defend((int)attackpoints);
            }            
        }
    }
}
