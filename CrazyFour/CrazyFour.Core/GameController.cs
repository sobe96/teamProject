using CrazyFour.Core.Actors;
using CrazyFour.Core.Actors.Enemy;
using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core
{
    public sealed class GameController
    {
        private static readonly object padlock = new object();
        private static GameController instance = null;

        public List<Texture2D> soldierSprites = new List<Texture2D>();

        public List<IActor> enemyList = new List<IActor>();
        public static List<ILaser> enemyLasers = new List<ILaser>();
        public static List<PlayerLaser> playerLasers = new List<PlayerLaser>();

        public static int hz = 60;
        public static float totalTime = 0f;

        private ActorFactory factory;
        private const int MAXSOLDIERS = 6;
        private const int MAXCAPOS = 4;
        private const int MAXUNDERBOSS = 2;
        private const int MAXBOSS = 1;

        private bool doneConfiguringSolders = false;
        private bool doneConfiguringUnderboss = false;
        private bool doneConfiguringCapo = false;
        private bool doneConfiguringBoss = false;

        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new GameController();
                        }
                    }
                }

                return instance;
            }
        }

        private GameController() { }

        public static void AddLaser(ILaser laser)
        {
            Type type = laser.GetType();

            if (type == typeof(SoldierLaser))
                enemyLasers.Add(laser);
            else
                playerLasers.Add((PlayerLaser)laser);
        }

        public void LoadContent(ActorFactory fac)
        {
            factory = fac;
        }

       public void InitializeEnemies(GameTime game, ActorTypes type)
        {
            switch(type)
            {
                case ActorTypes.Boss:
                    if (!doneConfiguringBoss)
                    {
                        if ((int)totalTime >= 15)
                        {
                            for (int i = 0; i < MAXBOSS; i++)
                            {
                                var sol = (Boss)factory.GetActor(ActorTypes.Boss);
                                enemyList.Add(sol);
                            }

                            doneConfiguringBoss = true;
                        }
                    }
                    break;
                case ActorTypes.Capo:
                    if (!doneConfiguringCapo)
                    {
                        if ((int)totalTime >= 5)
                        {
                            for (int i = 0; i < MAXCAPOS; i++)
                            {
                                var sol = (Capo)factory.GetActor(ActorTypes.Capo);
                                enemyList.Add(sol);
                            }

                            doneConfiguringCapo = true;
                        }
                    }
                    break;
                case ActorTypes.Player:
                    break;
                case ActorTypes.Soldier:
                    if (!doneConfiguringSolders)
                    {
                        if ((int)totalTime >= 1)
                        {
                            for (int i = 0; i < MAXSOLDIERS; i++)
                            {
                                var sol = (Soldier)factory.GetActor(ActorTypes.Soldier);
                                enemyList.Add(sol);
                            }

                            doneConfiguringSolders = true;
                        }
                    }
                    break;
                case ActorTypes.Underboss:
                    if (!doneConfiguringUnderboss)
                    {
                        if ((int)totalTime >= 10)
                        {
                            for (int i = 0; i < MAXUNDERBOSS; i++)
                            {
                                var sol = (Underboss)factory.GetActor(ActorTypes.Underboss);
                                enemyList.Add(sol);
                            }

                            doneConfiguringUnderboss = true;
                        }
                    }
                    break;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Config.inGame)
            {
                // Updating the enemy's position
                foreach (var sol in enemyList)
                {
                    sol.Draw(gameTime);
                }

                // Updating position for the enemy lasers
                foreach (SoldierLaser enemy in GameController.enemyLasers)
                {
                    enemy.Draw(gameTime);
                }

                // Updating position for the player lasers
                foreach (PlayerLaser player in GameController.playerLasers)
                {
                    player.Draw(gameTime);
                }

                // Removing any player lasors that have gone out of window
                GameController.playerLasers.RemoveAll(r => r.isActive is false);

                // Removing any enemy lasors that have done out of the window
                GameController.enemyLasers.RemoveAll(r => r.isActive is false);
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (Config.inGame)
            {
                InitializeEnemies(gameTime, ActorTypes.Soldier);
                InitializeEnemies(gameTime, ActorTypes.Capo);
                InitializeEnemies(gameTime, ActorTypes.Underboss);
                InitializeEnemies(gameTime, ActorTypes.Boss);

                //totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (var sol in enemyList)
                {
                    sol.Update(gameTime, playerPosition);
                }

                foreach (SoldierLaser enemy in GameController.enemyLasers)
                {
                    enemy.Update(gameTime);
                }

                foreach (PlayerLaser player in GameController.playerLasers)
                {
                    foreach (var sol in enemyList)
                    {
                        int sum = Soldier.radius + PlayerLaser.radius;

                        if (Vector2.Distance(player.position, sol.currentPosition) < sum)
                        {
                            sol.isHit = true;
                            player.isHit = true;
                        }
                    }

                    player.Update(gameTime);
                }

                GameController.playerLasers.RemoveAll(r => r.isActive is false || r.isHit);
                GameController.enemyLasers.RemoveAll(r => r.isActive is false || r.isHit);
                enemyList.RemoveAll(r => r.isHit);
            }
        }

        
    }
}
