using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
// test
namespace Game1.Content
{
    class XmlLoader
    {
        public static void loadAllTiles(String xmlfilepath)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.Load(xmlfilepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            } 

            XmlNodeList tiles = xdoc.GetElementsByTagName("tile");
            
            foreach(XmlNode node in tiles)
            {
                String type = node.Attributes.GetNamedItem("type").Value;
                switch (type)
                {
                    case "DEFAULT":
                        Tile defaultTile = new DefaultTile(Tile.TileType.DEFAULT, node);
                        break;

                    case "RESSOURCE":
                        Tile tile = new RessourcesTile(Tile.TileType.RESSOURCE, node);

                        break;

                    case "TREASURE":
                        Tile treasureTile = new TreasureTile(Tile.TileType.TREASURE, node);
                        break;
                    
                }
            }
        }

    }
}
