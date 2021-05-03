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
    public class Soldier : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/soldier";
        private float speed;
        private float initCounter = 10f;
        private float counter = 0.5f;
        //private bool returning = false;
        //private Vector2 returnPosition;
        private Vector2 positionRightBot;
        private Vector2 positionLeftBot;
        private Vector2 positionRightTop;
        private Vector2 positionLeftTop;
        private Vector2 move;
        private int hitCounter = 0;

        public Soldier(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 16;
            inGame = true;
            isActive = true;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);

            // Randomizing starting point
            //int width = Config.rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            //int height = Config.rand.Next(GetRadius() * -1, 0);
            float ratio = graphics.PreferredBackBufferWidth / (graphics.PreferredBackBufferHeight / 2);
            float width = 0 - 2 * GetRadius()  - (2 * ratio * i * GetRadius());
            float height = 0 - 2 * GetRadius() - (2 * i * GetRadius());

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;
        }

        public Vector2 GetSoldierPosition()
        {
            return position;
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
                    speed = Utilities.ConvertToPercentage(Speed.HalfSpeed) * GameController.hz;

                // Checking to see if we are out of scope, if so, we remove from memory
                //if(currentPosition.Y < (GetRadius() * -1))
                //    isActive = false;

                /*Vector2 move = playerPosition - currentPosition;
                // Checking to see if we are returning due to hitting the mid point of the screen
                if (returning)
                    if (currentPosition.Y <= 0)
                    {
                        returning = false;
                    }
                    else
                    {
                        move = returnPosition - currentPosition;
                    }

                else if (currentPosition.Y >= (graphics.PreferredBackBufferHeight / 2))
                {
                    //returnPosition = Utilities.GetReturnPosition(graphics, defaultPosition, radius);
                    returnPosition = new Vector2(currentPosition.X, 0);
                    move = returnPosition - currentPosition;
                    returning = true;
                }*/



                /*positionRightBot = new Vector2(graphics.PreferredBackBufferWidth - 3 * GetRadius(), (graphics.PreferredBackBufferHeight / 2) - 3 * GetRadius());
                positionLeftBot = new Vector2(0 + 3 * GetRadius(), (graphics.PreferredBackBufferHeight / 2) - 3 * GetRadius());
                positionRightTop = new Vector2(graphics.PreferredBackBufferWidth - 3 * GetRadius(), 0 + 3 * GetRadius());
                positionLeftTop = new Vector2(0 + 3 * GetRadius(), 0 + 3 * GetRadius());*/

                /*if (Math.Round(currentPosition.X) <= Math.Round(positionLeftTop.X) && Math.Round(currentPosition.Y) <= Math.Round(positionLeftTop.Y))
                {
                    move = positionRightBot - currentPosition;
                }
                if (Math.Round(currentPosition.X) >= Math.Round(positionRightBot.X) && Math.Round(currentPosition.Y) >= Math.Round(positionRightBot.Y))
                {
                    move = positionRightTop - currentPosition;
                }
                if (Math.Round(currentPosition.X) <= Math.Round(positionRightTop.X) && Math.Round(currentPosition.Y) <= Math.Round(positionRightTop.Y))
                {
                    move = positionLeftBot - currentPosition;
                }
                if (Math.Round(currentPosition.X) >= Math.Round(positionLeftBot.X) && Math.Round(currentPosition.Y) >= Math.Round(positionLeftBot.Y))
                {
                    move = positionLeftTop - currentPosition;
                }*/

                /*if (returning)
                    if (currentPosition.Y <= 0)
                    {
                        returning = false;
                    }
                    else
                    {
                        move = returnPosition - currentPosition;
                    }

                else if (currentPosition.Y >= (graphics.PreferredBackBufferHeight / 2))
                {
                    //returnPosition = Utilities.GetReturnPosition(graphics, defaultPosition, radius);
                    returnPosition = new Vector2(currentPosition.X, 0);
                    move = returnPosition - currentPosition;
                    returning = true;
                }*/

                // Checking to see if we are returning due to hitting the mid point of the screen
                /*if (returning)
                    if (currentPosition.Y <= 0){
                        returning = false;
                    }
                    else
                    {
                        move = returnPosition - currentPosition;
                    }
                    
                else if (currentPosition.Y >= (graphics.PreferredBackBufferHeight / 2))
                {
                    //returnPosition = Utilities.GetReturnPosition(graphics, defaultPosition, radius);
                    returnPosition = new Vector2(currentPosition.X, 0);
                    move = returnPosition - currentPosition;
                    returning = true;
                }*/

                positionRightBot = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight / 2);
                positionLeftBot = new Vector2(0 - 2 * GetRadius(), graphics.PreferredBackBufferHeight / 2);
                positionRightTop = new Vector2(graphics.PreferredBackBufferWidth, 0);
                positionLeftTop = new Vector2(0 - 2 * GetRadius(), 0);

                if (Math.Round(currentPosition.X) <= positionLeftTop.X && currentPosition.Y <= positionLeftTop.Y)
                {
                    move = positionRightBot - currentPosition;
                }
                if (currentPosition.X >= positionRightBot.X && currentPosition.Y >= positionRightBot.Y)
                {
                    move = positionRightTop - currentPosition;
                }
                if (Math.Round(currentPosition.X) >= positionRightTop.X && currentPosition.Y <= positionRightTop.Y)
                {
                    move = positionLeftBot - currentPosition;
                }
                if (currentPosition.X <= positionLeftBot.X && currentPosition.Y >= positionLeftBot.Y)
                {
                    move = positionLeftTop - currentPosition;
                }

                //move = Movement.CrossMovement(graphics, currentPosition, positionRightBot, positionLeftBot, positionRightTop, positionLeftTop);

                move.Normalize();
                currentPosition += move * 8 * speed * dt;

                //currentPosition = Movement.CrossMovement(graphics, currentPosition, positionRightBot, positionLeftBot, positionRightTop, positionLeftTop);

                counter -= dt;
                if (counter <= 0)
                {
                    LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                    ILaser laserSol = factory.GetLazer(LaserType.Soldier, new Vector2(currentPosition.X + radius - 3, currentPosition.Y + 15), gameTime);

                    GameController.AddLaser(laserSol);
                    counter = initCounter / 10;
                }

                //Tell a soldier that he got hit

                // Checking for any hit from the player lasers
                foreach (PlayerLaser laser in GameController.playerLasers)
                {
                    int sum = radius + PlayerLaser.radius;

                    if (Vector2.Distance(laser.position, currentPosition) < sum)
                    {
                        hitCounter += 1;
                        laser.isHit = true;

                        if (hitCounter == Config.SOL_HP)
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
