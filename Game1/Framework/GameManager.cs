using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1.Content;
using Game1.Framework;
using System;
using System.Drawing;

namespace Game1
{
    enum GAMESTATE
    {
        MENU,
        MAP
    }

    class GameManager
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        // Spieler
        Player playerOne;
        Player playerTwo;

        // Cursor 
        Cursor cursor;

        // Aktuell gewählte Map
        Map currentMap;
         
        Pane mainMenu;

        MapEditor editor;

        // States
        KeyboardState oldState;
        MouseState oldMouseEvent;
        static GAMESTATE gameState;

        public GameManager(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;

            // Spieler Initialisieren
            playerOne = new Player("One");
            playerTwo = new Player("Two");

            // Aktuell gewählte Map
            currentMap = Map.Maps["TEST"];
            currentMap.Init();
            currentMap.MuteSound(true);

            cursor = new Cursor(currentMap, currentMap.GetMapTiles()[25], spriteBatch);
            cursor.setPlayer(playerOne);

            // Test der Units
            Unit a0 = Unit.Units["apple"].GetCopy();
            Unit a1 = Unit.Units["banana"].GetCopy();
            Unit b0 = Unit.Units["carrot"].GetCopy();
            Unit b1 = Unit.Units["potato"].GetCopy();

            a0.setPlayer(playerOne);
            a1.setPlayer(playerOne);
            b0.setPlayer(playerTwo);
            b1.setPlayer(playerTwo);

            currentMap.GetMapTiles()[1].enter(a0);
            currentMap.GetMapTiles()[17].enter(a1);
            currentMap.GetMapTiles()[2].enter(b0);
            currentMap.GetMapTiles()[18].enter(b1);

            // Map Editor
            editor = new MapEditor();

            // Menüs initialisieren
            InitializePanes();

            gameState = GAMESTATE.MAP;
        }

        // Map Chosser
        public Map chooseMap() {

            return null;
        }

        public void InitializePanes()
        {
            // Main Menü
            mainMenu = new Pane("menu","mainMenue");
            mainMenu.setDimensions(800,500);
            mainMenu.setPosition(50,50);
            mainMenu.setFont(new FontObject(spriteFont));

            // LevelEditor öffnen
            Pane editor = new Pane("menuoption", "editor");
            editor.setPosition(new System.Drawing.Point(10, 10));
            editor.setDimensions(780, 50);
            editor.setFont(new FontObject(spriteFont));
            editor.addText("Map-Editor starten", new Point(10, 10));
            editor.Clicked += ShowMapEditor;

            // Music abstellen
            Pane mute = new Pane("menuoption", "mute");
            mute.setPosition(new System.Drawing.Point(10, 70));
            mute.setDimensions(780, 50);
            mute.setFont(new FontObject(spriteFont));
            mute.addText("Musik umschalten", new Point(10, 10));
            mute.Clicked += currentMap.ToggleSound;

            mainMenu.AddPane(editor);
            mainMenu.AddPane(mute);
            // Register Panes
            mainMenu.Register();
        }

        public void Render()
        {
            spriteBatch.Begin();

            currentMap.Draw(spriteBatch);

            RenderPanes();

            // Cursor nur Zeichen, wenn kein Menu angezeigt wird
            if(gameState == GAMESTATE.MAP)
                cursor.Draw();

            spriteBatch.End();
 
        }

        public void RenderPanes()
        {
            Pane.DrawPanes(spriteBatch);
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
            if (oldState.IsKeyDown(Keys.Tab) && newkeyboardEvent.IsKeyUp(Keys.Tab))
            {
                if (mainMenu.isVisible())
                {
                    mainMenu.Hide();
                    gameState = GAMESTATE.MAP;
                }
                else
                {
                    mainMenu.Show();
                    gameState = GAMESTATE.MENU;
                }
            }

            if ((oldState.IsKeyDown(Keys.LeftControl) && oldState.IsKeyDown(Keys.M)) && (newkeyboardEvent.IsKeyUp(Keys.LeftControl) || newkeyboardEvent.IsKeyUp(Keys.M)))
            {
                if (editor.Visible)
                    editor.Hide();
                else
                    editor.Show();
            }
            System.Drawing.Point pos = new System.Drawing.Point(newMouseEvent.Position.X, newMouseEvent.Position.Y);

            // Left Click
            if (oldMouseEvent.LeftButton == ButtonState.Pressed && newMouseEvent.LeftButton == ButtonState.Released)
            {
                if (gameState == GAMESTATE.MENU)
                {
                    foreach(Pane pane in Pane.currentPanes)
                    {
                        if (pane.isHit(pos))
                            pane.onClick(pos);
                    }
                }
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

        public void ShowMapEditor(object sender, EventArgs eventArgs)
        {
            editor.Show();
        }

        // Objekte in den Restmüll
        public void WasteStuff()
        {
            editor.Close();
        }
    }
}
