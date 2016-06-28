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
        Pane statusDisplay;
        Text ressourcesString;

        protected string title;
        protected bool defeated ;
        protected string id;
        protected int fraction;
        protected Int32 resourcePoints; 

        public Player(){}

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

        public void SetTitle(String title)
        {
            this.title = title;
        }

        /// <summary>
        /// Setzt den Kontostand des Panels auf den im Parameter übergebenen Wert
        /// </summary>
        /// <param name="points"></param>
        public void SetResourcePoints(int points)
        {
            this.resourcePoints = points;
        }

        /// <summary>
        /// Fügt eine Resourcen Anzeige des Spielers hinzu
        /// </summary>
        /// <param name="playerBar"></param>
        /// <param name="pos"></param>
        public void AddPlayerPane(Pane playerBar, Point pos)
        {
            statusDisplay = new Pane("menuoption", "playerone");
            statusDisplay.setPosition(pos.X, pos.Y);
            statusDisplay.setDimensions(pos.X * 64 / 4, 64);
            statusDisplay.setFont(new FontObject(Game1.font));
            statusDisplay.addText("Player 1", new Point(10, 10));
            statusDisplay.addText("Resourcen:", new Point(10,40));
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

        /// <summary>
        /// Fügt die angegebene Ressourcen Punkte dem Konto des Spielers hinzu
        /// </summary>
        /// <param name="points"></param>
        public void AddRessourcePoints(int points)
        {
            resourcePoints = resourcePoints + points;
            updateRessourcesString();
        }

        /// <summary>
        ///  Updates die Ressourcen Text des Panes
        /// </summary>
        public void updateRessourcesString()
        {
            ressourcesString.text = resourcePoints.ToString();
        }
    }
}
