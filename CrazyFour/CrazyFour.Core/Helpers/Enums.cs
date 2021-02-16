﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Helpers
{
    public enum ActorTypes
    {
        Player,
        Boss,
        Underboss,
        Capo,
        Soldier
    }

    public enum Speed
    {
        Normal = 4,
        ThreeQuarterSpeed = 3,
        HalfSpeed = 2,
        QuarterSpeed = 1
    }

    public enum LoadType
    {
        Ship,
        Lazer
    }

    public enum LazerType
    {
        Enemy,
        Player
    }

    public enum Direction
    {
        Top = -1,
        Right = 1,
        Bottom = 1,
        Left = -1
    }
}
