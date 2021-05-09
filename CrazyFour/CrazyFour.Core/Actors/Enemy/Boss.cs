using CrazyFour.Core.Actors.Hero;
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
    public class Boss : IActor
    {
        private float speed;
        private float initCounter = 10f;
        private float counter = 0.5f;
        private bool initialized = false;
        private Vector2 move;
        Config config;
        public ConfigReader confReader = new ConfigReader();
        private int hitCounter = 0;

        public Boss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            Config config = confReader.ReadJson();
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 91;
            inGame = true;
            isActive = true;
            hitCounter = config.BOSS_HP;

            LoadSprite(LoadType.Ship, config.BOSS_SPRITE);

            // Randomizing starting point
            //int width = Config.rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            //int height = Config.rand.Next(GetRadius() * -1,  0);

            float width = graphics.PreferredBackBufferWidth / 2;
            float height = 0 - 2 * radius;

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;
            laserFireOffset = new Vector2(0, 160);
            SetLaserMode((LaserMode)config.BOSS_LASERMODE);
        }

        public override void Draw(GameTime gameTime)
        {
            if (inGame)
            {
                spriteBatch.Draw(GetSprite(), currentPosition, Color.White);
            }
            else
            {
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
            }
        }

        public override void Update(GameTime gameTime, Vector2? pp)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerPosition = (Vector2)pp;
            playerPosition.X += -55;     // player's radius

            if (inGame)
            {
                // use controlling the speed of the game by pressing the S key
                if (kState.IsKeyDown(Keys.S))
                    speed = Utilities.ConvertToPercentage(Speed.QuarterSpeed) * GameController.hz;
                else
                    speed = Utilities.ConvertToPercentage(Speed.ThreeQuarterSpeed) * GameController.hz;

                Vector2 center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 5);
                Vector2 left = new Vector2(graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight / 5);
                Vector2 right = new Vector2(4 * graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight / 5);

                /*if (currentPosition.X == graphics.PreferredBackBufferWidth / 2 && Math.Round(currentPosition.Y) <= 0 - 2 * GetRadius())
                {
                    move = center - currentPosition;
                }*/
                if (currentPosition.X == center.X && Math.Round(currentPosition.Y) == center.Y && !initialized)
                {
                    move = left - currentPosition;
                    initialized = true;
                }
                /*if (Math.Round(currentPosition.X) == left.X && Math.Round(currentPosition.Y) == left.Y)
                {
                    move = right - currentPosition;
                }
                if (Math.Round(currentPosition.X) == right.X && Math.Round(currentPosition.Y) == right.Y)
                {
                    move = left - currentPosition;
                }*/
                //int rad = GetRadius();
                move = Movements.BossMovement.bossMovement(graphics, currentPosition, center, left, right, move, radius, initialized);

                move.Normalize();
                currentPosition += move * speed * dt;
                position = currentPosition;

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
            ILaser lazer = factory.GetEnemyLaser(config.BOSS_LASER_SPRITE, pos, dir, gameTime);
            LaserController.AddLaser(lazer);
        }
    }
}