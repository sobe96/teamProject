using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Movements
{
    public class BossMovement
    {
        // Bridge pattern
        public static Vector2 bossMovement(GraphicsDeviceManager g, Vector2 cp, Vector2 center, Vector2 left, Vector2 right, Vector2 move, int rad, bool initialized)
        {
            if (cp.X == g.PreferredBackBufferWidth / 2 && Math.Round(cp.Y) <= 0 - 2 * rad)
            {
                move = center - cp;
            }
            if (cp.X == center.X && Math.Round(cp.Y) == center.Y && !initialized)
            {
                move = left - cp;
                initialized = true;
            }
            if (Math.Round(cp.X) == left.X && Math.Round(cp.Y) == left.Y)
            {
                move = right - cp;
            }
            if (Math.Round(cp.X) == right.X && Math.Round(cp.Y) == right.Y)
            {
                move = left - cp;
            }

            return move;
        }
    }
}
