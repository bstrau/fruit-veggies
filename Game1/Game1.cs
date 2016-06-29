using Game1.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public static SpriteFont font;
        public static SpriteFont font_small;
        public static SpriteBatch spriteBatch;

        GraphicsDeviceManager graphics;
        GameManager gameManager;

        KeyboardState oldKeyState;
        MouseState oldMouseState;

        int SPIELFELDHOEHE = 9;
        int SPIELFELDBREITE = 16;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "";
            graphics.PreferredBackBufferHeight = 64 * (SPIELFELDHOEHE + 1);
            graphics.PreferredBackBufferWidth = 64 * SPIELFELDBREITE;
            graphics.ApplyChanges();
            
            // Titel
            this.Window.Title = "Fruit vs. Veggies";
            // Nur mit Vorsicht zu genießen
            this.Window.AllowUserResizing = true;
            // Mouse Zeigen
            this.IsMouseVisible = true;
            
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
            GraphicsObject.LoadGraphics("Content\\graphics\\marks", Content);

            // Soundobjekte initialisieren
            SoundObject.soundObjects = new Dictionary<string, SoundObject>();
            Content.RootDirectory = "Content";

            // Font laden
            font = Content.Load<SpriteFont>("fonts/font");
            font_small = Content.Load<SpriteFont>("fonts/font_size_10");

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

            gameManager = new GameManager(spriteBatch, font);
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
            KeyboardState newKeyState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            if (newKeyState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            gameManager.Update();

            oldMouseState = newMouseState;
            oldKeyState = newKeyState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            gameManager.Render();
            base.Draw(gameTime);
        }
    }
}
