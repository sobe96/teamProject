using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Helpers
{
    public class Config
    {
        public static int windowWidth = 1280;
        public static int windowHeight = 720;

        public static GameStatus status = GameStatus.Starting;
        public static int defaultNumOfLives = 3;
        public static int numOfLivesLeft = defaultNumOfLives;

        public static Random rand = new Random();

        public static int MAXSOLDIERS { get; } = 6;

        public static int MAXCAPOS { get; } = 4;

        public static int MAXUNDERBOSS { get; } = 2;

        public static int MAXBOSS { get; } = 1;

        public static int SOL_HP { get; } = 1;
        public static int SOL_LASERTYPE { get; } = 1;
        public static int SOL_LASERMODE { get; } = 0;

        public static int CAPO_HP { get; } = 2;
        public static int CAPO_LASERTYPE { get; } = 2;
        public static int CAPO_LASERMODE { get; } = 1;

        public static int UBOSS_HP { get; } = 3;
        public static int UBOSS_LASERTYPE { get; } = 3;
        public static int UBOSS_LASERMODE { get; } = 2;

        public static int BOSS_HP { get; } = 6;
        public static int BOSS_LASERTYPE { get; } = 4;
        public static int BOSS_LASERMODE { get; } = 4;

        public static int BOSS_TIME { get; } = 15;

        public static int CAPO_TIME { get; } = 5;

        public static int SOLDIER_TIME { get; } = 1;

        public static int UNDERBOSS_TIME { get; } = 10;

        public static bool doneConfiguringSolders { get; set; } = false;

        public static bool doneConfiguringUnderboss { get; set; } = false;
        
        public static bool doneConfiguringCapo { get; set; } = false;
        
        public static bool doneConfiguringBoss { get; set; } = false;
    }
}
