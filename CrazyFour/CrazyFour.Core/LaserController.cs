using CrazyFour.Core.Actors;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core
{
    public class LaserController
    {
        public static List<IActor> enemyList = new List<IActor>();
        public static List<ILaser> enemyLasers = new List<ILaser>();
        public static List<PlayerLaser> playerLasers = new List<PlayerLaser>();

        public void ProcessLasers(GameTime gameTime)
        {
            foreach (PlayerLaser laser in playerLasers)
            {
                foreach(IActor enemy in enemyList)
                {
                    CheckCollision(gameTime, laser, enemy);
                }
            }
        }

        public static void AddLaser(ILaser laser)
        {
            Type type = laser.GetType();

            if (type == typeof(EnemyLaser))
                LaserController.enemyLasers.Add(laser);
            else
                LaserController.playerLasers.Add((PlayerLaser)laser);
        }

        public void CheckCollision(GameTime gameTime, PlayerLaser laser, IActor actor)
        {
            int sum = actor.radius + laser.radius;

            if (Vector2.Distance(laser.position, actor.currentPosition) < sum)
            {
                actor.hitCounter += 1;
                laser.isHit = true;

                if (actor.hitCounter == Config.BOSS_HP)
                {
                    actor.isHit = true;
                    actor.hitCounter = 0;
                }
            }

            laser.Update(gameTime);
        }

        public void RemoveLasers()
        {

        }

        public void AddLaser()
        {

        }
    }
}
