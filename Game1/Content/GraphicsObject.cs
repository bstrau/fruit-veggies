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

        private System.Drawing.Point pos;
        private Size size;

        public GraphicsObject(Texture2D texture) {
            this.texture = texture;
            pos = new System.Drawing.Point(0, 0);
            size = new Size(64, 64);
        }

        public GraphicsObject(Bitmap bm)
        {
            this.bitmap = bm;
            pos = new System.Drawing.Point(0, 0);
            size = new Size(64, 64);
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
            batch.Draw(texture, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(pos.X, pos.Y), new Microsoft.Xna.Framework.Point(size.Width, size.Height)), Microsoft.Xna.Framework.Color.White);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(bitmap, pos);
        }

        public void SetPos(int x, int y)
        {
            pos = new System.Drawing.Point(x, y);
        }

        public void SetPos(System.Drawing.Point pos)
        {
            this.pos = pos;
        }

        public void setDimension(int width, int height)
        {
            size = new Size(width, height);
        }

        public void setDimension(Size size)
        {
            this.size = size;
        }

        public void Waste()
        {
            texture.Dispose();
            bitmap.Dispose();
        }

        // Global erreichbare Liste aller Grafikobjekte. Wird in der LoadContent-Methode gefüllt.
        public static Dictionary<String,GraphicsObject> graphicObjects = new Dictionary<String, GraphicsObject>();
    }
}
