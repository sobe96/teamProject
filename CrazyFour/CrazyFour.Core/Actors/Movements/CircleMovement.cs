using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using CrazyFour.Core.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Movements
{
    public class CircleMovement
        
    {
        public static int rad = 500;

        public static Vector2 circleMovementLeft(GraphicsDeviceManager g,Vector2 cp, Vector2 originLeft, Vector2 move, float angle, float dt)
        {
            move.X = (float)(originLeft.X - Math.Sin(angle) * rad) - cp.X;
            move.Y = (float)(originLeft.Y + Math.Cos(angle) * rad) - cp.Y;
            angle = angle + 2 * dt / 3;
            if (angle >= 360)
            {
                angle = 0;
            }
            return move;
        }
        public static Vector2 circleMovementRight(GraphicsDeviceManager g, Vector2 cp, Vector2 originRight, Vector2 move, float angle, float dt)
        {
            move.X = (float)(originRight.X + Math.Sin(angle) * rad) - cp.X;
            move.Y = (float)(originRight.Y + Math.Cos(angle) * rad) - cp.Y;
            angle = angle + 2 * dt / 3;
            if (angle >= 360)
            {
                angle = 0;
            }
            return move;
        }
    }
}
