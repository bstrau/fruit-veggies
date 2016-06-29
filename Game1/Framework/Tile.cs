using Game1.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;

namespace Game1.Content
{
    public class Tile
    {
        GraphicsObject graphics;

        protected int xPos, yPos;
        protected Point pos;
        protected bool accessible;
        // Getter Setter werden für Map Editor benötigt
        public string title { get; set; }
        public string id { get; set; }

        protected TileType type;

        // Speichert Verweis auf eine Einheit die evtl. auf diesem Tile steht
        protected Unit occupant;

        // Neue Typen bitte über MAXTILE einfügen. Dient als Iterationsschranke.
        public enum TileType
        {
            DEFAULT,
            RESSOURCE,
            TREASURE,
            FACTORY,
            BASE,
            MAXTILE
        }

        public void Register() {
            Tiles.Add(id, this);
        }

        public Tile(Tile tile)
        {
            this.id = tile.id;
            this.title = tile.title;
            this.type = tile.type;
            this.xPos = tile.xPos;
            this.yPos = tile.yPos;
            this.graphics = tile.graphics;
            this.accessible = tile.accessible;
        }

        public Tile(TileType type, XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            this.type = type;

            title = node.SelectSingleNode("title").InnerText;
            accessible = Convert.ToBoolean(node.SelectSingleNode("accessible").InnerText);
            String graphic = node.SelectSingleNode("graphic").InnerText;

            graphics = GraphicsObject.graphicObjects[graphic];

            // Als Prototyp registrieren
            Register();
        }

        public Tile(GraphicsObject graphics, int x, int y, bool accessible)
        {
            this.graphics = graphics;
            xPos = x;
            yPos = y;
            this.accessible = accessible;
        }

        public virtual Tile GetCopy()
        {
            return new Tile(this);
        }

        public void SetPos(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public Point getPos()
        {
            return new Point(this.xPos, this.yPos);
        }

        public String GetId()
        {
            return id;
        }

        public TileType GetTileType()
        {
            return type;
        }

        public void Draw(SpriteBatch batch)
        {
            graphics.SetPos(xPos, yPos);
            graphics.Draw(batch);
            if (occupant != null)
            {
                occupant.SetPos(xPos, yPos);
                occupant.Draw(batch);
            }
        }

        public bool enter(Unit unit)
        {
            if (accessible == false)
            {
                return false;
            }

            occupant = unit;

            return true;
        }

        public void Draw(Graphics g)
        {
            graphics.SetPos(xPos, yPos);
            graphics.Draw(g);
        }

        public Unit getOccupant()
        {
            return occupant;
        }


        public virtual void onClick(MouseEventArgs e)
        {
            if (occupant != null){
                //occupant.onClick();
            }
        }

        public Unit leave()
        {
            Unit ret = occupant;
            occupant = null;
            return ret;
        }

        public bool Reachable()
        {
            if (this.accessible == true && occupant == null)
                return true;
            else
                return false;
        }

        public bool Passable(Unit unit)
        {
            if(occupant != null && (occupant.getPlayer() != unit.getPlayer()))
            {
                return false;
            }
            else
                return true;
        }

        public virtual void onClick(Point pos)
        {
            
        }

        public virtual void onTurnBegin(Player currentplayer)
        {
            if(occupant != null && occupant.getPlayer() == currentplayer)
            {
                occupant.onRoundBegin(currentplayer);
            }
        }

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Tile> Tiles = new Dictionary<string, Tile>();
    }
}
