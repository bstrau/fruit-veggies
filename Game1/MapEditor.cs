using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Game1.Content;

namespace Game1
{
    public partial class MapEditor : Form
    {
        private String TileXMLPath;
        private Size mapSize;
        private Size formSize;
        private Map map;

        public MapEditor()
        {
            this.mapSize = this.Size;
            this.formSize = this.Size;
            
            this.TileXMLPath = "Content\\xml\\Tiles.XML";
            String[] files = Directory.GetFiles("Content\\graphics");

            foreach (String file in files)
            {
                String name = Path.GetFileNameWithoutExtension(file);
                if (GraphicsObject.graphicObjects.ContainsKey(name))
                {
                    GraphicsObject.graphicObjects[name].SetBitmap(new Bitmap(file));
                    
                }
                else {
                    GraphicsObject gObj = new GraphicsObject(new Bitmap(file));
                    GraphicsObject.graphicObjects.Add(name, gObj);
                }   
            }
            InitializeComponent();
            this.timer1.Interval = 100;
            
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            map = Map.Maps["0"];
            panel1.Size = new Size(map.getSizeX() * 64, map.getSizeY() * 64);
            this.Size = new Size(map.getSizeX() * 64 + 100, map.getSizeY() * 64 + 300);

            this.timer1.Start();
            
            /*
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlLoader.loadAllMaps(openFileDialog1.FileName);
                Console.WriteLine(Map.Maps.First().Key);
            } */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            this.map.Draw(g);            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (map != null)
            {
                foreach (Tile t in map.GetMapTiles())
                {
                    if (t.getPos().X <= e.X && t.getPos().X + 64 >= e.X && t.getPos().Y <= e.Y && t.getPos().Y + 64 >= e.Y)
                    {
                        MessageBox.Show(t.GetId() + t.getPos().ToString());
                    }
                }
            }

        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.map.writeToFile(saveFileDialog1.FileName);
            
            }
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.map = new Map();
            this.timer1.Start();
        }
    }
}
