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
        protected Player player;
        protected string id;
        protected string title;
        protected UnitType type;
        protected int price;
        protected int movePoints;

        // Wird true gesetzt, nachdem in einer Runde mit dieser Einheit gezogen wurde
        bool moved;

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
            this.movePoints = unit.movePoints;
            this.price = unit.price;
            this.id = unit.id;
            this.title = unit.title;
            this.type = unit.type;
            this.player = unit.player;
            this.pos = unit.pos;
            this.moved = false;
        }

        public Unit(UnitType type, XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            this.type = type;
            price = Convert.ToInt32(node.SelectSingleNode("price").InnerText);

            movePoints = Convert.ToInt32(node.SelectSingleNode("movepoints").InnerText);

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

        public int getMovePoints()
        {
            return movePoints;   
        }

        public Player getPlayer()
        {
            return player;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
        }

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Unit> Units = new Dictionary<string, Unit>();
    }
}
