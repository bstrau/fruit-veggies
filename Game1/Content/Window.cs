using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game1.Content
{
    class Component
    {
        protected String id;

        public Component(String id)
        {
            this.id = id;
        }
    }

    class Window : Component
    {
        // panel
        private GraphicsObject panel;
        // Position des Fensters auf dem Programmmbildschirm
        private int xPos, yPos;
        // Kind Objekte
        private Dictionary<String,Window> container;

        protected Window parent;

        /// <summary>
        /// Window Klasse zum erstellen eines Menüs
        /// </summary>
        /// <param name="background_id"></param>
        /// <param name="component_id"></param>
        public Window(String background_id, String component_id)  : base(component_id)
        {
            xPos = 0;
            yPos = 0;

            // Background und Dimension des Panels konfigurieren
            panel = GraphicsObject.graphicObjects[background_id];
            panel.setDimension(320,180);
        }

        /// <summary>
        /// Zeichnet das Fenster mit allen seinen enthaltenen Kind-Objekten
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            panel.SetPos(xPos, yPos);
            panel.Draw(batch);
            foreach (Window w in this.container.Values)
            {
                w.Draw(batch);
            }
        }

        /// <summary>
        /// Hinzufügen von einem Window Objekt
        /// </summary>
        /// <param name="w"></param>
        public void AddWindow(Window w)
        {
            w.parent = this;
            this.container.Add(w.id,w);
        }

        /// <summary>
        /// Windowobj über die Id des Objekts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Window getWindow(String id)
        {
            return this.container[id];
        }

        public Point absolute_position()
        {
            return new Point(this.xPos + parent.absolute_position().X, this.yPos + parent.absolute_position().Y); 
        }

        public void onClick(MouseState state)
        {
            Point pos = state.Position;
            foreach(Window w in this.container.Values)
            {
                if (w.absolute_position().X == pos.X && w.absolute_position().Y == pos.Y)
                {
 
                }
            }
            
        }
    }

}
