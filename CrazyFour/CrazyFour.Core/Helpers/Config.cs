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

        public static Random rand = new Random();

        public static int MAXSOLDIERS { get; } = 4;

        public static int MAXCAPOS { get; } = 4;

        public static int MAXUNDERBOSS { get; } = 2;

        public static int MAXBOSS { get; } = 1;

        public static int SOL_HP { get; } = 3;
        
        public static int CAPO_HP { get; } = 2;
        
        public static int UBOSS_HP { get; } = 3;
        
        public static int BOSS_HP { get; } = 4;

        public static bool doneConfiguringSolders { get; set; } = false;

        public static bool doneConfiguringUnderboss { get; set; } = false;
        
        public static bool doneConfiguringCapo { get; set; } = false;
        
        public static bool doneConfiguringBoss { get; set; } = false;
    }
}
