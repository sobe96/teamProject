using CrazyFour.Core.Actors.Enemy;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lazers;
using Microsoft.Xna.Framework;
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

        public List<Capo> capos = new List<Capo>();
        public List<Soldier> soldiers = new List<Soldier>();

        public static List<EnemyLazer> enemyLazers = new List<EnemyLazer>();
        public static List<PlayerLazer> playerLazers = new List<PlayerLazer>();

        public double timer = 2D;
        public double maxTime = 2D;
        public static int hz = 60;
        public int nextSpeed = 240;

        public bool inGame = false;
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

        public static void AddLazer(ILazer lazer)
        {
            Type type = lazer.GetType();

            if (type == typeof(EnemyLazer))
                enemyLazers.Add((EnemyLazer)lazer);
            else
                playerLazers.Add((PlayerLazer)lazer);
        }

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach(EnemyLazer enemy in GameController.enemyLazers)
                {
                    enemy.Update(gameTime);
                }

                foreach (PlayerLazer player in GameController.playerLazers)
                {
                    player.Update(gameTime);
                }
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
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
            foreach(EnemyLazer enemy in GameController.enemyLazers)
            {
                enemy.Draw(gameTime);
            }

            foreach (PlayerLazer player in GameController.playerLazers)
            {
                player.Draw(gameTime);
            }

            GameController.playerLazers.RemoveAll(r => r.inGame is false);
        }
    }
}
