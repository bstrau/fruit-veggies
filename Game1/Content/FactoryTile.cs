using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class FactoryTile : Tile
    {
        public FactoryTile(TileType type, XmlNode node)
            : base(type, node)
        {
 
        }

        //public Unit createUnit(String unit_id)
        //{
        //    return (Unit.Units[unit_id]).GetCopy();
        //}


    }
}
