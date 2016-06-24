using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    public class Unit
    {
        protected GraphicsObject graphics;
        protected SoundObject sound;

        protected int xPos, yPos;
        protected Point pos;
        protected Spieler spieler;
        protected string id;
        protected string title;
        protected UnitType type;
        protected int price;

        public void SetPos(int x, int y)
        {
            pos = new Point(x, y);
        }

        public void SetPos(Point pos)
        {
            this.pos = pos;
        }

        public String GetId()
        {
            return id;
        }
        public enum UnitType
        {
            DEFAULT,
            APPLE,
            BANANA,         
            MAXEINHEIT
        }

        public void Register()
        {
            Units.Add(id, this);
        }

        public Unit(Unit unit)
        {
            this.sound = unit.sound;
            this.graphics = unit.graphics;

            this.price = unit.price;
            this.id = unit.id;
            this.title = unit.title;
            this.type = unit.type;
            this.spieler = unit.spieler;
            this.pos = unit.pos;
        }

        public Unit(UnitType type, XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            this.type = type;
            price = Convert.ToInt32(node.SelectSingleNode("price").InnerText);

            title = node.SelectSingleNode("title").InnerText;

            String graphic = node.SelectSingleNode("graphic").InnerText;

            graphics = GraphicsObject.graphicObjects[graphic];

            Register();
        }

        public Unit(GraphicsObject graphics, int x, int y, bool accessible)
        {
            this.graphics = graphics;
            pos = new Point(x,y);
        }

        public virtual Unit GetCopy()
        {
            return new Unit(this);
        }

        public void Draw(SpriteBatch batch)
        {
            graphics.SetPos(pos);
            graphics.Draw(batch);
        }

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Unit> Units = new Dictionary<string, Unit>();
    }
}
