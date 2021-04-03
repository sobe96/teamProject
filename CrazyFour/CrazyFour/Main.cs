using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrazyFour.Core;
using CrazyFour.Core.Actors.Hero;
using CrazyFour.Core.Actors.Enemy;
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
        private MouseState mState;
        private IActor player;
        //private IActor soldier;
        private Texture2D spaceBackground;
        private SpriteFont defaultFont;

        public double timer;



        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = Config.windowWidth;
            _graphics.PreferredBackBufferHeight = Config.windowHeight;

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
            controller.LoadContent(factory);
            int i = 0;
            player = factory.GetActor(ActorTypes.Player, i);

            spaceBackground = Content.Load<Texture2D>("Images/space");
            defaultFont = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _spriteBatch.Begin();

                // Loading the defaults
                _spriteBatch.Draw(spaceBackground, new Vector2(0, 0), Color.White);

                if (((Player)player).isDead)
                {
                    Config.status = GameStatus.Died;

                    GameController.enemyLasers.Clear();
                    GameController.enemyList.Clear();

                    String msg = "You Lost!";
                    Vector2 sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2), Color.White);

                    _spriteBatch.End();
                    base.Draw(gameTime);
                    return;
                }


                // If the game hasn't started only
                if (Config.status == GameStatus.Starting)
                {
                    String msg = "Press Enter to Start Game.";
                    Vector2 sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2), Color.White);

                    msg = "Use the 'S' key to slow the game down";
                    sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2 + 50), Color.White);

                    msg = "Use the 'K' key to toggle auto-fire ON/OFF";
                    sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2 + 100), Color.White);

                    msg = "Use the Spacebar key to fire";
                    sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2 + 150), Color.White);

                    _spriteBatch.End();
                    base.Draw(gameTime);
                    return;
                }

                _spriteBatch.DrawString(defaultFont, "Timer: " + Utilities.TicksToTime(Math.Ceiling(timer)), new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(defaultFont, "Lives: " + ((Player)player).Lives, new Vector2(0, 25), Color.White);

                if (Config.status == GameStatus.Gameover)
                {
                    String msg = "You Won!";
                    Vector2 sizeOfText = defaultFont.MeasureString(msg);
                    _spriteBatch.DrawString(defaultFont, msg, new Vector2(Config.windowWidth / 2 - sizeOfText.X / 2, Config.windowHeight / 2), Color.White);

                    _spriteBatch.End();
                    base.Draw(gameTime);
                    return;
                }

                if (player.isHit)
                {
                    TimeSpan ndt = DateTime.Now.Subtract(((Player)player).hitTime);

                    if (ndt.Seconds >= 1)
                    {
                        ((Player)player).SetDefaultPosition();
                        player.Draw(gameTime);
                        player.isHit = false;
                    }
                    else
                    {
                        controller.Draw(gameTime);
                        _spriteBatch.End();
                        base.Draw(gameTime);
                        return;
                    }
                }

                player.Draw(gameTime);
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

        protected override void Update(GameTime gameTime)
        {
            try
            {
                GameController.totalTime = (float)timer;

                // Making sure we start the game when the enter button is pressed
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    Config.status = GameStatus.Playing;
                }

                if (Config.status == GameStatus.Playing)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                    
                    timer += gameTime.ElapsedGameTime.TotalSeconds;
                    mState = Mouse.GetState();

                    if (!player.isDead && !player.isHit)
                    {
                        player.Update(gameTime, null);
                        controller.Update(gameTime, (Player)player);
                    }

                    base.Update(gameTime);

                }
            }
            catch (Exception ex) {
                var colorTask = MessageBox.Show("Error Occurred", ex.Message, new[] { "OK" });
            }
        }
        
    }
}
