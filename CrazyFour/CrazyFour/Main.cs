using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrazyFour.Core;
using CrazyFour.Core.Actors.Hero;
using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Actors;
using System;

namespace CrazyFour
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ActorFactory factory;
        private GameController controller;

        private const int windowWidth = 1280;
        private const int windowHeight = 720;

        private MouseState mState;

        private IActor player;
        private IActor boss;
        private IActor underboss;
        private IActor capo;
        private IActor soldier;

        private Texture2D spaceBackground;
        private double timer;
        private SpriteFont defaultFont;
        private bool inGame = false;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;

            controller = GameController.Instance;

            timer = 0D;
        }

        protected override void Initialize()
        {
            // moved these two lines from top of LoadContent(), keep an eye out.  Might break later
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            factory = new ActorFactory(_graphics, _spriteBatch, Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            player = factory.GetActor(ActorTypes.Player);
            boss = factory.GetActor(ActorTypes.Boss);
            underboss = factory.GetActor(ActorTypes.Underboss);
            capo = factory.GetActor(ActorTypes.Capo);
            soldier = factory.GetActor(ActorTypes.Soldier);

            spaceBackground = Content.Load<Texture2D>("Images/space");
            defaultFont = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                // Making sure we start the game when the enter button is pressed
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                }

                if (inGame)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    
                    timer += gameTime.ElapsedGameTime.TotalSeconds;
                    mState = Mouse.GetState();

                    player.Update(gameTime);
                    controller.Update(gameTime);
                 

                        base.Update(gameTime);
                }
            }
            catch (Exception ex) {
                var colorTask = MessageBox.Show("Error Occurred", ex.Message, new[] { "OK" });
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            { 
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here

                _spriteBatch.Begin();

                // Loading the defaults
                _spriteBatch.Draw(spaceBackground, new Vector2(0, 0), Color.White);

                if(!inGame)
                {
                    String msg = "Press Enter to Start Game.";
                    Vector2 sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(windowWidth / 2 - sizeOfText.X / 2, windowHeight / 2), Color.White);

                    msg = "Use the 'S' key to slow the game down";
                    sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(windowWidth / 2 - sizeOfText.X / 2, windowHeight / 2 + 50), Color.White);

                    msg = "Use the Spacebar key to fire";
                    sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(windowWidth / 2 - sizeOfText.X / 2, windowHeight / 2 + 100), Color.White);

                    _spriteBatch.End();
                    base.Draw(gameTime);
                    return;
                }

                _spriteBatch.DrawString(defaultFont, "Timer: " + Utilities.TicksToTime(Math.Ceiling(timer)), new Vector2(0, 0), Color.White);

                player.Draw(gameTime);
                boss.Draw(gameTime);
                underboss.Draw(gameTime);
                capo.Draw(gameTime);
                soldier.Draw(gameTime);

                // updating the lasers
                controller.Draw(gameTime);

                _spriteBatch.End();

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {
                _spriteBatch.End();
                var colorTask = MessageBox.Show("Error Occurred", ex.Message, new[] { "OK" });
            }
        }
    }
}
