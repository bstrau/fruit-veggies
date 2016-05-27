using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class XmlLoader
    {
        public static Dictionary<String, Tile> loadAllTiles(String xmlfilepath)
        {
            Dictionary<String,Tile> ret = new Dictionary<String,Tile>();

            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.Load(xmlfilepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ret;
            } 

            XmlNodeList tiles = xdoc.GetElementsByTagName("tile");
            
            foreach(XmlNode node in tiles)
            {
                String id = node.Attributes.GetNamedItem("id").Value;
                String title = node.SelectSingleNode("title").InnerText;
                bool accessible = Convert.ToBoolean(node.SelectSingleNode("accessible").InnerText);
                String graphic = node.SelectSingleNode("graphic").InnerText;

                // Gehört hier eigentlich nicht hin, da die Tiles nicht über diese Attribute verfügen
                int cash_rounds = Convert.ToInt32(node.SelectSingleNode("cash/rounds").InnerText);
                int cash_amount = Convert.ToInt32(node.SelectSingleNode("cash/amount").InnerText);

                GraphicsObject g = GraphicsObject.graphicObjects[graphic];
                if (g != null) {
                    ret.Add(id, new Tile(g, 0, 0, Convert.ToBoolean(accessible)));
                }
            }
            return ret;
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
