using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class TreasureTile : Tile
    {
        protected int cash;

        public TreasureTile(TileType type, XmlNode node)
            : base(type, node)
        {
            this.cash = 0; // TODO KP: XML
            Register();
        }

        public TreasureTile(TreasureTile treasureTile) :base(treasureTile)
        {
            this.cash = treasureTile.cash;
        }

        public override Tile GetCopy()
        {
            return new TreasureTile(this);
        }

        public int GetLoot()
        {
            int loot = this.cash;
            this.cash = 0;
            return loot;
        }
    }
}
