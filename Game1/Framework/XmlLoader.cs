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
    public class XmlLoader
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
                
                // Tile erzeugen und registrieren
                switch (type)
                {        
                    case "RESSOURCE":
                        Tile ressouceTile = new RessourcesTile(Tile.TileType.RESSOURCE, node);
                        break;

                    case "TREASURE":
                        Tile treasureTile = new TreasureTile(Tile.TileType.TREASURE, node);
                        break;
                    case "BASE":
                        Tile baseTile = new BaseTile(Tile.TileType.BASE, node);
                        break;
                    case "FACTORY":
                        Tile factory = new FactoryTile(Tile.TileType.FACTORY, node);
                        break;
                    case "DEFAULT":
                    default:
                        Tile defaultTile = new DefaultTile(Tile.TileType.DEFAULT, node);
                        break;
                }
            }
        }

        public static void loadAllMaps(String xmlfilepath)
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

            XmlNodeList maps = xdoc.GetElementsByTagName("map");

            foreach (XmlNode node in maps)
            {
                Map map = new Map(node);
                map.Register();
            }
        }

        public static Map loadMap(String xmlfilepath)
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

            XmlNodeList maps = xdoc.GetElementsByTagName("map");

            XmlNode xmlMap = null;

            if (maps == null)
            {
                System.Windows.Forms.MessageBox.Show("Keine Map gefunden");
                
            }

            if (maps.Count >= 1)
            {
                xmlMap = maps[0];
            }

            return new Map(xmlMap);
             
           
        }
        public static void loadAllUnits(String xmlfilepath)
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

            XmlNodeList units = xdoc.GetElementsByTagName("unit");

            foreach (XmlNode node in units)
            {
                String type = node.Attributes.GetNamedItem("type").Value;

                // Unit erzeugen und registrieren
                switch (type)
                {
                    case "DEFAULT":
                        Unit defaultTile = new Unit(Unit.UnitType.DEFAULT, node);
                        break;

                    case "APPLE":
                        Unit tile = new AttackUnit(Unit.UnitType.APPLE, node);
                        break;

                    case "BANANA":
                        Unit treasureTile = new AttackUnit(Unit.UnitType.BANANA, node);
                        break;

                }
            }
        }

    }
}
