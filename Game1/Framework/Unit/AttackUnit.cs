using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    public class AttackUnit : Unit
    {
        protected int attackpower;
        protected double ripostefactor;

        public AttackUnit(UnitType type, XmlNode node)
            : base(type, node)
        {
            this.attackpower = Convert.ToInt32(node.SelectSingleNode("attackpower").InnerText);
            this.ripostefactor = Convert.ToDouble(node.SelectSingleNode("attackpower").InnerText);
        }

        public AttackUnit(AttackUnit attackUnit) :base(attackUnit)
        {
            this.attackpower = attackUnit.attackpower;
            this.ripostefactor = attackUnit.ripostefactor;
        }

        public override Unit GetCopy()
        {
            return new AttackUnit(this);
        }

        public void Defend (int attackpoints)
        {
            healthpoints = healthpoints - attackpoints;
            
        }

        public int GetAttack ()
        {
            return attackpower;
        }

        public bool IsDead()
        {
            if (healthpoints <= 0)
            {
                return true;
            }
            else
                return false;
        }

        public int GetRiposte()
        {
            return (int)( ripostefactor * GetAttack() );
        }
    }
}
