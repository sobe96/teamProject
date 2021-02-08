using CrazyFour.Core.Actors.Enemy;
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

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Config.inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        public void Draw(GameTime gameTime) 
        {
            if (Config.inGame)
            {
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
                GameController.playerLazers.RemoveAll(r => r.inGame is false);

                // Removing any enemy lasors that have done out of the window
                GameController.enemyLazers.RemoveAll(r => r.inGame is false);
            }
        }
    }
}
