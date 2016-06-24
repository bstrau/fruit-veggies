using Game1.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;

        int SPIELFELDHOEHE = 16;
        int SPIELFELDBREITE = 16;

        KeyboardState oldState;
        MouseState oldMouseEvent;

        Spieler player_one;
        Spieler player_two;
        MapEditor editor;
        Pane mainMenu;
        Cursor cursor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "";
            graphics.PreferredBackBufferHeight = 64 * SPIELFELDHOEHE;
            graphics.PreferredBackBufferWidth = 64 * SPIELFELDBREITE;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;
            
            editor = new MapEditor();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Liste der Grafikobjekte initialisieren
            GraphicsObject.graphicObjects = new Dictionary<string, GraphicsObject>();
            GraphicsObject.LoadGraphics("Content\\graphics\\tiles", Content);
            GraphicsObject.LoadGraphics("Content\\graphics\\units", Content);
            GraphicsObject.LoadGraphics("Content\\graphics\\menus", Content);

            // Soundobjekte initialisieren
            SoundObject.soundObjects = new Dictionary<string, SoundObject>();
            Content.RootDirectory = "Content";

           
            String[] files = Directory.GetFiles("Content\\sounds\\bg");
            foreach (String file in files)
            {
                String name = Path.GetFileNameWithoutExtension(file);
                SoundObject sound = new SoundObject(Content.Load<SoundEffect>("sounds/bg/" + name));

                // GraphicsObject in Dictionary übernehmen
                if (sound != null)
                {
                    SoundObject.soundObjects.Add(name, sound);
                }
            }

            // XMLs laden...
            XmlLoader.loadAllTiles("Content\\xml\\Tiles.XML");
            XmlLoader.loadAllUnits("Content\\xml\\Units.XML");
            XmlLoader.loadAllMaps("Content\\xml\\Maps.XML");
            
            // Player Festlegen

            player_one = new Spieler();
            player_two = new Spieler();

            // Map festlegen und initialisieren. Testweise die erste.
            map = Map.Maps["0"];
            map.Init();
            map.MuteSound(true);

            // MainMenu erzeugen
            mainMenu = new Pane("menu", "menu");
            mainMenu.setPosition(new System.Drawing.Point(100, 100));
            mainMenu.setDimensions(new Size(900, 540));
            mainMenu.Register();

            // LevelEditor Menu
            Pane editorMenu = new Pane("menuoption", "editor");
            editorMenu.setPosition(new System.Drawing.Point(10, 10));
            editorMenu.setDimensions(new Size(880, 80));
            mainMenu.AddPane(editorMenu);


            // Cursor

            cursor = new Cursor(map, map.GetMapTiles()[25]);
            cursor.setPlayer(player_one);
            cursor.getCurrentTile().enter(Unit.Units["1"]);

            List<Tile> availTiles = new List<Tile>();
            cursor.findWay(availTiles, cursor.getCurrentTile(), 5);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Mapeditor schliessen, sollte er noch offen sein
            editor.Close();

            // Alle Texturen freigeben
            foreach(GraphicsObject graphic in GraphicsObject.graphicObjects.Values)
            {
                graphic.Waste();
            }

            // Alle Sounds freigeben
            foreach (SoundObject sound in SoundObject.soundObjects.Values)
            {
                sound.Waste();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            MouseState newMouseEvent = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newState.IsKeyDown(Keys.Escape))
                Exit();

            if (oldState.IsKeyDown(Keys.Tab) && newState.IsKeyUp(Keys.Tab))
            {
                if (mainMenu.isVisible())
                    mainMenu.Hide();
                else
                    mainMenu.Show();
            }

            //&& newState.IsKeyUp(Keys.LeftControl & Keys.M)
            if ((oldState.IsKeyDown(Keys.LeftControl) && oldState.IsKeyDown(Keys.M)) && (newState.IsKeyUp(Keys.LeftControl) || newState.IsKeyUp(Keys.M)))
            {
                if (editor.Visible)
                    editor.Hide();
                else
                    editor.Show();
            }

            if (oldMouseEvent.LeftButton == ButtonState.Pressed && oldMouseEvent.LeftButton == ButtonState.Released)
            {
                map.onClick(newMouseEvent);
            }

            oldState = newState;
            oldMouseEvent = newMouseEvent;

            // TEST: Loote alle TREASURE Tiles, um zu zeigen, dass Instanziierung funktioniert
            foreach(Tile tile in map.GetMapTiles())
            {
                if(tile.GetTileType() == Tile.TileType.TREASURE)
                {
                    int loot = ((TreasureTile)tile).GetLoot();
                    if(loot != 0)
                    {
                        Console.WriteLine("Could loot " + loot + " resources");
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            spriteBatch.Begin();
            map.Draw(spriteBatch);
            Pane.DrawPanes(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
