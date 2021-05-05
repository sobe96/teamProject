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
        protected bool isSpriteLoaded = false;
        protected Vector2 defaultPosition;
        
        public Vector2 currentPosition;
        protected Vector2 soldierPosition;
        protected Vector2 playerPosition;

        public bool inGame = false;
        public bool isHit = false;
        public bool isActive = true;
        public bool isDead = false;

        protected Vector2 laserDirection = new Vector2(0, 1);
        protected Vector2 laserFireOffset = new Vector2(0, 0);
        protected Vector2 laserDensity =  new Vector2(20f, 0);
        protected LaserMode laserMode = LaserMode.Single;

        public virtual int hitCounter { get; set; } = 0;

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

        protected void FireLaser(GameTime gameTime)
        {
            switch (laserMode)
            {
                case LaserMode.Single:
                    SeriesOfLaser(gameTime, 1);
                    break;
                case LaserMode.Double:
                    SeriesOfLaser(gameTime, 2);
                    break;
                case LaserMode.Triple:
                    SeriesOfLaser(gameTime, 3);
                    break;
                case LaserMode.Cricle:
                    CircleLaser(gameTime, 5);
                    break;
                case LaserMode.Cone:
                    ConeLaser(gameTime, 5);
                    break;
            }
        }
        protected abstract void CreateLaser(Vector2 pos, Vector2 dir, GameTime gameTime);
        protected virtual void SeriesOfLaser(GameTime gameTime, int count)
        {
            bool isEven = count % 2 == 0;
            for (int i = 0; i < count; ++i)
            {
                Vector2 dir = laserDirection;
                Vector2 pos = new Vector2(position.X - 6 + GetSprite().Width / 2, position.Y) + laserFireOffset;
                if (isEven)
                {
                    pos.X -= laserDensity.X/2;
                }

                int threes = (1 + i / 3);
                if (i % 3 == 1)
                {
                    pos.X += threes * laserDensity.X;
                    pos.Y += threes * laserDensity.Y;
                }
                else if (i % 3 == 2)
                {
                    pos.X -= threes * laserDensity.X;
                    pos.Y += threes * laserDensity.Y;
                }
                CreateLaser(pos, dir, gameTime);
            }
        }
        protected virtual void CircleLaser(GameTime gameTime, int count, int curveDirX = 1, int curveDirY = 1)
        {
            int iterations = (count - 1) / 3;
            bool isEven = count % 2 == 0;
            for (int i = 0; i < count; ++i)
            {
                Vector2 dir = laserDirection;
                int threes = (1 + i / 3);
                Vector2 pos = new Vector2(position.X - 6 + GetSprite().Width / 2, position.Y) + laserFireOffset;
                if (isEven)
                {
                    pos.X -= laserDensity.X / 2;
                }

                if (!isEven && i == 0)
                {
                    dir.X = 0;
                }
                else if (i % 2 == 0)
                {
                    pos.X += threes * laserDensity.X;
                    pos.Y += curveDirY * threes * laserDensity.Y;
                    dir.X = curveDirX * threes * laserDirection.X;
                }
                else
                {
                    pos.X -= threes * laserDensity.X;
                    pos.Y += curveDirY * threes * laserDensity.Y;
                    dir.X = -curveDirX * threes * laserDirection.X;
                }
                CreateLaser(pos, dir, gameTime);
            }
        }
        protected virtual void ConeLaser(GameTime gameTime, int count)
        {
            CircleLaser(gameTime, count, -1);
        }
        public virtual void SetLaserMode(LaserMode laserMode)
        {
            this.laserMode = laserMode;
            switch (laserMode)
            {
                case LaserMode.Single:
                    laserDensity.Y = 0f;
                    laserDirection.X = 0;
                    break;
                case LaserMode.Double:
                    laserDensity.X = 25;
                    laserDensity.Y = 0f;
                    laserDirection.X = 0;
                    break;
                case LaserMode.Triple:
                    laserDensity.X = 30;
                    laserDensity.Y = 0f;
                    laserDensity.Y = 8f;
                    laserDirection.X = 0;
                    break;
                case LaserMode.Cricle:
                    laserDensity.X = 28;
                    laserDensity.Y = 25f;
                    laserDirection.X = 0.1f;
                    break;
                case LaserMode.Cone:
                    laserDensity.X = 20;
                    laserDensity.Y = 15f;
                    laserDirection.X = 0.05f;
                    break;
            }
        }
    }
}
