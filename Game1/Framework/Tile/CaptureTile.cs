using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    public class CaptureTile : Tile
    {
        protected int capturepoints;
        protected Player owner;

        public CaptureTile(TileType type, XmlNode node): base(type,node)
        {
            capturepoints = Convert.ToInt32(node.SelectSingleNode("capturepoints").InnerText); 
        }

        public CaptureTile(CaptureTile tile) : base(tile)
        {
            this.capturepoints = tile.capturepoints;
        }

        public override void onClick(System.Drawing.Point pos)
        {
 	         base.onClick(pos);
        }

        public void setOwner(Player o)
        {
            owner = o;
        }

        public Player getOwner()
        {
            return owner;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            // Setze eine Färbung des CapturTiles, damit man erkennen kann, wem es gehört.
            Microsoft.Xna.Framework.Color tint = Microsoft.Xna.Framework.Color.White;
            if(owner == GameManager.playerOne)
            {
                tint = Microsoft.Xna.Framework.Color.Red;
            }
            else if (owner == GameManager.playerTwo)
            {
                tint = Microsoft.Xna.Framework.Color.Blue;
            }

            graphics.SetPos(xPos, yPos);
            graphics.Draw(batch,tint);
            if (occupant != null)
            {
                occupant.SetPos(xPos, yPos);
                occupant.Draw(batch);
            }
        }
        public void Capture(Unit unit)
        {
            // TODO: Logik für Eroberung über mehrere Runden einfügen
            setOwner(unit.getPlayer());
        }
    }
}
