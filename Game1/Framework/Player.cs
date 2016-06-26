using System;
using System.Collections.Generic;
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

        public void SetTitle(String title)
        {
            this.title = title;
        }

        public void SetResourcePoints(int points)
        {
            this.resourcePoints = points;
        }
    }
}
