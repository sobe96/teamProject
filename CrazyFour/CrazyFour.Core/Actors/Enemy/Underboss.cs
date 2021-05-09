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

namespace CrazyFour.Core.Actors.Enemy
{
    public class Underboss : IActor
    {
        private float speed;
        private float initCounter = 10f;
        private float counter = 0.5f;
        Config config;
        public ConfigReader confReader = new ConfigReader();
        private int hitCounter = 0;
        private Vector2 move;
        private float angle = 0;
        private bool rotateUp = false;
        private bool rotateDown = false;

        public Underboss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            Config config = confReader.ReadJson();
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 40;
            inGame = true;
            isActive = true;
            hitCounter = config.UBOSS_HP;

            LoadSprite(LoadType.Ship, config.UBOSS_SPRITE);

            // Randomizing starting point
            //int width = Config.rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            //int height = Config.rand.Next(GetRadius() * -1, 0);

            float width = (float)(i * graphics.PreferredBackBufferWidth - Math.Pow(-1,i) * GetRadius());
            float height = (float)(graphics.PreferredBackBufferHeight / 3 + i * graphics.PreferredBackBufferHeight / 3) - 100;

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;
            laserFireOffset = new Vector2(0, 55);
            SetLaserMode((LaserMode)config.UBOSS_LASERMODE);
        }

        public override void Draw(GameTime gameTime)
        {
            if (inGame)
            {
                spriteBatch.Draw(GetSprite(), currentPosition, Color.White);
            }
            else
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2? pp)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerPosition = (Vector2)pp;
            playerPosition.X += 2;     // player's radius

            if (inGame)
            {
                // use controlling the speed of the game by pressing the S key
                if (kState.IsKeyDown(Keys.S))
                    speed = Utilities.ConvertToPercentage(Speed.QuarterSpeed) * GameController.hz;
                else
                    speed = Utilities.ConvertToPercentage(Speed.ThreeQuarterSpeed) * GameController.hz;

                // Checking to see if we are out of scope, if so, we remove from memory
                //if (currentPosition.Y < (GetRadius() * -1))
                //    isActive = false;

                /*Vector2 move = playerPosition - currentPosition;

                // Checking to see if we are returning due to hitting the mid point of the screen
                if (returning)
                    move = returnPosition - currentPosition;
                else if (currentPosition.Y >= (graphics.PreferredBackBufferHeight / 2))
                {
                    returnPosition = Utilities.GetReturnPosition(graphics, defaultPosition, radius);
                    move = returnPosition - currentPosition;
                    returning = true;
                }*/

                Vector2 centerHigh = new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 3) - 100);
                Vector2 centerLow = new Vector2(graphics.PreferredBackBufferWidth / 2, (2 * graphics.PreferredBackBufferHeight / 3) - 100);
                Vector2 origin = new Vector2(graphics.PreferredBackBufferWidth / 2, (graphics.PreferredBackBufferHeight / 2) - 100);
                Vector2 rightUp = new Vector2(graphics.PreferredBackBufferWidth + GetRadius(), graphics.PreferredBackBufferHeight / 3);
                Vector2 leftUp = new Vector2(0 - GetRadius(), graphics.PreferredBackBufferHeight / 3);
                Vector2 rightDown = new Vector2(graphics.PreferredBackBufferWidth + GetRadius(), 2 * graphics.PreferredBackBufferHeight / 3);
                Vector2 leftDown = new Vector2(0 - GetRadius(), 2 * graphics.PreferredBackBufferHeight / 3);
                float rad = 300;

                if (currentPosition.X <= 0 && currentPosition.Y == (graphics.PreferredBackBufferHeight / 3) - 100)
                {
                    move = centerHigh - currentPosition;
                }
                if (currentPosition.X >= graphics.PreferredBackBufferWidth && currentPosition.Y == (2 * graphics.PreferredBackBufferHeight / 3) - 100)
                {
                    move = centerLow - currentPosition;
                }
                if (Math.Round(currentPosition.X) == centerHigh.X && Math.Round(currentPosition.Y) == centerHigh.Y)
                {
                    rotateUp = true;
                    
                }
                if (rotateUp)
                {
                    move.X = (float)(origin.X + Math.Cos(angle) * rad) - currentPosition.X;
                    move.Y = (float)(origin.Y + Math.Sin(angle) * rad) - currentPosition.Y;
                    angle = angle + dt;
                    if (angle >= 360)
                    {
                        angle = 0;
                    }
                }
                if (Math.Round(currentPosition.X) == centerLow.X && Math.Round(currentPosition.Y) == centerLow.Y)
                {
                    rotateDown = true;
                }

                if (rotateDown)
                {
                    move.X = (float)(origin.X - Math.Cos(angle) * rad) - currentPosition.X;
                    move.Y = (float)(origin.Y - Math.Sin(angle) * rad) - currentPosition.Y;
                    angle = angle + dt;
                    if (angle >= 360)
                    {
                        angle = 0;
                    }
                }



                move.Normalize();
                currentPosition += move * 3 * speed * dt;

                counter -= dt;
                if (counter <= 0)
                {
                    FireLaser(gameTime);
                    counter = initCounter / 10;
                }

            }
        }
        protected override void CreateLaser(Vector2 pos, Vector2 dir, GameTime gameTime)
        {
            Config config = confReader.ReadJson();
            LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
            ILaser lazer = factory.GetEnemyLaser(config.UBOSS_LASER_SPRITE, pos, dir, gameTime);
            LaserController.AddLaser(lazer);
        }
    }
}
