using CrazyFour.Core.Actors.Enemy;
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

        public double timer = 2D;
        public double maxTime = 2D;
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

        public void Initialize()
        { 

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

        }
    }
}
