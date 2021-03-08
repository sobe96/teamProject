using CrazyFour.Core.Helpers;
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
        protected Texture2D laserImage;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;

        protected Vector2 position;
        public bool inGame = false;
        protected bool isSpriteLoaded = false;
        protected Vector2 defaultPosition;
        
        public Vector2 currentPosition;
        protected Vector2 soldierPosition;
        protected Vector2 playerPosition;

        public bool isHit = false;
        public bool isActive = true;
        public bool isDead = false;

        public virtual int radius { get; set; } = 0;

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
                case LoadType.Laser:
                    laserImage = content.Load<Texture2D>(img);
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
                int rad = 0;

                if (width > height)
                    rad = Convert.ToInt32(Math.Ceiling((decimal)(width / 2)));
                else
                    rad = Convert.ToInt32(Math.Ceiling((decimal)(height / 2)));

                return rad;
            }

            throw new ArgumentNullException("Must set the sprite image first.");
        }

        public Texture2D GetSprite()
        {
            if (spriteImage != null)
                return spriteImage;

            throw new ArgumentException("No sprite defined");
        }

        public abstract void Update(GameTime gameTime, Vector2? playerPosition);

        public abstract void Draw(GameTime gameTime);


    }
}
