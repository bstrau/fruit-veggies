using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    public class FightManager
    {
        Random rand;
        Map map;

        public FightManager(Map map)
        {
            rand = new Random();
            this.map = map;
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
