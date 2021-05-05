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
    public class CrossMovement
    {
        // Bridge pattern
        public static Vector2 crossMovement(Vector2 cp, Vector2 positionRightBot, Vector2 positionLeftBot, Vector2 positionRightTop, Vector2 positionLeftTop, Vector2 move)
        {
            /*private static Vector2 positionRightBot;
            private static Vector2 positionLeftBot;
            private static Vector2 positionRightTop;
            private static Vector2 positionLeftTop;*/

            /*float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 positionRightBot = new Vector2(g.PreferredBackBufferWidth, g.PreferredBackBufferHeight / 2);
            Vector2 positionLeftBot = new Vector2(0 - 2 * rad, g.PreferredBackBufferHeight / 2);
            Vector2 positionRightTop = new Vector2(g.PreferredBackBufferWidth, 0);
            Vector2 positionLeftTop = new Vector2(0 - 2 * rad, 0);*/

            if (Math.Round(cp.X) <= positionLeftTop.X && cp.Y <= positionLeftTop.Y)
            {
                move = positionRightBot - cp;
            }
            if (cp.X >= positionRightBot.X && cp.Y >= positionRightBot.Y)
            {
                move = positionRightTop - cp;
            }
            if (Math.Round(cp.X) >= positionRightTop.X && cp.Y <= positionRightTop.Y)
            {
                move = positionLeftBot - cp;
            }
            if (cp.X <= positionLeftBot.X && cp.Y >= positionLeftBot.Y)
            {
                move = positionLeftTop - cp;
            }

            return move; 
        }
    }
}
