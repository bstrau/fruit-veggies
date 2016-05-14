using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    class GraphicsObject
    {
        private Texture2D texture;
        private Rectangle rect;

        public GraphicsObject(Texture2D texture) {
            this.texture = texture;
            rect = new Rectangle();
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(texture, rect, Color.White);
        } 

        public void SetRect(Rectangle rect)
        {
            this.rect = rect;
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public void Waste()
        {
            texture.Dispose();
        }

        // Global erreichbare Liste aller Grafikobjekte. Wird in der LoadContent-Methode gefüllt.
        public static Dictionary<String,GraphicsObject> graphicObjects;
    }
}
