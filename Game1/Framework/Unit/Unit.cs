using Game1.Framework;
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

        protected int healthpoints;
        protected int xPos, yPos;
        protected Point pos;
        protected Player player;
        protected string id;
        protected string title;
        protected UnitType type;
        protected int price;
        protected int movePoints;
        protected string ownerId;

        protected Pane lifePointsDisplay;
        protected Pane attackpowerDisplay;

        // Wird true gesetzt, nachdem in einer Runde mit dieser Einheit gezogen wurde
        bool moved;

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
            this.healthpoints = unit.healthpoints;
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

            this.healthpoints = Convert.ToInt32(node.SelectSingleNode("healthpoints").InnerText);

            ownerId = node.SelectSingleNode("ownerId").InnerText;

            String graphic = node.SelectSingleNode("graphic").InnerText;

            graphics = GraphicsObject.graphicObjects[graphic];

            Register();
        }

        public Unit(GraphicsObject graphics, int x, int y, bool accessible)
        {
            this.graphics = graphics;
            pos = new Point(x,y);
        }

        public virtual void LifePointsDisplay(int x, int y)
        {
            FontObject font = new FontObject(Game1.font_small);
            lifePointsDisplay = new Pane("menugreen", "lifePointsDisplay" + this.id);
            lifePointsDisplay.setPosition(x, y);
            lifePointsDisplay.setDimensions(32, 12);
            lifePointsDisplay.setFont(font);
            lifePointsDisplay.addText(this.healthpoints.ToString(), new Point(2, 2));
            lifePointsDisplay.Show();
        }

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

        public String GetTitle()
        {
            return title;
        }

        public Int32 GetPrice()
        {
            return price;
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

        public void onMouseMove(Point pos)
        {
            LifePointsDisplay(pos.X,pos.Y);
        }

        public virtual void onCursorLeave()
        {
            lifePointsDisplay.Hide();
        }

        public int GetHealthPoints()
        {
            return this.healthpoints;
        }

        public void onRoundBegin(Player currentPlayer)
        {
            moved = false;
        }

        public void Moved()
        {
            moved = true;
        }

        public bool MayMove()
        {
            return !moved;
        }

        public string getOwnerId()
        {
            return ownerId;
        }

       

        // Global erreichbare Liste aller Tiles.
        public static Dictionary<String, Unit> Units = new Dictionary<string, Unit>();
    }
}
