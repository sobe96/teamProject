﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors
{
    public abstract class IActor
    {
        protected Texture2D spriteImage;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;

        protected Vector2 position;
        public bool inGame = false;
        protected int hz = 60;
        protected bool isSpriteLoaded = false;
        protected Vector2 defaultPosition;

        public virtual void Initialize(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;
        }

        public virtual bool LoadSprite(String img)
        {
            spriteImage = content.Load<Texture2D>(img);
            isSpriteLoaded = true;
            return true;
        }

        public int GetRadius()
        {
            if (spriteImage != null)
            {
                int width = spriteImage.Width;
                int height = spriteImage.Height;

                if (width > height)
                    return Convert.ToInt32(Math.Ceiling((decimal)(width / 2)));
                else
                    return Convert.ToInt32(Math.Ceiling((decimal)(width / 2)));
            }

            throw new ArgumentNullException("Must set the sprite image first.");
        }

        public Texture2D GetSprite()
        {
            if (spriteImage != null)
                return spriteImage;

            throw new ArgumentException("No sprite defined");
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);


    }
}
