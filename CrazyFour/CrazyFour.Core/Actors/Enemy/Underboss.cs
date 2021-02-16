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
        private int speed;
        private int radius = 40;



        public Underboss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = 4 * GameController.hz;

            defaultPosition.X = 125;
            defaultPosition.Y = 424;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
            LoadSprite(LoadType.Lazer, LAZER_IMAGE);
        }

        public override void Draw(GameTime gameTime)
        {
            if (inGame)
            {
                Random rand = new Random();
                int width = rand.Next(GetRadius(), graphics.PreferredBackBufferWidth / 2);
                int height = rand.Next(graphics.PreferredBackBufferHeight - 200);
                spriteBatch.Draw(GetSprite(), new Vector2(width, height), Color.White);
            }
            else
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2? pp)
        {
            throw new NotImplementedException();
        }
    }
}
