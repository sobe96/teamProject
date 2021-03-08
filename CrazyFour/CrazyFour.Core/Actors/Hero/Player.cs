using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Hero
{
    public class Player : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/hero";
        private const int SOL_HP = 3;
        private int counterHits = 0;
        private float speed;
        private float initCounter = 5f;
        private float counter = 0.5f;
        
        // I'd say that firing should be automatic
        private bool isFiring = true;
        private bool autoFire = true;
        private bool toggler = false;


        public Player(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            radius = 15;

            // defining the default speed
            speed = 4 * GameController.hz;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
        }

        public Vector2 GetPlayerPosition()
        { 
            return position; 
        }

        public Vector2 GetPlayerTruePosition()
        {
            Vector2 pos = GetPlayerPosition();
            Vector2 nPos = new Vector2();

            nPos.X = pos.X + GetSprite().Width / 2;
            nPos.Y = pos.Y + GetSprite().Height / 2;

            return nPos;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!inGame)
            {
                defaultPosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - (int)(GetSprite().Width / 2), graphics.PreferredBackBufferHeight - GetSprite().Height);
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
                inGame = true;
                position = defaultPosition;
            }
            else
                spriteBatch.Draw(GetSprite(), new Vector2(position.X, position.Y), Color.White);
        }

        public override void Update(GameTime gameTime, Vector2? pp)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // use controlling the speed of the game by pressing the S key
            speed = Utilities.ConvertToPercentage(Speed.Normal) * GameController.hz;

            // Moving the player
            if (kState.IsKeyDown(Keys.Right) && position.X < graphics.PreferredBackBufferWidth + 1 - GetSprite().Width)
                position.X += 3f * speed * dt;

            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
                position.X -= 3f * speed * dt;

            if (kState.IsKeyDown(Keys.Down) && position.Y < graphics.PreferredBackBufferHeight + 1 - GetSprite().Height)
                position.Y += 3f * speed * dt;

            if (kState.IsKeyDown(Keys.Up) && position.Y > (graphics.PreferredBackBufferHeight / 2))
                position.Y -= 3f * speed * dt;
            

            if (kState.IsKeyDown(Keys.K) && toggler == false)
            {
                if (autoFire == true)
                {
                    autoFire = false;
                    toggler = true;
                }
                else
                {
                    autoFire = true;
                    toggler = true;
                }
            }
            if (kState.IsKeyUp(Keys.K))
            {
                toggler = false;
            }
            // Firing projectile but making sure we fire only one at a time
            if (!autoFire)
            {
                if (!isFiring)
                {
                    if (kState.IsKeyDown(Keys.Space))
                    {
                        isFiring = true;

                        LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                        ILaser lazer = factory.GetLazer(LaserType.Player, new Vector2(position.X + radius, position.Y), gameTime);

                        GameController.AddLaser(lazer);
                    }
                }

                //releasing the flag once we fire one
                if (kState.IsKeyUp(Keys.Space))
                {
                    isFiring = false;
                }
            }
            else
            {
                counter -= dt;
                if (counter <= 0)
                {
                    LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                    ILaser lazer = factory.GetLazer(LaserType.Player, new Vector2(position.X + radius, position.Y), gameTime);

                    GameController.AddLaser(lazer);
                    counter = initCounter / 10;
                }
            }
        }

    }
}
