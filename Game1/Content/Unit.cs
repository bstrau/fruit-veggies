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
        protected int price;
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
            this.id = unit.id;
            this.title = unit.title;
            this.type = unit.type;
            this.price = unit.price;
            this.spieler = unit.spieler;
            this.xPos = unit.xPos;
            this.yPos = unit.yPos;
            this.sound = unit.sound;
            this.graphics = unit.graphics;
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
            xPos = x;
            yPos = y;
        }

        public virtual Unit GetCopy()
        {
            return new Unit(this);
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
