using System;
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
        Slow = 3,
        Slowest = 2
    }
}
