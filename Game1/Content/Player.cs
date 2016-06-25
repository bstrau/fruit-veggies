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

        public void SetTitle(String title)
        {
            this.title = title;
        }

    }
}
