using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using Game1.Framework;


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

    class Text
    {
        public Point pos;
        public String text;

        public Text(String text, int x, int y)
        {
            this.pos = new Point(x, y);
            this.text = text;
        }
    }

    class Pane : Component
    {
        private GraphicsObject panel;
        private FontObject font;

        private System.Drawing.Point pos;
        private Size size;

        // Kind Objekte
        private Dictionary<String,Pane> container;
        private List<Text> texts;

        protected Pane parent;
        public event EventHandler Clicked;

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

            // Texte initialisieren
            texts = new List<Text>();

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

            foreach(Text text in texts)
            {
                batch.DrawString(font.getSpriteFont(), text.text, new Microsoft.Xna.Framework.Vector2( absolute_position().X + text.pos.X, absolute_position().Y + text.pos.Y), Microsoft.Xna.Framework.Color.Black);
            }

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

        public void addText(String text, Point pos)
        {
            texts.Add(new Text(text, pos.X, pos.Y));
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
            return currentPanes.Contains(this);
        }

        /// <summary>
        /// Trägt sich in die CurrentPanes ein. Wird somit gezeichnet.
        /// </summary>
        public void Show()
        {
            currentPanes.Add(this);
        }

        /// <summary>
        /// Trägt sich aus aus den CurrentPanes aus. Wird somit nicht mehr gezeichnet.
        /// </summary>
        public void Hide()
        {
            currentPanes.Remove(this);
        }

        /// <summary>
        /// Zeichnet die aktuellen anzuzeigenden Panes auf den Bildschirm. Dafür wird 
        /// die statische liste currentpanes verwendet
        /// </summary>
        /// <param name="batch"></param>
        public static void DrawPanes(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            foreach (Pane pane in currentPanes)
            {
                pane.Draw(batch);
            }
        }

        public void setFont(FontObject font)
        {
            this.font = font;
        }

        public void onClick(Point pos)
        {
            foreach(Pane child in container.Values)
            {
                if (child.isHit(pos))
                {
                    child.onClick(pos);
                    return;
                }
            }

            // KlickEvent auslösen
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public bool isHit(Point pos)
        {
            if ((pos.X <= absolute_position().X + size.Width) && (pos.X > absolute_position().X) &&
                (pos.Y <= absolute_position().Y + size.Height) && (pos.Y > absolute_position().Y))
                return true;
            else
                return false;
        }

        // Verwaltet aktuell anzuzeigende Panes.
        public static List<Pane> currentPanes = new List<Pane>();

        // Hier werden alle Menüs aufbewahrt.
        public static Dictionary<String, Pane> Panes = new Dictionary<String,Pane>();
    }

}
