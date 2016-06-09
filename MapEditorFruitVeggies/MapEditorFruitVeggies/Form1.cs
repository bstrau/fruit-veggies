using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Game1.Content;
using System.IO;

namespace MapEditorFruitVeggies
{
    public partial class Form1 : Form
    {
        private int tileSize;
        private int mapHeight;
        private int mapWidth;

        public Form1()
        {
            mapWidth = 16;
            mapHeight = 9;

            tileSize = 64;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BorderStyle = BorderStyle.FixedSingle;
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //XmlLoader.loadAllTiles(openFileDialog1.FileName);
                String[] files = Directory.GetFiles("Content\\graphics");

                foreach (String file in files)
                {
                    Bitmap bitmap = new Bitmap(file);

                    GraphicsObject test = new GraphicsObject(bitmap);
                    /*if (test != null)
                    {
                        String name = Path.GetFileNameWithoutExtension(file);
                        GraphicsObject.graphicObjects.Add(name, test);
                    }*/
                }

            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlLoader.loadAllMaps(openFileDialog1.FileName);    
                Console.WriteLine(Map.Maps.First().Key);
            }
        }
    }
}
