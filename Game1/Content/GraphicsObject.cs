using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game1.Content
{
    // Kapselt die Darstellung von Grafiken
    public class GraphicsObject
    {
        private Texture2D texture;
        private Bitmap bitmap;
        private int xPos, yPos;
        private int width, height;


        public GraphicsObject(Texture2D texture) {
            this.texture = texture;
            xPos = yPos = 0;
        }

        public GraphicsObject(Bitmap bm)
        {
            this.bitmap = bm;
            xPos = yPos = 0;
        }

        public void SetBitmap(Bitmap bm)
        {
            this.bitmap = bm;
        }

        // TODO
        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(texture, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(xPos, yPos), new Microsoft.Xna.Framework.Point(64, 64)), Microsoft.Xna.Framework.Color.White);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(bitmap, xPos, yPos);
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
