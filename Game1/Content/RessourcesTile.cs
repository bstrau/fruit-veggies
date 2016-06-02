using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class RessourcesTile : Tile
    {
        protected int cash_rounds;
        protected int cash_amount;

        public RessourcesTile(TileType type, XmlNode node): base(type,node)
        {
            cash_rounds = Convert.ToInt32(node.SelectSingleNode("cash/rounds").InnerText);
            cash_amount = Convert.ToInt32(node.SelectSingleNode("cash/amount").InnerText);
        }

        public RessourcesTile(RessourcesTile tile) : base(tile)
        {
            this.cash_amount = tile.cash_amount;
            this.cash_rounds = tile.cash_rounds;
        }

        // Translators note: Loot heißt Beute
        public int GetLoot()
        {
            if (cash_rounds > 0)
            {
                cash_rounds--;
                return cash_amount;
            }

            return 0;
        }
    }
}
