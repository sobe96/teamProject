using CrazyFour.Core.Helpers;
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
        private const string LAZER_IMAGE = "Images/Lazers/GreenLazer";
        private float speed;
        private int radius = 15;
        private Random rand = new Random();

        public Soldier(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = .5F * (float)GameController.hz;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
            LoadSprite(LoadType.Lazer, LAZER_IMAGE);
            inGame = true;

            //int width = rand.Next(GetRadius(), graphics.PreferredBackBufferWidth / 2);
            //int height = rand.Next(graphics.PreferredBackBufferHeight - 200);

            int w = GetRadius() * -1;
            int w2 = (GetRadius() * 3) * -1;

            int width = rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            int height = rand.Next((GetRadius() * 3) * -1, GetRadius() * -1);

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;

        }

        public override void Draw(GameTime gameTime)
        {
            if (inGame)
            {
                int width = rand.Next(-1, 2);
                int height = rand.Next(-1, 2);

                currentPosition.X += width;
                currentPosition.Y += height;

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
