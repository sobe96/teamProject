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
        public List<Soldier> soldiers = new List<Soldier>();

        public static List<EnemyLaser> enemyLazers = new List<EnemyLaser>();
        public static List<PlayerLaser> playerLazers = new List<PlayerLaser>();

        public double timer = 2D;
        public double maxTime = 2D;
        public static int hz = 60;
        public int nextSpeed = 240;
        public float totalTime = 0f;

        private ActorFactory factory;
        private const int MAXSOLDIERS = 4;

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

        public void Draw(GameTime gameTime)
        {
            if (Config.inGame)
            {
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // spawning enemy solders
                if ((int)totalTime > 5 && soldiers.Count < MAXSOLDIERS)
                {
                    Soldier sol = (Soldier)factory.GetActor(ActorTypes.Soldier);
                    sol.Draw(gameTime);

                    //IActor player = factory.GetActor(ActorTypes.Player);
                    soldiers.Add(sol);
                }

                // Updating the enemy's position
                foreach (Soldier sol in soldiers)
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
            if (Config.inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach(Soldier sol in soldiers)
                {
                    sol.Update(gameTime, playerPosition);
                }

                foreach(EnemyLaser enemy in GameController.enemyLazers)
                {
                    enemy.Update(gameTime);
                }

                foreach (PlayerLaser player in GameController.playerLazers)
                {
                    player.Update(gameTime);
                }
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    Config.inGame = true;
                    totalTime = 0;
                    timer = 2D;
                    maxTime = 2D;
                    nextSpeed = 240;
                }
            }

            if (timer <= 0)
            {
                //asteroids.Add(new Asteroid(nextSpeed));
                timer = maxTime;

                if (maxTime > 0.5)
                    maxTime -= 0.1D;

                if (nextSpeed < 720)
                    nextSpeed += 4;
            }
        }

        
    }
}
