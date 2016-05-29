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
    class Tile
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
            STANDARD,
            RESSOURCE,
            RESSOURCEONCE,
            MAXTILE
        }

        protected void Register() {
            Tiles.Add(id, this);
        }

        public Tile(TileType type, XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            this.type = type;

            title = node.SelectSingleNode("title").InnerText;
            accessible = Convert.ToBoolean(node.SelectSingleNode("accessible").InnerText);
            String graphic = node.SelectSingleNode("graphic").InnerText;

            graphics = GraphicsObject.graphicObjects[graphic];
        }

        public Tile(GraphicsObject graphics, int x, int y, bool accessible)
        {
            this.graphics = graphics;
            xPos = x;
            yPos = y;
            this.accessible = accessible;
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
