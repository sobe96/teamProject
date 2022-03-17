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
