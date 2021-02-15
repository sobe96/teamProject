﻿using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
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
        protected Texture2D lazerImage;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;

        protected Vector2 position;
        public bool inGame = false;
        protected bool isSpriteLoaded = false;
        protected Vector2 defaultPosition;
        public int radius;

        public virtual void Initialize(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;
        }

        public virtual bool LoadSprite(LoadType type, String img)
        {
            switch(type)
            {
                case LoadType.Ship:
                    spriteImage = content.Load<Texture2D>(img);
                    isSpriteLoaded = true;
                    break;
                case LoadType.Lazer:
                    lazerImage = content.Load<Texture2D>(img);
                    break;
            }
            
            return true;
        }

        public int GetRadius()
        {
            if (spriteImage != null)
            {
                int width = spriteImage.Width;
                int height = spriteImage.Height;

                if (width > height)
                    return radius = Convert.ToInt32(Math.Ceiling((decimal)(width / 2)));
                else
                    return radius = Convert.ToInt32(Math.Ceiling((decimal)(width / 2)));
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
