using Microsoft.Xna.Framework;
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
        public static string PLAYER_SPRITE { get; } = "Images/Players/hero";
        public static string PLAYER_LASER_SPRITE { get; } = "Images/Lazers/AguaLazer";
        public static int PLAYER_AVAILABLE_LASERS { get; } = 5;

        public static int MAX_SOLDIERS { get; } = 6;
        public static int SOLDIER_TIME { get; } = 1;
        public static string SOLDIER_SPRITE { get; } = "Images/Players/soldier";
        public static string SOLDIER_LASER_SPRITE { get; } = "Images/Lazers/BlueLazer";
        public static int SOLDIER_HP { get; } = 1;
        public static int SOLDIER_LASERMODE { get; } = 0;

        public static int MAX_CAPOS { get; } = 4;
        public static int CAPO_TIME { get; } = 5;
        public static string CAPO_SPRITE { get; } = "Images/Players/capo";
        public static string CAPO_LASER_SPRITE { get; } = "Images/Lazers/GreenLazer";
        public static int CAPO_HP { get; } = 2;
        public static int CAPO_LASERMODE { get; } = 1;

        public static int MAX_UBOSS { get; } = 2;
        public static int UBOSS_TIME { get; } = 10;
        public static string UBOSS_SPRITE { get; } = "Images/Players/underboss";
        public static string UBOSS_LASER_SPRITE { get; } = "Images/Lazers/YellowLazer";
        public static int UBOSS_HP { get; } = 3;
        public static int UBOSS_LASERMODE { get; } = 2;

        public static int MAX_BOSS { get; } = 1;
        public static int BOSS_TIME { get; } = 15;
        public static string BOSS_SPRITE { get; } = "Images/Players/boss";
        public static string BOSS_LASER_SPRITE { get; } = "Images/Lazers/RedLazer";
        public static int BOSS_HP { get; } = 6;
        public static int BOSS_LASERMODE { get; } = 3;

        //must be odd number!!
        public static int CIRCLE_LASER_COUNT { get; } = 5;
        public static int CONE_LASER_COUNT { get; } = 5;

        public static Vector2 DOUBLE_LASER_DENSITY { get; } = new Vector2(25, 0);
        public static Vector2 TRIPLE_LASER_DENSITY { get; } = new Vector2(30, 8);

        public static Vector2 CIRCLE_LASER_DENSITY { get; } = new Vector2(28, 25);
        public static float CIRCLE_LASER_DIRECTION_X_MODIF { get; } = 0.1f;
        
        public static Vector2 CONE_LASER_DENSITY { get; } = new Vector2(20, 15);
        public static float CONE_LASER_DIRECTION_X_MODIF { get; } = 0.05f;




        public static bool doneConfiguringSolders { get; set; } = false;

        public static bool doneConfiguringUnderboss { get; set; } = false;
        
        public static bool doneConfiguringCapo { get; set; } = false;
        
        public static bool doneConfiguringBoss { get; set; } = false;
    }
}
