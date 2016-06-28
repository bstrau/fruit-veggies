using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using Game1.Framework;
using System.Drawing;

namespace Game1.Content
{
    public class FactoryTile : CaptureTile
    {
        private Pane buyMenu;

        public FactoryTile(TileType type, XmlNode node)
            : base(type, node)
        {
        }

        public FactoryTile(FactoryTile ftile) 
            : base(ftile)
        {   
        }

        public override Tile GetCopy()
        {
            return new FactoryTile(this);
        }
        
        public void BuyUnit(object sender, EventArgs eventArgs)
        {
            Pane clickedPane = (Pane)sender;
            Unit unit = Unit.Units[clickedPane.GetId()].GetCopy();

            unit.setPlayer(owner);
            enter(unit);
            owner.SubtractRessourcePoints(unit.GetPrice());
            GameManager.gameState = GAMESTATE.MAP;
            buyMenu.Hide();
        }

        public void BuyMenu()
        {
            // Abbrechen, da Einheit diese Tile besetzt
            if(occupant != null)
            {
                return;
            }

            FontObject font = new FontObject(Game1.font);
            buyMenu = new Pane("menu", "buyMenu");
            buyMenu.setPosition(0, 0);
            buyMenu.setDimensions(200, 200);
            buyMenu.setFont(font);

            int i = 10;
            foreach (Unit u in Unit.Units.Values)
            {
                // Ein Spieler darf nur Einheiten kaufen, die seiner Fraktion/OwnerId angehören
                if(u.getOwnerId() != GameManager.currentPlayer.GetId())
                {
                    continue;
                }
                Pane buyUnitP = new Pane("menuoption", u.GetId());
                buyUnitP.setPosition(10, i);
                buyUnitP.setDimensions(100, 25);
                buyUnitP.setFont(font);
                buyUnitP.addText(u.GetTitle() + ": " + u.GetPrice() , new Point(5, 5));
                buyUnitP.Clicked += BuyUnit;
                buyMenu.AddPane(buyUnitP);
                
                buyUnitP.Show();
                i += 32;
            }
            buyMenu.Show();
        }

        public override void onClick(Point pos)
        {
            if (GameManager.currentPlayer != owner)
                return;

            base.onClick(pos);

            // Einheiten können nur gekauft werden wenn keine auf dem Feld steht, sie würde ja sonst überschrieben.
            if (occupant == null)
            {
                GameManager.gameState = GAMESTATE.MENU;
                BuyMenu();
            }
        }
    }
}
