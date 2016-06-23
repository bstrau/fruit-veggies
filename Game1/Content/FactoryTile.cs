using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace Game1.Content
{
    class FactoryTile : Tile
    {
        public FactoryTile(TileType type, XmlNode node)
            : base(type, node)
        {
 
        }

        public Unit buyUnit(int x, int y)
        {
            Pane pane = new Pane("menu", "buy Player");
            pane.Register(); // Als Pane Registrieren
            pane.setPosition(x, y);
            pane.setDimensions(350, 450);
            pane.Show();
            return null;
        }

        public override void onClick(MouseEventArgs e)
        {
            // Window Open
            if (occupant != null)
            {
                buyUnit(e.X,e.Y);
            }
        }

        public override void onClick(Microsoft.Xna.Framework.Input.MouseState e)
        {
            // Window Open
            if (occupant != null)
            {
                buyUnit(e.X, e.Y);
            }
        }

        //public Unit createUnit(String unit_id)
        //{
        //    return (Unit.Units[unit_id]).GetCopy();
        //}


    }
}
