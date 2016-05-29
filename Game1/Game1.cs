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

        int SPIELFELDHOEHE = 4;
        int SPIELFELDBREITE = 4;  

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "";
            graphics.PreferredBackBufferHeight = 64 * SPIELFELDHOEHE;
            graphics.PreferredBackBufferWidth = 64 * SPIELFELDBREITE;
            graphics.ApplyChanges();
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Zeige alle im XML-Dokument definierten Tiles an 
            int posx = 0;
            int posy = 0;
            int sizex = GraphicsDevice.Viewport.Width;
            spriteBatch.Begin();
            foreach(Tile tile in Tile.Tiles.Values)
            {
                if(tile.GetTileType() == Tile.TileType.RESSOURCE)
                {
                    int loot = ((RessourcesTile)tile).GetLoot();
                    if(loot != 0)
                        Console.WriteLine(loot);
                }
                if (posx + 64 > sizex)
                {
                    posx = 0;
                    posy += 64;
                }

                tile.SetPos(posx, posy);
                tile.Draw(spriteBatch);

                posx += 64;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
