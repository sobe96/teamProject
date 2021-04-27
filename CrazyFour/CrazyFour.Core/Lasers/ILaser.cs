using CrazyFour.Core.Actors;
using CrazyFour.Core.Actors.Hero;
using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Lasers
{
    public abstract class ILaser
    {
        protected Texture2D spriteImage;
        protected Texture2D lasorImage;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;
        protected bool isSpriteLoaded = false;

        public bool isActive = true;
        public bool isHit = false;

        public virtual int radius { get; } = 6;
        public Vector2 direction;



        public abstract void Initialize(ActorTypes type, Vector2 pos, Vector2 dir);

        public virtual bool LoadSprite(LoadType type, String img)
        {
            switch (type)
            {
                case LoadType.Ship:
                    spriteImage = content.Load<Texture2D>(img);
                    isSpriteLoaded = true;
                    break;
                case LoadType.Laser:
                    lasorImage = content.Load<Texture2D>(img);
                    break;
            }

            return true;
        }

        public abstract void Draw(GameTime game);

        public abstract void Update(GameTime game);

        public abstract bool CheckHit(GameTime gameTime, Player playerPos);
    }
}
