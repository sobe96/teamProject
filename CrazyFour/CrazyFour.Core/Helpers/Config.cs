using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Helpers
{
    public class Config
    {
        public static int windowWidth = 1280;
        public static int windowHeight = 720;

        public static bool inGame = false;
        public static bool isDead = false;
        public static int defaultNumOfLives = 3;
        public static int numOfLivesLeft = defaultNumOfLives;
    }
}
