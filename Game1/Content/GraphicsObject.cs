using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    // Kapselt die Darstellung von Grafiken
    class GraphicsObject
    {
        private Texture2D texture;
        private int xPos, yPos;
        private int width, height;

        public GraphicsObject(Texture2D texture) {
            this.texture = texture;
            xPos = yPos = 0;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(texture, new Rectangle(new Point(xPos, yPos), new Point(64,64)), Color.White);
        } 

        public void SetPos(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public void setDimension(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Waste()
        {
            texture.Dispose();
        }

        // Global erreichbare Liste aller Grafikobjekte. Wird in der LoadContent-Methode gefüllt.
        public static Dictionary<String,GraphicsObject> graphicObjects = new Dictionary<String, GraphicsObject>();
    }
}
