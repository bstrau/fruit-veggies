using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class DefaultTile : Tile
    {

        public DefaultTile(TileType type, XmlNode node)
            : base(type, node)
        {
        }

        public DefaultTile(DefaultTile tile) : base(tile)
        {
        }

        public override Tile GetCopy()
        {
            return new DefaultTile(this);
        }
    }
}
