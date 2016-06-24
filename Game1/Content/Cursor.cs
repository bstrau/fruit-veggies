using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    public class Cursor
    {
        public enum CURSORSTATE
        {
            SELECT,
            MOVE,
            ACTION
        };

        public enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        Map currentMap;
        Unit currenUnit;
        Tile currentTile;

        public void findWay(List<Tile> tiles, Tile waypoint, DIRECTION direction, int movepoints)
        {
            Tile neighbour = currentMap.getNeighbour(waypoint, direction);
            if (movepoints == 0 || direction == DIRECTION.RIGHT && neighbour != null)
            {
            }

            tiles.Add(waypoint);
            findWay(tiles, waypoint, direction, movepoints - 1);
        }

        public void onClick(MouseState e)
        {

        }
    }
}
