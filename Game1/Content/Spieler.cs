using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    class Spieler
    {
        GraphicsObject graphics;
        GraphicsObject RessourcesTile;

        protected string title;
        protected bool tot ;
        protected string id;
        //FIXME 1 Noch zu klären
        protected int Fraktion;
        protected int Sound;
        //END VON FIXME 1

        public String GetId()
        {
            return id;
        }



    }
}
