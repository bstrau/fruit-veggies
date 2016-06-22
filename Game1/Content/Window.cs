using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Drawing;

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
        private GraphicsObject panel;
        private System.Drawing.Point pos;
        private Size size;

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
            pos = new Point(0, 0);
            size = new Size(320, 180);

            // Background und Dimension des Panels konfigurieren
            panel = GraphicsObject.graphicObjects[background_id];

            container = new Dictionary<string, Window>();
        }

        public void setPosition(Point pos)
        {
            this.pos = pos;
        }

        public void setDimensions(Size size)
        {
            this.size = size;
        }

        /// <summary>
        /// Zeichnet das Fenster mit allen seinen enthaltenen Kind-Objekten
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            panel.SetPos(absolute_position());
            panel.setDimension(size);
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
            if (parent != null)
                return new Point(pos.X + parent.absolute_position().X, pos.Y + parent.absolute_position().Y);
            else
                return pos; 
        }

        public void onClick(MouseState state)
        {
            Point pos = new Point(state.Position.X, state.Position.Y);
            foreach(Window w in this.container.Values)
            {
                if (w.absolute_position().X == pos.X && w.absolute_position().Y == pos.Y)
                {
                    
                }
            }
            
        }
    }

}
