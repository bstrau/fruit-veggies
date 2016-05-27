using Game1.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game1.Content
{
    class Tile
    {
        GraphicsObject graphics;
        
        int xPos, yPos;
        bool hindernis;

        public Tile(GraphicsObject graphics, int x, int y, bool hindernis)
        {
            this.graphics = graphics;
            xPos = x;
            yPos = y;
            this.hindernis = hindernis;
        }

        public void SetPos(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public void Draw(SpriteBatch batch)
        {
            graphics.SetPos(xPos, yPos);
            graphics.Draw(batch);
        }

        // Global erreichbare Liste aller Grafikobjekte. Wird in der ContentLoad Methode über den XML-Loader gefüllt.
        public static Dictionary<String, Tile> Tiles = new Dictionary<string, Tile>();
    }
}
