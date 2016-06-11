using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class AttackUnit : Unit
    {
        protected int attackpower;

        public AttackUnit(UnitType type, XmlNode node)
            : base(type, node)
        {
            this.attackpower = Convert.ToInt32(node.SelectSingleNode("attackpower").InnerText);
        }

        public AttackUnit(AttackUnit attackUnit) :base(attackUnit)
        {
            this.attackpower = attackUnit.attackpower;
        }

        public override Unit GetCopy()
        {
            return new AttackUnit(this);
        }

    }
}
