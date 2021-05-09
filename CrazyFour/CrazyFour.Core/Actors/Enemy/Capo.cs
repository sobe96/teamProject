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
        private float speed;
        private float initCounter = 10f;
        private float counter = .5f;
        public ConfigReader confReader = new ConfigReader();
        Config config;
        private int hitCounter = 0;
        Vector2 move;
        Vector2 leftCircleStart;
        Vector2 rightCircleStart;
        bool startDrawRight = false;
        bool startDrawLeft = false;
        bool movementStarted;
        float angle = 0;
        float width;
        float height;


        public Capo(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            Config config = confReader.ReadJson();
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 17;
            isActive = true;
            inGame = true;
            hitCounter = config.CAPO_HP;

            LoadSprite(LoadType.Ship, config.CAPO_SPRITE);

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
            laserFireOffset = new Vector2(0, 15);
            SetLaserMode((LaserMode)config.CAPO_LASERMODE);
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
            Config config = confReader.ReadJson();
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
                    //Movements.CircleMovement.circleMovementLeft(graphics, currentPosition, originLeft, move, angle, dt);
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
                    //Movements.CircleMovement.circleMovementRight(graphics, currentPosition, originRight, move, angle, dt);
                }

                //move = Movements.CircleMovement.circleMovement(graphics, currentPosition, leftCircleStart, rightCircleStart, originLeft, originRight, move, startDrawLeft, startDrawRight, angle, dt);

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
            ILaser lazer = factory.GetEnemyLaser(config.CAPO_LASER_SPRITE, pos, dir, gameTime);
            LaserController.AddLaser(lazer);
        }
    }
}
