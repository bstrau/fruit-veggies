using Game1.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "";
            graphics.PreferredBackBufferHeight = 64 * SPIELFELDHOEHE;
            graphics.PreferredBackBufferWidth = 64 * SPIELFELDBREITE;
            graphics.ApplyChanges();

            MapEditor form = new MapEditor();
            form.Show();

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
            String[] files = Directory.GetFiles("Content\\graphics");
            foreach(String file in files)
            {
                GraphicsObject test = new GraphicsObject(Content.Load<Texture2D>(file));
                if (test != null)
                {
                    String name = Path.GetFileNameWithoutExtension(file);
                    GraphicsObject.graphicObjects.Add(name, test);
                }
            }

            // XMLs laden...
            XmlLoader.loadAllTiles("Content\\xml\\Tiles.XML");
            XmlLoader.loadAllMaps("Content\\xml\\Maps.XML");

            // Map festlegen. Testweise die erste
            map = Map.Maps["0"];
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Alle Texturen freigeben
            foreach(GraphicsObject graphic in GraphicsObject.graphicObjects.Values)
            {
                graphic.Waste();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
