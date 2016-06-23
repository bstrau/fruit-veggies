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

    class Pane : Component
    {
        private GraphicsObject panel;
        private System.Drawing.Point pos;
        private Size size;

        // Kind Objekte
        private Dictionary<String,Pane> container;

        protected Pane parent;

        /// <summary>
        /// Pane Klasse zum erstellen eines Menüs
        /// 
        /// </summary>
        /// <param name="background_id">Graphicsobject</param>
        /// <param name="component_id">Id der Componente</param>
        public Pane(String background_id, String component_id)  : base(component_id)
        {
            pos = new Point(0, 0);
            size = new Size(320, 180);

            // Background und Dimension des Panels konfigurieren
            panel = GraphicsObject.graphicObjects[background_id];

            container = new Dictionary<string, Pane>();
        }

        public void setPosition(Point pos)
        {
            this.pos = pos;
        }

        public void setPosition(int x, int y)
        {
            this.pos = new Point(x, y);
        }

        public void setDimensions(Size size)
        {
            this.size = size;
        }

        public void setDimensions(int width, int height)
        {
            this.size = new Size(width, height);
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

            foreach (Pane w in this.container.Values)
            {
                w.Draw(batch);
            }
        }

        /// <summary>
        /// Hinzufügen von einem Pane Objekt
        /// </summary>
        /// <param name="w"></param>
        public void AddPane(Pane w)
        {
            w.parent = this;
            this.container.Add(w.id,w);
        }

        /// <summary>
        /// Paneobj über die Id des Objekts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pane getPane(String id)
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
            foreach(Pane w in this.container.Values)
            {
                if (w.absolute_position().X == pos.X && w.absolute_position().Y == pos.Y)
                {
                    
                }
            }
            
        }

        public void Register()
        {
            Panes.Add(id, this);
        }

        public bool isVisible()
        {
            return currentPanes.Contains(id);
        }

        /// <summary>
        /// Trägt sich in die CurrentPanes ein. Wird somit gezeichnet.
        /// </summary>
        public void Show()
        {
            currentPanes.Add(id);
        }

        /// <summary>
        /// Trägt sich aus aus den CurrentPanes aus. Wird somit nicht mehr gezeichnet.
        /// </summary>
        public void Hide()
        {
            currentPanes.Remove(id);
        }

        /// <summary>
        /// Zeichnet die aktuellen anzuzeigenden Panes auf den Bildschirm. Dafür wird 
        /// die statische liste currentpanes verwendet
        /// </summary>
        /// <param name="batch"></param>
        public static void DrawPanes(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            foreach (string cp in currentPanes)
            {
                Panes[cp].Draw(batch);
            }
        }

        // Verwaltet aktuell anzuzeigende Panes.
        public static List<string> currentPanes = new List<string>();

        // Hier werden alle Menüs aufbewahrt.
        public static Dictionary<String, Pane> Panes = new Dictionary<String,Pane>();
    }

}
