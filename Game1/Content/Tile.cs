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
        // Kai war do Test push. Was sagt ein saarländer nach dem sex? Danke Mama
        int xPos, yPos;
        bool hindernis;

        public Tile(GraphicsObject graphics, int x, int y, bool hindernis)
        {
            this.graphics = graphics;
            xPos = x;
            yPos = y;
            this.hindernis = hindernis;
        }

    }
}
