using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Game1.Content;
using Game1.Framework;

namespace Game1.Content
{
    public class Map
    {
        private String id;
        private String title;
        private int sizeX, sizeY;
        private Tile[,] tiles;
        private SoundObject bgSound;

        private Pane playerBar;

        // künftig public Map(XMLNode node)
        public Map(XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            title = node.SelectSingleNode("title").InnerText;
            sizeX = Convert.ToInt32(node.SelectSingleNode("SizeX").InnerText);
            sizeY = Convert.ToInt32(node.SelectSingleNode("SizeY").InnerText);

            String sound = node.SelectSingleNode("sound") .InnerText;
            bgSound = SoundObject.soundObjects[sound];
            bgSound.setLooped(true);

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
                }
            }  
        }

        public Map()
        {
            this.id = "";
            this.title = "New";
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

        public void setTileTo(int xPos, int yPos, Tile t)
        {
            tiles[yPos, xPos] = t;
        }

        public void writeToFile(String path)
        {
            XmlDocument doc = null;
            XmlElement maps = null;
            XmlElement root = null;

           
            doc = new XmlDocument();
            root = doc.CreateElement("doc");
            maps = doc.CreateElement("maps");
            

            foreach (Map curmap in Map.Maps.Values)
            {
                XmlElement map = doc.CreateElement("map");

                XmlElement title = doc.CreateElement("title");
                title.InnerText = curmap.title;

                XmlElement sound = doc.CreateElement("sound");
                sound.InnerText = SoundObject.soundObjects.First(x => x.Value == curmap.bgSound).Key;

                XmlElement SizeX = doc.CreateElement("SizeX");
                SizeX.InnerText = curmap.sizeX.ToString();

                XmlElement SizeY = doc.CreateElement("SizeY");
                SizeY.InnerText = curmap.sizeY.ToString();

                XmlElement tiles = doc.CreateElement("tiles");
                String tileIdRange = "";
                for (int x = 0; x < sizeX; x++)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        if (y != 0 || x != 0)
                        {
                            tileIdRange += ',';
                        }
                        tileIdRange += curmap.tiles[y, x].GetId();
                    }
                }
                tiles.InnerText = tileIdRange;

                map.SetAttribute("id", id);
                map.AppendChild(title);
                map.AppendChild(sound);
                map.AppendChild(SizeX);
                map.AppendChild(SizeY);
                map.AppendChild(tiles);
                
                maps.AppendChild(map);
            }

            root.AppendChild(maps);

            doc.AppendChild(root);

            doc.Save(path);
        }

        public Point getTilePos(Tile tile)
        {
            for(int y = 0; y < sizeY; y++)
            {
                for(int x = 0; x < sizeX; x++)
                {
                    if(tiles[y,x] == tile)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return new Point(-1, -1);
        }

        public Tile findUnit(Unit unit)
        {
            foreach(Tile tile in tiles)
            {
                if(tile.getOccupant() == unit)
                {
                    return tile;
                }
            }

            return null;
        }

        public void Init()
        {
            bgSound.startPlaying();
        }

        public void MuteSound(bool muted)
        {
            if (muted)
                bgSound.stopPlaying();
            else
                bgSound.startPlaying();
        }

        public Tile getNeighbour(Tile tile, Cursor.DIRECTION direction)
        {
            Point pos = this.getTilePos(tile);
            
            if (direction == Cursor.DIRECTION.LEFT && pos.X > 0)     
                return tiles[pos.Y, pos.X - 1];
            
            if (direction == Cursor.DIRECTION.RIGHT && pos.X < sizeX - 1)
                return tiles[pos.Y, pos.X + 1];
            
            if (direction == Cursor.DIRECTION.UP && pos.Y > 0)
                return tiles[pos.Y - 1, pos.X];
            
            if (direction == Cursor.DIRECTION.DOWN && pos.Y < sizeY - 1)
                return tiles[pos.Y + 1 , pos.X];

            else
                return null;
        }

        public void onClick(MouseEventArgs e)
        {
            foreach (Tile t in this.GetMapTiles())
            {
                // Tile Position TODO: Eventuell auslagern. 
                if (t.getPos().X <= e.X && t.getPos().X + 64 >= e.X && t.getPos().Y <= e.Y && t.getPos().Y + 64 >= e.Y)
                {
                    t.onClick(e);
                }
            }
        }

        public void onClick(Microsoft.Xna.Framework.Input.MouseState e)
        {
         
        }


        public Tile getTilebyPos(int x, int y)
        {
            // Wenn x und y Werte zwischen 0 und size
            if((x <= sizeX - 1 && y <= sizeY - 1) && (x >= 0 && y >= 0))
                return tiles[y,x];
            else
                return null;
        }

        public void ToggleSound(object sender, EventArgs eventArgs)
        {
            if(bgSound.muted)
            {
                bgSound.startPlaying();
            }
            else
            {
                bgSound.stopPlaying();
            }
        }

        public void Update()
        {
            // KP: ???
        }

        public static Dictionary<String, Map> Maps = new Dictionary<string, Map>();
    }
}
