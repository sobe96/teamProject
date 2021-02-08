﻿using CrazyFour.Core.Factories;
using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Actors.Hero
{
    public class Player : IActor
    {
        private const string SPRITE_IMAGE = "Images/Players/hero";
        private int speed;
        private bool isFiring = false;

        public Player(GraphicsDeviceManager g, SpriteBatch s, ContentManager c)
        {
            graphics = g;
            spriteBatch = s;
            content = c;

            // defining the default speed
            speed = 4 * GameController.hz;

            LoadSprite(LoadType.Ship, SPRITE_IMAGE);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!inGame)
            {
                defaultPosition = new Vector2(graphics.PreferredBackBufferWidth / 2 - (int)(GetSprite().Width / 2), graphics.PreferredBackBufferHeight - GetSprite().Height);
                spriteBatch.Draw(GetSprite(), defaultPosition, Color.White);
                inGame = true;
                position = defaultPosition;
            }
            else
                spriteBatch.Draw(GetSprite(), new Vector2(position.X, position.Y), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // use controlling the speed of the game by pressing the S key
            if (kState.IsKeyDown(Keys.S))
                speed = (int)Speed.QuarterSpeed * GameController.hz;
            else 
                speed = (int)Speed.Normal * GameController.hz;


            // Moving the player
            if (kState.IsKeyDown(Keys.Right) && position.X < graphics.PreferredBackBufferWidth + 1 - GetSprite().Width)
                position.X += speed * dt;

            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
                position.X -= speed * dt;

            if (kState.IsKeyDown(Keys.Down) && position.Y < graphics.PreferredBackBufferHeight + 1 - GetSprite().Height)
                position.Y += speed * dt;

            if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
                position.Y -= speed * dt;


            // Firing projectile but making sure we fire only one at a time
            if (!isFiring)
            {
                if (kState.IsKeyDown(Keys.Space))
                {
                    isFiring = true;

                    LaserFactory factory = new LaserFactory(graphics, spriteBatch, content);
                    ILaser lazer = factory.GetLazer(LazerType.Player, position, gameTime);

                    GameController.AddLazer(lazer);
                    //position.X += speed * dt;
                }
            }

            // releasing the flag once we fire one
            if (kState.IsKeyUp(Keys.Space))
            {
                isFiring = false;
            }
        }
    }
}
