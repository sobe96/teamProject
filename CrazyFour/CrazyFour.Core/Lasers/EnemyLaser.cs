using CrazyFour.Core.Actors;
using CrazyFour.Core.Actors.Enemy;
using CrazyFour.Core.Actors.Hero;
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
    public class EnemyLaser : ILaser
    {
        private Texture2D projectile;
        public Vector2 position;

        public float speed { get; set; }

        public int radius { get; } = 6;

        public bool isActive { get; set; } = true;


        public EnemyLaser(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
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
                speed = Utilities.ConvertToPercentage(Speed.QuarterSpeed) * GameController.hz;
            else
                speed = Utilities.ConvertToPercentage(Speed.Normal) * GameController.hz;

            position.Y += 2f * speed * dt;

            // preping for removal
            if (position.Y < 0)
                isActive = false;
        }

        public override bool CheckHit(Player player)
        {
            int sum = radius + player.radius;
            float dis = Vector2.Distance(position, player.GetPlayerTruePosition());

            if (dis <= sum)
            {
                player.isHit = true;
                player.Lives -= 1;
                return true;
            }

            return false;
        }
    }
}
