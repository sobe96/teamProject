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

        public Underboss(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            LoadSprite(SPRITE_IMAGE);
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
