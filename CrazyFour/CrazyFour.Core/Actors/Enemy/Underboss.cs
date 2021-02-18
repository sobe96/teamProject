using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Enemy
{
    public class Underboss : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/underboss";
        private const string LAZER_IMAGE = "Images/Lazers/BlueLazer";
        private float speed;
        private Random rand = new Random();
        public static int radius { get; } = 10;

        public Underboss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = 3f * (float)GameController.hz;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
            LoadSprite(LoadType.Laser, LAZER_IMAGE);
            inGame = true;

            // Randomizing starting point
            int width = rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            int height = rand.Next(GetRadius() * 1, (GetRadius() * 3) * 1);

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
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerPosition = (Vector2)pp;

            if (inGame)
            {
                Vector2 move = playerPosition - currentPosition;
                move.Normalize();
                currentPosition += move * speed * dt;
            }
        }
    }
}
