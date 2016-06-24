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
            RIGHT,
            DOWN,
            LEFT
        };

        Spieler currentPlayer;
        Map currentMap;
        Unit currenUnit;
        Tile currentTile;

        public Cursor(Map cm, Tile ct)
        {
            currentMap = cm;
            currentTile = ct;
            currenUnit = null;
        }

        public Tile getCurrentTile()
        {
            return this.currentTile;
        }

        public void setCurrentTile(Tile currentTile)
        {
            this.currentTile = currentTile;
        }

        public void setPlayer(Spieler cp)
        {
            currentPlayer = cp;
        }

        public void findWay(List<Tile> tiles, Tile waypoint, DIRECTION direction, int movepoints, int directionChanges)
        {
            Tile neighbour = currentMap.getNeighbour(waypoint, direction);
            if (movepoints == 0 || ( directionChanges == 3 && neighbour == null))
            {
                return;
            }

            if (direction == DIRECTION.LEFT)
                direction = DIRECTION.UP;
                
            else
                direction++;

            directionChanges++;

            if (neighbour != null)
            {    
                if (tiles.Contains(waypoint) == false)
                {
                    tiles.Add(waypoint);        
                }
               
                findWay(tiles, neighbour, direction, movepoints - 1, 0);
                
            }
            else {
                findWay(tiles, waypoint, direction, movepoints, directionChanges);
            }
            
        }

        public void onClick(MouseState e)
        {

        }
    }
}
