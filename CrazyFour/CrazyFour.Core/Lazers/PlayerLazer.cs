using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Lazers
{
    public class PlayerLazer : ILazer
    {
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public SpriteBatch spriteBatch;

        private const string LAZER_IMAGE = "Images/Lazers/AguaLazer";
        private Texture2D projectile;
        private Vector2 position;

        public bool inGame = true;
        private bool objectComplete = false;
        

        public PlayerLazer(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
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

            objectComplete = true;
        }

        public override void Draw(GameTime game)
        {
            spriteBatch.Draw(projectile, position, Color.White);
        }

        public override void Update(GameTime game)
        {
            Vector2 newPos = new Vector2(position.X, position.Y - 3);

            if (position.Y < 0)
                inGame = false;

            if(inGame)
                position = newPos;
        }

    }
}
