using CrazyFour.Core.Actors;
using CrazyFour.Core.Actors.Enemy;
using CrazyFour.Core.Actors.Hero;
using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrazyFour.Core
{
    //Break the GC in microControllers
    public sealed class GameController
    {
        private static readonly object padlock = new object();
        private static GameController instance = null;

        public List<Texture2D> soldierSprites = new List<Texture2D>();

        public static List<IActor> enemyList = new List<IActor>();
        public static List<ILaser> enemyLasers = new List<ILaser>();
        public static List<PlayerLaser> playerLasers = new List<PlayerLaser>();

        public static int hz = 60;
        public static float totalTime = 0f;

        private ActorFactory factory;
        int wave = 0;


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

            if (type == typeof(EnemyLaser))
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
                    // Flip time and Configuration of enemies
                    if ((int)totalTime == 135 || (wave == 3 && enemyList.Count == 0))
                    {
                        if (!Config.doneConfiguringBoss)
                        {
                            wave = 4;
                            for (int i = 0; i < Config.MAXBOSS; i++)
                            {
                                
                                var sol = (Boss)factory.GetActor(ActorTypes.Boss, i);
                                enemyList.Add(sol);
                            }

                            Config.doneConfiguringBoss = true;
                        }
                    }
                    break;
                case ActorTypes.Capo:
                    if (((int)totalTime == 45 || (wave == 1 && enemyList.Count == 0)))
                    {
                        if (!Config.doneConfiguringCapo)
                        {
                            wave = 2;
                            for (int i = 0; i < Config.MAXCAPOS; i++)
                            {
                                var capo = (Capo)factory.GetActor(ActorTypes.Capo, i);
                                enemyList.Add(capo);
                            }

                            Config.doneConfiguringCapo = true;
                        }
                    }
                    break;
                case ActorTypes.Soldier:
                    if ((int)totalTime == 1 && wave == 0)
                    {
                        if (!Config.doneConfiguringSolders)
                        {
                            wave = 1;
                            for (int i = 0; i < Config.MAXSOLDIERS; i++)
                            {
                                var sol = (Soldier)factory.GetActor(ActorTypes.Soldier, i);
                                enemyList.Add(sol);
                            }

                            Config.doneConfiguringSolders = true;
                        }
                    }
                    break;
                case ActorTypes.Underboss:
                    if ((int)totalTime == 90 || (wave == 2 && enemyList.Count == 0))
                    {
                        if (!Config.doneConfiguringUnderboss)
                        {
                            wave = 3;
                            for (int i = 0; i < Config.MAXUNDERBOSS; i++)
                            {
                                var sol = (Underboss)factory.GetActor(ActorTypes.Underboss, i);
                                enemyList.Add(sol);
                            }

                            Config.doneConfiguringUnderboss = true;
                        }
                    }
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        private bool InitializeEnemiesObjects(GameTime gameTime)
        {
            InitializeEnemies(gameTime, ActorTypes.Boss);
            InitializeEnemies(gameTime, ActorTypes.Underboss);
            InitializeEnemies(gameTime, ActorTypes.Capo);
            InitializeEnemies(gameTime, ActorTypes.Soldier);


            if (Config.doneConfiguringSolders) return true;
            else if (Config.doneConfiguringUnderboss) return true;
            else if (Config.doneConfiguringCapo) return true;
            else if (Config.doneConfiguringBoss) return true;
            else 
                return false;

        }

        public void Draw(GameTime gameTime)
        {
            if (Config.status == GameStatus.Playing)
            {
                // Updating the enemy's position
                foreach (var sol in enemyList)
                {
                    sol.Draw(gameTime);
                }

                // Updating position for the enemy lasers
                foreach (EnemyLaser enemy in GameController.enemyLasers)
                {
                    enemy.Draw(gameTime);
                }

                // Updating position for the player lasers
                foreach (PlayerLaser player in GameController.playerLasers)
                {
                    player.Draw(gameTime);
                }
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            if (Config.status == GameStatus.Playing)
            {
                if(!InitializeEnemiesObjects(gameTime))
                {
                    // means no enemies have been created, so skipping
                    return;
                }

                foreach (var sol in enemyList)
                {
                    sol.Update(gameTime, player.GetPlayerPosition());
                }

                bool hit = false;

                foreach (EnemyLaser lasor in GameController.enemyLasers)
                {
                    lasor.Update(gameTime);
                    hit = lasor.CheckHit(gameTime, player);

                    if (hit)
                        break;
                }

                if (hit)
                    GameController.enemyLasers.Clear();

                // Removing any player lasors that have gone out of window
                GameController.playerLasers.RemoveAll(r => r.isActive is false || r.isHit);

                // Removing any enemy lasors that have done out of the window
                GameController.enemyLasers.RemoveAll(r => r.isActive is false || r.isHit);

                // Removing the enemies from our list
                enemyList.RemoveAll(r => r.isActive is false || r.isHit);


                if (enemyList.Count == 0 && wave == 4)
                {
                    Config.status = GameStatus.Gameover;
                }
                    
            }
        }
        
    }
}
