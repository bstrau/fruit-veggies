using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class Unit
    {
        GraphicsObject graphics;

        protected int xPos, yPos;
        protected Spieler spieler;
        protected string id;
        protected string title;
        protected UnitType type;
        //FIXMEE Noch zu klären 1
        protected string sound;
        //Ende 1

        public void SetPos(int x, int y)
        {
            xPos = x;
            yPos = y;
        }
        public String GetId()
        {
            return id;
        }
        public enum UnitType
        {
            STANDARD,
            APFEL,
            BANANE,         
            MAXEINHEIT
        }

        public Unit(UnitType type, XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            this.type = type;

            title = node.SelectSingleNode("title").InnerText;

            String graphic = node.SelectSingleNode("graphic").InnerText;

            graphics = GraphicsObject.graphicObjects[graphic];
        }

        public Unit(GraphicsObject graphics, int x, int y, bool accessible)
        {
            this.graphics = graphics;
            xPos = x;
            yPos = y;
        }

        public void Draw(SpriteBatch batch)
        {
            graphics.SetPos(xPos, yPos);
            graphics.Draw(batch);
        }

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Unit> Units = new Dictionary<string, Unit>();

    }
}
