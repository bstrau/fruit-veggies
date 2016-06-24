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
            LEFT,
            MAXDIRECTION
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

        public void findWay(List<Tile> tiles, Tile waypoint, int movepoints)
        {
            // Abbruchbedingung
            if (movepoints == 0)
                return;

            
            for(DIRECTION direction = DIRECTION.UP; direction < DIRECTION.MAXDIRECTION; direction++)
            {
                Tile neighbour = null;
                if (direction == DIRECTION.UP)
                {
                    int a = 9;
                }

                neighbour = currentMap.getNeighbour(waypoint, direction);
                if (neighbour != null)
                {
                    // Nächstes erreichbares Tile in Liste aufnehmen
                    if (!tiles.Contains(neighbour))
                    {
                        neighbour.enter(Unit.Units["0"].GetCopy());
                        tiles.Add(neighbour);
                    }
                    findWay(tiles, neighbour, movepoints - 1);
                }
            }
        }

        public void onClick(MouseState e)
        {

        }
    }
}
