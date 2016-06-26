﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        GraphicsObject graphics;
        GraphicsObject target;

        Player currentPlayer;
        Map currentMap;
        Unit currentUnit;

        Tile currentTile;
        Tile originTile;

        List<Tile> reachableTiles;

        CURSORSTATE cursorState;

        public Cursor(Map cm, Tile ct, SpriteBatch batch)
        {
            this.batch = batch;
            currentMap = cm;
            currentTile = ct;
            currentUnit = null;
            originTile = null;

            reachableTiles = new List<Tile>();

            graphics = GraphicsObject.graphicObjects["cursor"];
            target = GraphicsObject.graphicObjects["target"];

            graphics.setDimension(64, 64);
            target.setDimension(64, 64);
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
        
        public void Draw()
        {
            if(cursorState == CURSORSTATE.MOVE)
            {
                foreach(Tile tile in reachableTiles)
                {
                    target.SetPos(tile.getPos());
                    target.Draw(batch);
                }
            }

            graphics.SetPos(currentTile.getPos());
            graphics.Draw(batch);
        }

        public void onLeftClick(Point pos)
        {
            if(cursorState == CURSORSTATE.MOVE)
            {
                if (reachableTiles.Contains(currentTile))
                {
                    currentTile.enter(originTile.leave());
                }
                reachableTiles.Clear();
                cursorState = CURSORSTATE.SELECT;
            }

            else if(cursorState == CURSORSTATE.SELECT) 
            {
                if (currentUnit != null)
                {
                    cursorState = CURSORSTATE.MOVE;
                    originTile = currentTile;
                    findWay(reachableTiles, originTile, currentUnit.getMovePoints());
                }
            }
        }

        public void onMouseMove(Point pos)
        {
            // FIXME: Es kann momentan noch passieren, das die Positon der Maus negativ wird. Das sollte verhindert werden!
            if (currentMap.getTilebyPos(pos.X / 64, pos.Y / 64) != null)
            {
                currentTile = currentMap.getTilebyPos(pos.X / 64, pos.Y / 64);
                if (cursorState == CURSORSTATE.SELECT)
                {
                    currentUnit = currentTile.getOccupant();
                }
            }
        }
    }
}