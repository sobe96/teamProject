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

        public List<Capo> capos = new List<Capo>();
        public List<Soldier> soldiersList = new List<Soldier>();

        public static List<EnemyLaser> enemyLazers = new List<EnemyLaser>();
        public static List<PlayerLaser> playerLazers = new List<PlayerLaser>();

        public double timer = 2D;
        public double maxTime = 2D;
        public static int hz = 60;
        public int nextSpeed = 240;
        public float totalTime = 0f;

        private ActorFactory factory;
        private const int MAXSOLDIERS = 4;

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

        public static void AddLazer(ILaser lazer)
        {
            Type type = lazer.GetType();

            if (type == typeof(EnemyLaser))
                enemyLazers.Add((EnemyLaser)lazer);
            else
                playerLazers.Add((PlayerLaser)lazer);
        }

        public void LoadContent(ActorFactory fac)
        {
            factory = fac;

            //player = factory.GetActor(ActorTypes.Player);
            //boss = factory.GetActor(ActorTypes.Boss);
            //underboss = factory.GetActor(ActorTypes.Underboss);
            //capo = factory.GetActor(ActorTypes.Capo);
            //soldier = factory.GetActor(ActorTypes.Soldier);
        }

        public void InitializeEnemies(GameTime game, ActorTypes type)
        {
            switch(type)
            {
                case ActorTypes.Boss:
                    break;
                case ActorTypes.Capo:
                    break;
                case ActorTypes.Player:
                    break;
                case ActorTypes.Soldier:
                    if (!doneConfiguringSolders)
                    {
                        totalTime += (float)game.ElapsedGameTime.TotalSeconds;

                        // spawning enemy solders
                        if ((int)totalTime > 1 && soldiersList.Count < MAXSOLDIERS)
                        {
                            for (int i = 0; i <= MAXSOLDIERS; i++)
                            {
                                var sol = (Soldier)factory.GetActor(ActorTypes.Soldier);
                                soldiersList.Add(sol);
                            }

                            doneConfiguringSolders = true;
                        }
                    }
                    break;
                case ActorTypes.Underboss:
                    break;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Config.inGame)
            {
                // Updating the enemy's position
                foreach (Soldier sol in soldiersList)
                {
                    sol.Draw(gameTime);
                }

                // Updating position for the enemy lasers
                foreach (EnemyLaser enemy in GameController.enemyLazers)
                {
                    enemy.Draw(gameTime);
                }

                // Updating position for the player lasers
                foreach (PlayerLaser player in GameController.playerLazers)
                {
                    player.Draw(gameTime);
                }

                // Removing any player lasors that have gone out of window
                GameController.playerLazers.RemoveAll(r => r.isActive is false);

                // Removing any enemy lasors that have done out of the window
                GameController.enemyLazers.RemoveAll(r => r.isActive is false);
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            InitializeEnemies(gameTime, ActorTypes.Soldier);

            if (Config.inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach(Soldier sol in soldiersList)
                {
                    sol.Update(gameTime, playerPosition);
                }

                foreach(EnemyLaser enemy in GameController.enemyLazers)
                {
                    enemy.Update(gameTime);
                }

                foreach (PlayerLaser player in GameController.playerLazers)
                {
                    foreach (Soldier sol in soldiersList)
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

                GameController.playerLazers.RemoveAll(r => r.isHit);
                soldiersList.RemoveAll(r => r.isHit);
            }
        }

        
    }
}
