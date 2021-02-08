using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Factories
{
    interface ILaserFactory
    {
        ILaser GetLazer(LazerType type, Vector2 pos, GameTime game);
    }
}
