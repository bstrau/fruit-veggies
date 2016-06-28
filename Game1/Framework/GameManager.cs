using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1.Content;
using Game1.Framework;
using System;
using System.Drawing;

namespace Game1
{
    public enum GAMESTATE
    {
        MENU,
        MAP
    }

    public class GameManager
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        // Spieler
        public static Player playerOne;
        public static Player playerTwo;

        // Cursor 
        public static Cursor cursor;

        // Spiel Runden
        public static int gameRounds;

        // Aktuell gewählte Map
        Map currentMap;

        // Spieler, der in der aktuellen Runde zieht
        public static Player currentPlayer;

        // Menüs
        Pane mainMenu;
        MapEditor editor;
        Pane playerBar; 

        // States
        KeyboardState oldState;
        MouseState oldMouseEvent;
        public static GAMESTATE gameState;

        public GameManager(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;

            // Spieler Initialisieren
            playerOne = new Player("1");
            playerTwo = new Player("2");
            playerOne.SetResourcePoints(500);
            playerTwo.SetResourcePoints(500);

            // Aktuell gewählte Map
            currentMap = Map.Maps["0"];
            currentMap.Init();
            currentMap.MuteSound(true);

            cursor = new Cursor(currentMap, currentMap.GetMapTiles()[25], spriteBatch);

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

            foreach(Tile tile in currentMap.GetMapTiles())
            {
                if(tile.GetTileType() == Tile.TileType.BASE)
                {
                    ((BaseTile)tile).setOwnership();
                }
            }

            // Map Editor
            editor = new MapEditor();

            // Menüs initialisieren
            InitializePanes();

            gameState = GAMESTATE.MAP;
            onTurnBegin();
        }

        // Map Chosser
        public Map chooseMap() {

            return null;
        }

        public void InitializePanes()
        {
           

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

            // Main Menü
            mainMenu = new Pane("menu", "mainMenue");
            mainMenu.setDimensions(800, 500);
            mainMenu.setPosition(50, 50);
            mainMenu.setFont(new FontObject(spriteFont));
            mainMenu.AddPane(editor);
            mainMenu.AddPane(mute);
            // Register Panes
            mainMenu.Register();

            Pane roundDisplay = new Pane("menuoption", "rounddisplay");
            roundDisplay.setPosition(currentMap.getSizeX() * 64 / 5 * 2, 0);
            roundDisplay.setDimensions(currentMap.getSizeX() * 64 / 5, 64);
            roundDisplay.setFont(new FontObject(Game1.font));
            roundDisplay.addText("Rounds:" + GameManager.gameRounds.ToString(), new Point(10, 10));
            
            playerBar = new Pane("menu", "playerbar");
            playerBar.setPosition(0, currentMap.getSizeY() * 64);
            playerBar.setDimensions(currentMap.getSizeX() * 64, 64);
            playerBar.AddPane(roundDisplay);

            playerOne.AddPlayerPane(playerBar,new Point(0, 0));

            roundDisplay.Show();
            playerBar.Show();
        }

        // Map und deren Komponenten zeichnen
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

        // Paned Zeichnen
        public void RenderPanes()
        {
            Pane.DrawPanes(spriteBatch);
        }

        // Events an Bearbeitende Instanzen weitergeben
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
                    Pane[] copy = new Pane[Pane.currentPanes.Count];
                    Pane.currentPanes.CopyTo(copy);
                    foreach (Pane pane in copy)
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

        // Mapeditor anzeigen. Wird über eine Tastenkombi aufgerufen, die in Update definiert ist
        public void ShowMapEditor(object sender, EventArgs eventArgs)
        {
            editor.Show();
        }

        // Objekte in den Restmüll
        public void WasteStuff()
        {
            editor.Close();
        }

        // Runden Manager
        public void onTurnBegin()
        {
            if (currentPlayer == null)
            {
                currentPlayer = playerOne;
            }
            else {
                // Aktuellen Player wechseln
                if (currentPlayer == playerOne)
                    currentPlayer = playerTwo;
                else
                    currentPlayer = playerOne;
            }

            foreach (Tile tile in currentMap.GetMapTiles())
            {
                tile.onTurnBegin(currentPlayer);
            }

            gameRounds++;
        }
    }
}
