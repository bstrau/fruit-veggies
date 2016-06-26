using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class CaptureTile : Tile
    {
        protected int capturepoints;

        public CaptureTile(TileType type, XmlNode node): base(type,node)
        {
            capturepoints = Convert.ToInt32(node.SelectSingleNode("capturepoints").InnerText); 
        }

        public CaptureTile(CaptureTile tile) : base(tile)
        {
            this.capturepoints = tile.capturepoints;

        }
    }
}
