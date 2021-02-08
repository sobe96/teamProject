﻿using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lazers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Factories
{
    interface ILazerFactory
    {
        ILazer GetLazer(LazerType type, Vector2 pos, GameTime game);
    }
}
