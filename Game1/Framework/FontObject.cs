using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Framework
{
    public class FontObject
    {
        SpriteFont spriteFont;
        Font font;

        public FontObject(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            this.font = null;
        }

        public SpriteFont getSpriteFont()
        {
            return spriteFont;
        }
       
    }
}
