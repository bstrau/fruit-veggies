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

        public static int gameRounds;

        // Aktuell gewählte Map
        Map currentMap;

        // Spieler, der in der aktuellen Runde zieht
        public static Player currentPlayer;

        Pane mainMenu;

        MapEditor editor;

        // States
        KeyboardState oldState;
        MouseState oldMouseEvent;
        public static GAMESTATE gameState;

        // Menüs
        Pane playerBar; 

        public static FightManager fightManager;

        // Runden Display
        Text roundDisplayString;

        public GameManager(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;

            // Spieler Initialisieren
            playerOne = new Player("1");
            playerTwo = new Player("2");
            playerOne.SetTitle("Player One");
            playerTwo.SetTitle("Player Two");

            playerOne.SetResourcePoints(500);
            playerTwo.SetResourcePoints(500);

            // Aktuell gewählte Map
            currentMap = Map.Maps[""];
            currentMap.Init();
            currentMap.MuteSound(true);

            fightManager = new FightManager(currentMap);
            
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
            currentMap.GetMapTiles()[4].enter(b0);
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

            onTurnBegin(this, EventArgs.Empty);
        }

        // Map Chosser
        public Map chooseMap() {

            return null;
        }

        public void InitializePanes()
        {
            FontObject font = new FontObject(spriteFont);


            // Main Menü
            mainMenu = new Pane("menu","mainMenue");
            mainMenu.setDimensions(8*64, 5*64);
            mainMenu.setPosition(4*64, 50);
            mainMenu.setFont(font);

            // LevelEditor öffnen
            Pane editor = new Pane("menuoption", "editor");
            editor.setPosition(new System.Drawing.Point(10, 10));
            editor.setDimensions(8*64-20, 50);
            editor.setFont(font);
            editor.addText("Map-Editor starten", new Point(10, 10));
            editor.Clicked += ShowMapEditor;

            // Music abstellen
            Pane mute = new Pane("menuoption", "mute");
            mute.setPosition(new System.Drawing.Point(10, 70));
            mute.setDimensions(8 * 64 - 20, 50);
            mute.setFont(font);
            mute.addText("Musik umschalten", new Point(10, 10));
            mute.Clicked += currentMap.ToggleSound;

            // Map wählen
            Pane chooseMap = new Pane("menuoption", "chooseMap");
            chooseMap.setPosition(new System.Drawing.Point(10, 130));
            chooseMap.setDimensions(8 * 64 - 20, 50);
            chooseMap.setFont(font);
            chooseMap.addText("Map auf Datei Laden", new Point(10, 10));
            chooseMap.Clicked += openMapChooser;

            mainMenu.AddPane(chooseMap);
            mainMenu.AddPane(editor);
            mainMenu.AddPane(mute);

            // Register Panes
            mainMenu.Register();

            roundDisplayString = new Text(gameRounds.ToString(),100,40);
            Pane roundDisplay = new Pane("menuoption", "rounddisplay");
            roundDisplay.setPosition(currentMap.getSizeX() * 64 / 5 * 2, 0);
            roundDisplay.setDimensions(currentMap.getSizeX() * 64 / 5, 64);
            roundDisplay.setFont(font);
            roundDisplay.addText("Rounds", new Point(80, 26));
            roundDisplay.addText(roundDisplayString);
            
            
            // Newt Round 
            Pane nextRoundDisplay = new Pane("menugreen", "nextrounddisplay");
            nextRoundDisplay.setPosition(currentMap.getSizeX() * 64 / 5 * 2, 0);
            nextRoundDisplay.setDimensions(currentMap.getSizeX() * 64 / 5 , 24);
            nextRoundDisplay.setFont(font);
            nextRoundDisplay.addText("Weiter", new Point(80, 2));
            nextRoundDisplay.Clicked += onTurnBegin;
           
            nextRoundDisplay.Show();

            playerBar = new Pane("menu", "playerbar");
            playerBar.setPosition(0, currentMap.getSizeY() * 64);
            playerBar.setDimensions(currentMap.getSizeX() * 64, 64);
            playerBar.AddPane(roundDisplay);
            playerBar.AddPane(nextRoundDisplay);

            playerOne.AddPlayerPane(playerBar,new Point(0, 0), new Size( currentMap.getSizeX() *64 / 4 , 64));
            playerTwo.AddPlayerPane(playerBar, new Point( currentMap.getSizeX() *64 / 4 *3, 0), new Size(currentMap.getSizeX() * 64 / 4, 64));

            roundDisplay.Show();
            playerBar.Show();
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
            if (!this.MenuEvents(newKeyEvent, newMouseEvent))
            {
                //Events, die mit dem Spiel zu tun haben
                this.GameEvents(newKeyEvent, newMouseEvent);
            }
            
            oldState = newKeyEvent;
            oldMouseEvent = newMouseEvent;
        }

        /// <summary>
        /// Soll alle Events enthalten die mit den Panes oder Menus zu tun haben
        /// </summary>
        public bool MenuEvents(KeyboardState newkeyboardEvent, MouseState newMouseEvent ) 
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
                        {
                            pane.onClick(pos);
                            return true;
                        }
                    }
                }
                
            }
            return false;
        }

        /// <summary>
        /// Soll alle Events enthalten die mit dem Spiel zu tun haben
        /// </summary>
        public void GameEvents(KeyboardState newkeyboardEvent, MouseState newMouseEvent)
        {
            // Left Click
            System.Drawing.Point pos = new System.Drawing.Point(newMouseEvent.Position.X, newMouseEvent.Position.Y);
            if (oldMouseEvent.LeftButton == ButtonState.Pressed && newMouseEvent.LeftButton == ButtonState.Released)
            {
                cursor.onLeftClick(new System.Drawing.Point(pos.X, pos.Y));

                {
                    Pane[] copy = new Pane[Pane.currentPanes.Count];
                    Pane.currentPanes.CopyTo(copy);
                    foreach (Pane pane in copy)
                    {
                        if (pane.isHit(pos))
                        {
                            pane.onClick(pos);
                        }
                    }
                }
            }

            // MouseMove
            if (oldMouseEvent.Position != newMouseEvent.Position)
            {
                cursor.onMouseMove(new System.Drawing.Point(newMouseEvent.Position.X, newMouseEvent.Position.Y));
            }
        }

        public void ShowMapEditor(object sender, EventArgs eventArgs)
        {
            if (!editor.Visible)
                editor.Show();
                
            else
                editor.Hide();
        }

        // Objekte in den Restmüll
        public void WasteStuff()
        {
            editor.Close();
            editor = null;
        }

        public void onTurnBegin(object sender, EventArgs eventArgs)
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
            if(gameRounds % 2 == 0)
                roundDisplayString.text = (gameRounds / 2 ).ToString();
        }

        public void openMapChooser(object sender, EventArgs eventArgs)
        {
            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Map choosenMap = XmlLoader.loadMap(fd.FileName);
                if (choosenMap != null)
                {
                    currentMap = choosenMap;
                }
               
            }
          
        }
    }
}
