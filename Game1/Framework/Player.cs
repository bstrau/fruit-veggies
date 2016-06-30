using Game1.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    public class Player
    {
        GraphicsObject graphics;
        SoundObject sound;

        protected string title;
        protected bool defeated ;
        protected string id;
        //FIXME 1 Noch zu klären
        protected int fraction;
        //END VON FIXME 1

        Pane statusDisplay;
        Text ressourcesString;

        protected Int32 resourcePoints;

        public Player()
        { 
        }

        public Player(String id)
        {
            this.id = id;
        }

        public String GetId()
        {
            return id;
        }

        public String GetTitle()
        {
            return title;
        }

        public Int32 GetResourcePoints()
        {
            return resourcePoints;
        }

        public string GetOwnerId()
        {
            return this.id;
        }

        public void SetTitle(String title)
        {
            this.title = title;
        }

        public void SetResourcePoints(int points)
        {
            this.resourcePoints = points;
        }

        public void AddPlayerPane(Pane playerBar, Point pos, Size size)
        {
            statusDisplay = new Pane("menuoption", "player"+this.id);
            statusDisplay.setPosition(pos.X, pos.Y);
            statusDisplay.setDimensions(size.Width, 64);
            statusDisplay.setFont(new FontObject(Game1.font));
            statusDisplay.addText( this.title , new Point(10, 10));
            statusDisplay.addText("Ressourcen:", new Point(10,40));
            ressourcesString = new Text(this.GetResourcePoints().ToString(), 100, 40);
            statusDisplay.addText(ressourcesString);

            statusDisplay.Show();
            playerBar.AddPane(statusDisplay);
        }

        /// <summary>
        /// Zieht eine Menge von Punkten den ressourepoints ab zb: bei kauf von units
        /// </summary>
        /// <param name="points"></param>
        public void SubtractRessourcePoints(int points)
        {
            resourcePoints = resourcePoints - points;
            updateRessourcesString();
        }

        public void AddRessourcePoints(int points)
        {
            resourcePoints = resourcePoints + points;
            updateRessourcesString();
        }

        public void updateRessourcesString()
        {
            ressourcesString.text = resourcePoints.ToString();
        }
    }
}
