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
                    case "Ressource":
                        Tile tile = new RessourcesTile(Tile.TileType.RESSOURCE, node);

                        // Tile wird eigentlich vom Konstruktor registriert  
                        //Tile.Tiles.Add(tile.GetId(), tile);
                        break;
                }
            }
        }

        public static Dictionary<String, String> loadAllUnits(String xmlfilepath)
        {
            Dictionary<String, String> ret = new Dictionary<String, String>();

            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(xmlfilepath);

            XmlNodeList units = xdoc.GetElementsByTagName("unit");

            for(int i=0; i< units.Count;i++)
            {
                XmlElement unit = (XmlElement)units.Item(i);
                String id = unit.GetAttribute("id");
                String title = units.Item(i).SelectSingleNode("/title").Value;
                String accessible = units.Item(i).SelectSingleNode("/accessible").Value;
                String graphic = units.Item(i).SelectSingleNode("/graphic").Value;
                String cash_rounds = units.Item(i).SelectSingleNode("/cash/rounds").Value;
                String cash_amount = units.Item(i).SelectSingleNode("/cash/amount").Value;
                
                //GraphicsObject test = new GraphicsObject(Content.Load<Texture2D>(graphic));
                //dict.Add(id, new Tile(, 0, 0, Convert.ToBoolean(accessible));
            }
            return ret;
        }

        public static void LoadMap()
        { 
        }
    }
}
