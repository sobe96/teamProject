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
    public class Capo : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/capo";
        private float speed;
        private float initCounter = 10f;
        private float counter = .5f;
        private bool returning = false;
        private Vector2 returnPosition;
        private int hitCounter = 0;
        Vector2 move;
        Vector2 leftCircleStart;
        Vector2 rightCircleStart;
        Vector2 originLeft;
        Vector2 originRight;
        bool startDrawLeft = false;
        bool startDrawRight = false;
        float angle = 0;
        float width;
        float height;


        public Capo(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 17;
            isActive = true;
            inGame = true;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);

            // Randomizing starting point
            //int width = Config.rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            //int height = Config.rand.Next(GetRadius() * -1, 0);
            if (i > 1)
            {
                width = graphics.PreferredBackBufferWidth * (i % 2);
                height = graphics.PreferredBackBufferHeight / 2;
            }
            else
            {
                width = graphics.PreferredBackBufferWidth * (i % 2);
                height = 0;
            }

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;
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
            playerPosition.X += 25;     // player's radius

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
                leftCircleStart = new Vector2(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 3);
                rightCircleStart = new Vector2(3 * graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 3);
                float rad = 500;
                Vector2 originLeft = new Vector2(graphics.PreferredBackBufferWidth / 4, (graphics.PreferredBackBufferHeight / 3));
                Vector2 originRight = new Vector2(3 * graphics.PreferredBackBufferWidth / 4, (graphics.PreferredBackBufferHeight / 3));

                if (currentPosition.X <= 0 && currentPosition.Y <= 0 || currentPosition.X <= 0 && currentPosition.Y >=  graphics.PreferredBackBufferHeight / 2)
                {
                    move = leftCircleStart - currentPosition;
                }
                if (currentPosition.X >= graphics.PreferredBackBufferWidth && currentPosition.Y <= 0 || currentPosition.X >= graphics.PreferredBackBufferWidth && currentPosition.Y >= graphics.PreferredBackBufferHeight / 2)
                {
                    move = rightCircleStart - currentPosition;
                }
                if (Math.Round(currentPosition.X) == leftCircleStart.X && Math.Round(currentPosition.Y) == leftCircleStart.Y)
                {
                    startDrawLeft = true;
                }
                if (startDrawLeft)
                {
                    move.X = (float)(originLeft.X - Math.Sin(angle) * rad) - currentPosition.X;
                    move.Y = (float)(originLeft.Y + Math.Cos(angle) * rad) - currentPosition.Y;
                    angle = angle + 2 * dt / 3;
                    if (angle >= 360)
                    {
                        angle = 0;
                    }
                }

                if (Math.Round(currentPosition.X) == rightCircleStart.X && Math.Round(currentPosition.Y) == rightCircleStart.Y)
                {
                    startDrawRight = true;
                }
                if (startDrawRight)
                {
                    move.X = (float)(originRight.X + Math.Sin(angle) * rad) - currentPosition.X;
                    move.Y = (float)(originRight.Y + Math.Cos(angle) * rad) - currentPosition.Y;
                    angle = angle + 2 * dt / 3;
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
                    LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                    ILaser laserSol = factory.GetLazer(LaserType.Capo, new Vector2(currentPosition.X + radius - 3, currentPosition.Y + 15), gameTime);

                    GameController.AddLaser(laserSol);
                    counter = initCounter / 10;
                }

                // Checking for any hit from the player lasers
                foreach (PlayerLaser laser in GameController.playerLasers)
                {
                    int sum = radius + PlayerLaser.radius;

                    if (Vector2.Distance(laser.position, currentPosition) < sum)
                    {
                        hitCounter += 1;
                        laser.isHit = true;

                        if (hitCounter == Config.CAPO_HP)
                        {
                            isHit = true;
                            hitCounter = 0;
                        }
                    }

                    laser.Update(gameTime);
                }
            }
        }
    }
}
