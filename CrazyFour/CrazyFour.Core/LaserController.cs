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
        public static List<ILaser> enemyLasers = new List<ILaser>();
        public static List<PlayerLaser> playerLasers = new List<PlayerLaser>();

        public void ProcessLasers(GameTime gameTime)
        {
            foreach (PlayerLaser laser in playerLasers)
            {
                foreach(IActor enemy in GameController.enemyList)
                {
                    CheckCollision(gameTime, laser, enemy);
                }
            }

            RemoveLasers();
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
                    actor.hitCounter = 0;
                    actor.isHit = true;
                }
            }

            laser.Update(gameTime);
        }

        public void RemoveLasers()
        {
            // Removing any player lasors that have gone out of window
            LaserController.playerLasers.RemoveAll(r => r.isActive is false || r.isHit);

            // Removing any enemy lasors that have done out of the window
            LaserController.enemyLasers.RemoveAll(r => r.isActive is false || r.isHit);
        }

    }
}
