using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game1
{
    class GameManager
    {
        SpriteBatch spriteBatch;

        // Spieler
        Player playerOne;
        Player playerTwo;

        // Cursor 
        Cursor cursor;

        // Aktuell gewählte Map
        Map currentMap;

        Pane mainMenu;
        Pane factoryMenu;
        Pane unitMenu;

        MapEditor editor;

        // States
        KeyboardState oldState;
        MouseState oldMouseEvent;

        // Das Oberste Pane auf dem Bildschirm
        //Pane frontPane; // KP: Nur eine Idee

        public GameManager(SpriteBatch spriteBatch)
        {
            // Menüs initialisieren
            this.InitializePanes();
            this.spriteBatch = spriteBatch;

            // Spieler Initialisieren
            playerOne = new Player("One");
            playerTwo = new Player("Two");

            // Aktuell gewählte Map
            currentMap = Map.Maps["0"];
            currentMap.Init();
            currentMap.MuteSound(false); // Damit sich der Herr Straus freut: false
            //currentMap.MuteSound(true);

            cursor = new Cursor(currentMap, currentMap.GetMapTiles()[25], spriteBatch);
            cursor.setPlayer(playerOne);
            cursor.getCurrentTile().enter(Unit.Units["1"]);

            List<Tile> availTiles = new List<Tile>();
            cursor.findWay(availTiles, cursor.getCurrentTile(), 5);

            // TESTS FÜR NEUE METHODE GET TILE POS BESTANDEN
            //Tile t0 = currentMap.getTilebyPos(0, 0);
            //Tile t1 = currentMap.getTilebyPos(1, 2);
            //Tile t2 = currentMap.getTilebyPos(28, 2);

            // Map Editor
            editor = new MapEditor();
        }

        // Map Chosser
        public Map chooseMap() {

            return null;
        }

        public void InitializePanes()
        {
            // Main Menü
            mainMenu = new Pane("menu","mainMenue");
            mainMenu.AddPane(new Pane("menuoption","start"));
            mainMenu.AddPane(new Pane("menuoption", "close"));
            mainMenu.setDimensions(800,500);
            mainMenu.setPosition(50,50);

            // LevelEditor Menu
            Pane editorMenu = new Pane("menuoption", "editor");
            editorMenu.setPosition(new System.Drawing.Point(10, 10));
            editorMenu.setDimensions(880, 80);
            editorMenu.AddPane(editorMenu);

            // Factory Menü
            factoryMenu = new Pane("menu", "factoryMenue");
            factoryMenu.setDimensions(200, 300);
            factoryMenu.setPosition(20, 20);

            unitMenu = new Pane("menu","unitMenue");
            unitMenu.setDimensions(200,200);
            unitMenu.setPosition(10,10);

            // Register Panes
            mainMenu.Register();
            factoryMenu.Register();
            unitMenu.Register();
        }

        public void Render()
        {
            spriteBatch.Begin();
            //mainMenu.Draw(spriteBatch);
            currentMap.Draw(spriteBatch);
            cursor.Draw();
            spriteBatch.End();
 
        }

        public void Update()
        {            
            KeyboardState newKeyEvent = Keyboard.GetState();
            MouseState newMouseEvent = Mouse.GetState();

            // Events, die mit den Menus zu tun haben
            this.MenuEvents(newKeyEvent, newMouseEvent);
            //Events, die mit dem Spiel zu tun haben
            this.GameEvents(newKeyEvent, newMouseEvent);

            oldState = newKeyEvent;
            oldMouseEvent = newMouseEvent;
        }

        /// <summary>
        /// Soll alle Events enthalten die mit den Panes oder Menus zu tun haben
        /// </summary>
        public void MenuEvents(KeyboardState newkeyboardEvent, MouseState newMouseEvent ) 
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newkeyboardEvent.IsKeyDown(Keys.Escape))
                //Exit();

                if (oldState.IsKeyDown(Keys.Tab) && newkeyboardEvent.IsKeyUp(Keys.Tab))
                {
                    if (mainMenu.isVisible())
                        mainMenu.Hide();
                    else
                        mainMenu.Show();
                }

            //&& newState.IsKeyUp(Keys.LeftControl & Keys.M)
            if ((oldState.IsKeyDown(Keys.LeftControl) && oldState.IsKeyDown(Keys.M)) && (newkeyboardEvent.IsKeyUp(Keys.LeftControl) || newkeyboardEvent.IsKeyUp(Keys.M)))
            {
                if (editor.Visible)
                    editor.Hide();
                else
                    editor.Show();
            }
        }

        /// <summary>
        /// Soll alle Events enthalten die mit dem Spiel zu tun haben
        /// </summary>
        public void GameEvents(KeyboardState newkeyboardEvent, MouseState newMouseEvent)
        {
            // Left Click
            if (oldMouseEvent.LeftButton == ButtonState.Pressed && newMouseEvent.LeftButton == ButtonState.Released)
            {
                cursor.onLeftClick(new System.Drawing.Point(newMouseEvent.Position.X, newMouseEvent.Position.Y));
            }

            // MouseMove
            if (oldMouseEvent.Position != newMouseEvent.Position)
            {
                cursor.onMouseMove(new System.Drawing.Point(newMouseEvent.Position.X, newMouseEvent.Position.Y));
            }
        }


        // Objekte in den Restmüll
        public void WasteStuff()
        {
            editor.Close();
        }

    }
}
