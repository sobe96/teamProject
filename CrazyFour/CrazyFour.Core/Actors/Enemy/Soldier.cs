using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using CrazyFour.Core.Actors.Movements;
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
        private float speed;
        private float initCounter = 10f;
        private float counter = 0.5f;
        private Vector2 positionRightBot;
        private Vector2 positionLeftBot;
        private Vector2 positionRightTop;
        private Vector2 positionLeftTop;
        private Vector2 move;
        private int hitCounter = 0;
        Config config;
        public ConfigReader confReader = new ConfigReader();

        public Soldier(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, int i)
        {
            Config config = confReader.ReadJson();
            graphics = g;
            spriteBatch = s;
            content = c;
            radius = 16;
            inGame = true;
            isActive = true;
            hitCounter = config.SOLDIER_HP;

            LoadSprite(LoadType.Ship, config.SOLDIER_SPRITE);

            // Randomizing starting point
            //int width = Config.rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            //int height = Config.rand.Next(GetRadius() * -1, 0);
            float ratio = graphics.PreferredBackBufferWidth / (graphics.PreferredBackBufferHeight / 2);
            float width = 0 - 2 * GetRadius()  - (2 * ratio * i * GetRadius());
            float height = 0 - 2 * GetRadius() - (2 * i * GetRadius());

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;
            laserFireOffset = new Vector2(0, 15);
            SetLaserMode((LaserMode)config.SOLDIER_LASERMODE);
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

                positionRightBot = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight / 2);
                positionLeftBot = new Vector2(0 - 2 * GetRadius(), graphics.PreferredBackBufferHeight / 2);
                positionRightTop = new Vector2(graphics.PreferredBackBufferWidth, 0);
                positionLeftTop = new Vector2(0 - 2 * GetRadius(), 0);

                /*if (Math.Round(currentPosition.X) <= positionLeftTop.X && currentPosition.Y <= positionLeftTop.Y)
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
                }*/

                move = CrossMovement.crossMovement(currentPosition, positionRightBot, positionLeftBot, positionRightTop, positionLeftTop, move);

                move.Normalize();
                currentPosition += move * 8 * speed * dt;

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
            ILaser lazer = factory.GetEnemyLaser(config.SOLDIER_LASER_SPRITE, pos, dir, gameTime);
            LaserController.AddLaser(lazer);
        }
    }
}
