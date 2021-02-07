using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Enemy
{
    public class Boss : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/boss";
        private int speed;

        public Boss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = 4 * hz;

            LoadSprite(SPRITE_IMAGE);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(GetSprite(),
                new Vector2(graphics.PreferredBackBufferWidth / 2 - (int)(GetSprite().Width / 2), 100),
                Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
