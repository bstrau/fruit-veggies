using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game1.Content
{
    class BaseTile : FactoryTile
    {
        string ownerId;

        public BaseTile(TileType type, XmlNode node): base(type,node)
        {
            ownerId = node.SelectSingleNode("ownerid").InnerText; 
        }

        public BaseTile(BaseTile baseTile) : base(baseTile)
        {
            this.ownerId = baseTile.ownerId;
        }

        public override Tile GetCopy()
        {
            return new BaseTile(this);
        }

        public void setOwnership()
        {
            if(GameManager.playerOne.GetId() == ownerId)
            {
                owner = GameManager.playerOne;
            }
            else {
                owner = GameManager.playerTwo;
            }
        }
    }
}
