using Game1.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    public class Tile
    {
        GraphicsObject graphics;

        protected int xPos, yPos;
        protected bool accessible;
        protected String id;
        protected String title;
        protected TileType type;

        // Neue Typen bitte über MAXTILE einfügen. Dient als Iterationsschranke.
        public enum TileType
        {
            DEFAULT,
            RESSOURCE,
            TREASURE,
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
        }

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Tile> Tiles = new Dictionary<string, Tile>();
    }
}
