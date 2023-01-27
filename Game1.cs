using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace monogametopic8keep
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState;
        MouseState mouseState;
        // Textures
        Texture2D manLeftTexture;
        Texture2D manRightTexture;
        Texture2D manUpTexture;
        Texture2D manDownTexture;
        Texture2D currentManTexture; // Current Pacman texture to draw
        Rectangle manRect; // This rectangle will track where Pacman is and his size
        Texture2D exitTexture;
        Rectangle exitRect;
        Texture2D barrierTexture;
        List<Rectangle> barriers;
        Texture2D eggTexture;
        List<Rectangle> eggs;
        int manSpeed;
        Screen screen;
        SpriteFont Text;

        enum Screen
        {
            start,
            game,
            lose,
            win
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1425;
            _graphics.PreferredBackBufferHeight = 750;
            _graphics.ApplyChanges();

            //screen = Screen.start;

            base.Initialize();

            manSpeed = 5;
            manRect = new Rectangle(1275, 600, 75, 75);

            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(0, 0, 375, 75));
            barriers.Add(new Rectangle(375, 0, 375, 75));
            barriers.Add(new Rectangle(750, 0, 375, 75));
            barriers.Add(new Rectangle(1125, 0, 375, 75));
            barriers.Add(new Rectangle(0, 675, 375, 75));
            barriers.Add(new Rectangle(375, 675, 375, 75));            
            barriers.Add(new Rectangle(750, 675, 375, 75));
            barriers.Add(new Rectangle(1125, 675, 150, 75));
            barriers.Add(new Rectangle(0, 150, 375, 75));
            barriers.Add(new Rectangle(0, 0, 75, 375));
            barriers.Add(new Rectangle(0, 375, 75, 375));
            barriers.Add(new Rectangle(1350, 0, 75, 375));
            barriers.Add(new Rectangle(1350, 375, 75, 375));
            barriers.Add(new Rectangle(1200, 525, 75, 350));
            barriers.Add(new Rectangle(900, 525, 300, 75));
            barriers.Add(new Rectangle(150, 225, 75, 150));
            barriers.Add(new Rectangle(225, 300, 225, 75));
            barriers.Add(new Rectangle(150, 450, 75, 150));
            barriers.Add(new Rectangle(225, 525, 75, 150));
            barriers.Add(new Rectangle(300, 525, 225, 75));
            barriers.Add(new Rectangle(300, 375, 75, 75));
            barriers.Add(new Rectangle(600, 450, 75, 150));
            barriers.Add(new Rectangle(750, 375, 75, 225));
            barriers.Add(new Rectangle(675, 525, 75, 150));
            barriers.Add(new Rectangle(1125, 375, 75, 150));
            barriers.Add(new Rectangle(1275, 375, 75, 75));
            barriers.Add(new Rectangle(975, 375, 150, 75));
            barriers.Add(new Rectangle(825, 375, 75, 75));
            barriers.Add(new Rectangle(525, 300, 300, 75));
            barriers.Add(new Rectangle(450, 450, 75, 75));
            barriers.Add(new Rectangle(1200, 75, 75, 225));
            barriers.Add(new Rectangle(450, 75, 75, 150));
            barriers.Add(new Rectangle(1050, 150, 150, 75));
            barriers.Add(new Rectangle(900, 225, 225, 75));
            barriers.Add(new Rectangle(900, 75, 75, 75));
            barriers.Add(new Rectangle(750, 75, 75, 150));
            barriers.Add(new Rectangle(525, 150, 150, 75));


            eggs = new List<Rectangle>();
            eggs.Add(new Rectangle(75, 75, 75, 75));
            eggs.Add(new Rectangle(525, 75, 75, 75));
            eggs.Add(new Rectangle(150, 600, 75, 75));
            eggs.Add(new Rectangle(1125, 600, 75, 75));
            eggs.Add(new Rectangle(675, 450, 75, 75));
            eggs.Add(new Rectangle(1125, 75, 75, 75));

            exitRect = new Rectangle(1275, 675, 75, 75);

            Text = Content.Load<SpriteFont>("Text");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Pacman
            manDownTexture = Content.Load<Texture2D>("manDown");
            manUpTexture = Content.Load<Texture2D>("manUp");
            manRightTexture = Content.Load<Texture2D>("manRight");
            manLeftTexture = Content.Load<Texture2D>("manLeft");
            currentManTexture = manRightTexture;
            // Barrier
            barrierTexture = Content.Load<Texture2D>("stone_barrier");
            // Exit
            exitTexture = Content.Load<Texture2D>("hobbit_door");
            // Coin
            eggTexture = Content.Load<Texture2D>("duct tape");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.start)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.game;
                }

            }

            else if (screen == Screen.game)
            {
                // TODO: Add your update logic here
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    manRect.X -= manSpeed;
                    currentManTexture = manLeftTexture;
                    foreach (Rectangle barrier in barriers)
                        if (manRect.Intersects(barrier))
                        {
                            manRect.X = barrier.Right;
                        }
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    manRect.X += manSpeed;
                    currentManTexture = manRightTexture;
                    foreach (Rectangle barrier in barriers)
                        if (manRect.Intersects(barrier))
                        {
                            manRect.X = barrier.Left - manRect.Width;
                        }
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    manRect.Y -= manSpeed;
                    currentManTexture = manUpTexture;
                    foreach (Rectangle barrier in barriers)
                        if (manRect.Intersects(barrier))
                        {
                            manRect.Y = barrier.Bottom;
                        }
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    manRect.Y += manSpeed;
                    currentManTexture = manDownTexture;
                    foreach (Rectangle barrier in barriers)
                        if (manRect.Intersects(barrier))
                        {
                            manRect.Y = barrier.Top - manRect.Height;
                        }
                }

                for (int i = 0; i < eggs.Count; i++)
                {
                    if (manRect.Intersects(eggs[i]))
                    {
                        eggs.RemoveAt(i);
                        i--;
                    }
                }

                if (exitRect.Contains(manRect) && eggs.Count == 0)
                    Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.start)
            {
                _spriteBatch.DrawString(Text, "Left click to start the game", new Vector2(340,250), Color.Black);
            }

            else if (screen == Screen.game)
            {
                foreach (Rectangle barrier in barriers)
                    _spriteBatch.Draw(barrierTexture, barrier, Color.White);

                _spriteBatch.Draw(exitTexture, exitRect, Color.White);
                _spriteBatch.Draw(currentManTexture, manRect, Color.White);
                foreach (Rectangle coin in eggs)
                    _spriteBatch.Draw(eggTexture, coin, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}