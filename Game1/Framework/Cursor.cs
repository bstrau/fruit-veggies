using Microsoft.Xna.Framework.Graphics;
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
            ACTION,
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
        GraphicsObject fightTarget;

        Map currentMap;
        Unit currentUnit;

        Tile currentTile;
        Tile originTile;

        List<Tile> reachableTiles;
        List<Tile> attackableTiles;

        CURSORSTATE cursorState;
        CURSORSTATE previosState;

        public Cursor(Map cm, Tile ct, SpriteBatch batch)
        {
            this.batch = batch;
            currentMap = cm;
            currentTile = ct;
            currentUnit = null;
            originTile = null;

            reachableTiles = new List<Tile>();
            attackableTiles = new List<Tile>();

            graphics = GraphicsObject.graphicObjects["cursor"];
            target = GraphicsObject.graphicObjects["target"];
            fightTarget = GraphicsObject.graphicObjects["fightTarget"];

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

        private void setCursorState(CURSORSTATE state)
        {
            previosState = cursorState;
            cursorState = state;
        }

        public void findWay(List<Tile> tiles, Tile waypoint, int movepoints)
        {
            // Abbruchbedingung
            if (movepoints == 0)
                return;

            
            for(DIRECTION direction = DIRECTION.UP; direction < DIRECTION.MAXDIRECTION; direction++)
            {
                Tile neighbour = null;

                neighbour = currentMap.getNeighbour(waypoint, direction);
                if (neighbour != null)
                {
                    // Nächstes erreichbares Tile in Liste aufnehmen
                    if (!tiles.Contains(neighbour) && neighbour.Reachable())
                    {
                        tiles.Add(neighbour);
                    }
                    if(neighbour.Passable(currentUnit))
                        findWay(tiles, neighbour, movepoints - 1);
                }
            }
        }
        public void findFight(List<Tile> tiles)
        {
            attackableTiles.Clear();
            Tile neighbour;
            for (DIRECTION direction = DIRECTION.UP; direction < DIRECTION.MAXDIRECTION; direction++)
            {
                neighbour = currentMap.getNeighbour(currentTile, direction);
                if (neighbour != null && attackableTiles.Contains(neighbour) == false &&
                     neighbour.getOccupant() != null && neighbour.getOccupant().getPlayer() != GameManager.currentPlayer)
                {
                    attackableTiles.Add(neighbour);
                }
            }
        }
        
        public void Draw()
        {
            foreach (Tile tile in attackableTiles)
            {
                fightTarget.SetPos(tile.getPos());
                fightTarget.Draw(batch);
            }

            foreach (Tile tile in reachableTiles)
            {
                target.SetPos(tile.getPos());
                target.Draw(batch);
            }

            graphics.SetPos(currentTile.getPos());
            graphics.Draw(batch);
        }

        public void onLeftClick(Point pos)
        {
            if (cursorState == CURSORSTATE.SELECT)
            {
                if (currentUnit != null && currentUnit.getPlayer() == GameManager.currentPlayer && currentUnit.MayMove())
                {
                    // Prüfe, ob gegnerische Einheit neben CurrentTile steht
                    // Nimm diese Unit und die CurrentUnit, übergib sie einem FightManager
                    // Wenn gekämpft, dann return
                    findFight(attackableTiles);
                    setCursorState(CURSORSTATE.MOVE);
                    originTile = currentTile;
                    findWay(reachableTiles, originTile, currentUnit.getMovePoints());
                    cursorState = CURSORSTATE.MOVE;
                }
                else
                {
                    currentTile.onClick(pos);
                }
            }
            else if (cursorState == CURSORSTATE.MOVE)
            {
                // Schau, ob ein Kampftile ausgewählt wurde
                setCursorState(CURSORSTATE.SELECT);
                if (attackableTiles.Contains(currentTile))
                {
                    AttackUnit attacker = (AttackUnit)currentUnit;
                    AttackUnit defender = (AttackUnit)currentTile.getOccupant();
                    GameManager.fightManager.Fight(attacker, defender);
                    
                    attacker.Moved();
                    attackableTiles.Clear();
                }

                if (reachableTiles.Contains(currentTile))
                {
                    currentTile.enter(originTile.leave());
                    findFight(attackableTiles);
                    if(attackableTiles.Count != 0)
                    {
                        cursorState = CURSORSTATE.ACTION;
                    }
                }
                currentUnit.Moved();
                reachableTiles.Clear();
            }
            else if (cursorState == CURSORSTATE.ACTION)
            {
                if (attackableTiles.Contains(currentTile))
                {
                    AttackUnit attacker = (AttackUnit)currentUnit;
                    AttackUnit defender = (AttackUnit)currentTile.getOccupant();
                    GameManager.fightManager.Fight(attacker, defender);

                    attacker.Moved();
                    attackableTiles.Clear();
                    cursorState = CURSORSTATE.SELECT;
                }
            }
        }

        public void onMouseMove(Point pos)
        {
            // FIXME: Es kann momentan noch passieren, das die Positon der Maus negativ wird. Das sollte verhindert werden!
            if (currentMap.getTilebyPos(pos.X / 64, pos.Y / 64) != null)
            {
                if(currentUnit != null)
                    currentUnit.onCursorLeave();

                currentTile = currentMap.getTilebyPos(pos.X / 64, pos.Y / 64);
                if (cursorState == CURSORSTATE.SELECT)
                {
                    currentUnit = currentTile.getOccupant();
                    if (currentUnit != null)
                    {
                        currentUnit.onMouseMove(currentTile.getPos());
                    }
                }
            }
        }
    }
}
