using CrazyFour.Core.Actors.Enemy;
using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Lasers
{
    public class PlayerLaser : ILaser
    {
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public SpriteBatch spriteBatch;

        private const string LAZER_IMAGE = "Images/Lazers/AguaLazer";
        private Texture2D projectile;
        public Vector2 position;
        private int speed;

        public static int radius { get; } = 6;

        public bool isActive { get; set; } = true;

        public bool isHit { get; set; } = false;


        public PlayerLaser(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
        {
            graphics = gra;
            spriteBatch = spr;
            content = con;
        }

        public override void Initialize(ActorTypes type, Vector2 pos)
        {
            position = pos;

            switch (type)
            {
                case ActorTypes.Player:
                    projectile = content.Load<Texture2D>("Images/Lazers/AguaLazer");
                    break;
                case ActorTypes.Boss:
                    projectile = content.Load<Texture2D>("Images/Lazers/RedLazer");
                    break;
                case ActorTypes.Underboss:
                    projectile = content.Load<Texture2D>("Images/Lazers/YellowLazer");
                    break;
                case ActorTypes.Capo:
                    projectile = content.Load<Texture2D>("Images/Lazers/GreenLazer");
                    break;
                case ActorTypes.Soldier:
                    projectile = content.Load<Texture2D>("Images/Lazers/BlueLazer");
                    break;
                default:
                    throw new NotImplementedException();
            }

        }

        public override void Draw(GameTime game)
        {
            spriteBatch.Draw(projectile, position, Color.White);
        }

        public override void Update(GameTime game)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)game.ElapsedGameTime.TotalSeconds;

            // use controlling the speed of the game by pressing the S key
            if (kState.IsKeyDown(Keys.S))
                speed = (int)Speed.HalfSpeed * GameController.hz;
            else
                speed = (int)Speed.Normal * GameController.hz;

            position.Y -= speed * dt;

            if (position.Y < 0)
                isActive = false;

            
        }

    }
}
