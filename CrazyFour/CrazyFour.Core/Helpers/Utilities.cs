using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Helpers
{
    public class Utilities
    {
        public static String TicksToTime(double t)
        {
            int totalSeconds = (int)Math.Ceiling(t);

            int seconds = totalSeconds % 60;
            int minutes = totalSeconds / 60;
            string time = minutes.ToString("D2") + ":" + seconds.ToString("D2");

            return time;
        }
    }
}
