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
        public abstract void Initialize(ActorTypes type, Vector2 pos);

        public abstract void Draw(GameTime game);

        public abstract void Update(GameTime game);
    }
}
