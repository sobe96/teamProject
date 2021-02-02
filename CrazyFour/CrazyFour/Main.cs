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

        MouseState mState;

        IActor player;
        IActor boss;
        IActor underboss;
        IActor capo;
        IActor soldier;


        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
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

        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                if (player.inGame)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    // TODO: Add your update logic here
                    mState = Mouse.GetState();

                    player.Update(gameTime);

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

                player.Draw(gameTime);
                boss.Draw(gameTime);
                underboss.Draw(gameTime);
                capo.Draw(gameTime);
                soldier.Draw(gameTime);

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
