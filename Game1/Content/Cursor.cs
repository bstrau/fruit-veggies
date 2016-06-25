using Microsoft.Xna.Framework.Graphics;
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

        SpriteBatch batch;
        
        Player currentPlayer;
        Map currentMap;
        Unit currentUnit;
        Tile currentTile;
        CURSORSTATE currentCursorState;

        public Cursor(Map cm, Tile ct, SpriteBatch batch)
        {
            this.batch = batch;
            currentMap = cm;
            currentTile = ct;
            currentUnit = null;
        }

        public Tile getCurrentTile()
        {
            return this.currentTile;
        }

        public void setCurrentTile(Tile currentTile)
        {
            this.currentTile = currentTile;
        }

        public void setPlayer(Player cp)
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

                        //GraphicsObject mark = GraphicsObject.graphicObjects["marked"];
                        //mark.SetPos(neighbour.getPos());
                        //mark.setDimension(64,64);
                        //mark.Draw(batch);
                        tiles.Add(neighbour);
                    }
                    findWay(tiles, neighbour, movepoints - 1);
                }
            }
        }

        public void onClick(MouseState e)
        {
            // TODO: Noch nicht geklärt
            //currentTile = currentMap.getTilebyPos(e.X, e.Y);
        }
    }
}
