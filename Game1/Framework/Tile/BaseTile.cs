using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class BaseTile : CaptureTile
    {
        protected int xxx;

        public BaseTile(TileType type, XmlNode node): base(type,node)
        {
            xxx = Convert.ToInt32(node.SelectSingleNode("xxx").InnerText); 
        }

        public BaseTile(BaseTile baseTile) : base(baseTile)
        {
            this.xxx = baseTile.xxx;

        }
    }
}
