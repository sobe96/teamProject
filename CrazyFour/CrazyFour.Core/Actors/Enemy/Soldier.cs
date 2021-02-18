using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Enemy
{
    public class Soldier : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/soldier";
        private const string LAZER_IMAGE = "Images/Lazers/BlueLazer";
        private const int SOL_HP = 3;
        private float speed;
        private int counterHits = 0;
        private Random rand = new Random();

        private float initCounter = 5f;
        private float counter = 0.5f;

        public static int radius { get; } = 15;


        public Soldier(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = 1f * (float)GameController.hz;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
            LoadSprite(LoadType.Lazer, LAZER_IMAGE);
            inGame = true;

            // Randomizing starting point
            int width = rand.Next(GetRadius(), graphics.PreferredBackBufferWidth - GetRadius());
            int height = rand.Next(GetRadius() * 1, (GetRadius() * 3) * 1);

            defaultPosition = new Vector2(width, height);
            currentPosition = defaultPosition;

        }

        public Vector2 GetSoldierPosition()
        {
            return position;
        }

        public override void Draw(GameTime gameTime)
        {
            if (inGame)
            {
                spriteBatch.Draw(GetSprite(), currentPosition, Color.White);
            }
            else
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
        }

        public override void Update(GameTime gameTime, Vector2? pp)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            playerPosition = (Vector2)pp;

            if (inGame)
            {
                Vector2 move = playerPosition - currentPosition;
                move.Normalize();
                currentPosition += move * speed * dt;

                counter -= dt;
                if (counter <= 0)
                {

                    LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                    ILaser lazerSol = factory.GetLazer(LazerType.Soldier, new Vector2(currentPosition.X + radius - 3, currentPosition.Y + 15), gameTime);

                    GameController.AddLazer(lazerSol);
                    counter = initCounter / 10;
                }

                
                

            }


        }
    }
}
