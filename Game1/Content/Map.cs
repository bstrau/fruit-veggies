using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Game1.Content
{
    public class Map
    {
        private String id;
        private String title;
        private int sizeX, sizeY;
        private Tile[,] tiles;
        private SoundObject bgSound;

        // künftig public Map(XMLNode node)
        public Map(XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            title = node.SelectSingleNode("title").InnerText;
            sizeX = Convert.ToInt32(node.SelectSingleNode("SizeX").InnerText);
            sizeY = Convert.ToInt32(node.SelectSingleNode("SizeY").InnerText);

            String sound = node.SelectSingleNode("sound") .InnerText;
            bgSound = SoundObject.soundObjects[sound];

            tiles = new Tile[sizeY, sizeX];

            string[] tileids = node.SelectSingleNode("tiles").InnerText.Split(',');
            if(tileids.Length < sizeX * sizeY - 1)
            {
                Exception e = new Exception("Zu wenige Tiles für Map definiert!");
                throw e;
            }
            
            for (int y = 0; y < sizeY; y++)
            {
                for(int x = 0; x < sizeX; x++)
                {
                    tiles[y, x] = Tile.Tiles[tileids[y * sizeY + x]].GetCopy();
                    tiles[y, x].enter(Unit.Units["0"]);
                }
            }

            
        }

        public Map()
        {
            this.id = "";
            this.sizeX = 16;
            this.sizeY = 9;
            this.setMapTiles("R1");
        }

        // Setzt alle Kacheln der Map die übergebene Kachel id
        public void setMapTiles(String id)
        {
            tiles = new Tile[sizeY, sizeX];

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    this.tiles[y, x] = Tile.Tiles[id].GetCopy();
                }
            }
        }

        public int getSizeX()
        {
            return this.sizeX;
        }

        public int getSizeY()
        {
            return this.sizeY;
        }

        public void Register()
        {
            Maps.Add(id, this);
        }

        public void Draw(SpriteBatch batch)
        {
            for(int y = 0; y < sizeY; y++)
            {
                for(int x = 0; x < sizeX; x++)
                {
                    tiles[y, x].SetPos(x * 64, y * 64);
                    tiles[y, x].Draw(batch);
                }
            }
        }

        public void Draw(Graphics g)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    tiles[y, x].SetPos(x * 64, y * 64);
                    tiles[y, x].Draw(g);
                }
            }
        }
        public List<Tile> GetMapTiles()
        {
            List<Tile> tilelist = new List<Tile>();

            foreach(Tile tile in tiles)
            {
                tilelist.Add(tile);
            }

            return tilelist;
        }

        public void writeToFile(String path)
        {
            XmlDocument doc = null;
            XmlElement maps = null;

            if (File.Exists(path) == false)
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement("doc");
                maps = doc.CreateElement("maps");
            }
            else
            {
                doc = new XmlDocument();
                doc.Load(path);
                maps = (XmlElement)doc.SelectSingleNode("/doc/maps/");
            }
            XmlElement map = doc.CreateElement("map");
            XmlElement title = doc.CreateElement("title");
            title.InnerText = this.title;
            XmlElement SizeX = doc.CreateElement("SizeX");
            SizeX.InnerText = this.sizeX.ToString();
            XmlElement SizeY = doc.CreateElement("SizeY");
            SizeY.InnerText = this.sizeY.ToString();
            XmlElement tiles = doc.CreateElement("tiles");

            String tileIdRange = "";
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    tileIdRange = tileIdRange + this.tiles[y, x].GetId()+",";
                }
            }
            tiles.InnerText = tileIdRange;

            map.SetAttribute("id", id);
            map.AppendChild(title);
            map.AppendChild(SizeX);
            map.AppendChild(SizeY);
            map.AppendChild(tiles);

            maps.AppendChild(map);
            doc.Save(path);
        }

        public void Init()
        {
            bgSound.startPlaying();
        }

        public static Dictionary<String, Map> Maps = new Dictionary<string, Map>();
    }
}
