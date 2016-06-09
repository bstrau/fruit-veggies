using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    public class Map
    {
        private String id;
        private String title;
        private int sizeX, sizeY;
        private Tile[,] tiles;


        // künftig public Map(XMLNode node)
        public Map(XmlNode node)
        {
            id = node.Attributes.GetNamedItem("id").Value;
            title = node.SelectSingleNode("title").InnerText;
            sizeX = Convert.ToInt32(node.SelectSingleNode("SizeX").InnerText);
            sizeY = Convert.ToInt32(node.SelectSingleNode("SizeY").InnerText);
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

        public List<Tile> GetMapTiles()
        {
            List<Tile> tilelist = new List<Tile>();

            foreach(Tile tile in tiles)
            {
                tilelist.Add(tile);
            }

            return tilelist;
        }

        public static Dictionary<String, Map> Maps = new Dictionary<string, Map>();
    }
}
