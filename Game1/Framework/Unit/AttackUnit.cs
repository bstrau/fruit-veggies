using Game1.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.ripostefactor = Convert.ToDouble(node.SelectSingleNode("ripostefactor").InnerText);
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
                this.lifePointsDisplay.Hide();
                this.attackpowerDisplay.Hide();

                return true;

            }
            else
                return false;
        }

        public int GetRiposte()
        {
            return (int)( ripostefactor * GetAttack() );
        }

        public override void LifePointsDisplay(int x, int y)
        {
            FontObject font = new FontObject(Game1.font_small);
            base.LifePointsDisplay(x, y);
            attackpowerDisplay = new Pane("menuoption", "attackposerdisplay" + this.id);
            attackpowerDisplay.setPosition(x + 32, y);
            attackpowerDisplay.setDimensions(32, 12);
            attackpowerDisplay.setFont(font);
            attackpowerDisplay.addText(this.attackpower.ToString(), new Point(2,2));
            //lifePointsDisplay.Register();
            
            attackpowerDisplay.Show();
        
        }

        public override void onCursorLeave()
        {
 	        base.onCursorLeave();
            attackpowerDisplay.Hide();
        }
        
    }
}
