using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lasers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Factories
{
    class LaserFactory : ILaserFactory
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;

        public LaserFactory(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
        {
            graphics = gra;
            spriteBatch = spr;
            content = con;
        }

        public ILaser GetLazer(LaserType type, Vector2 pos, GameTime game)
        {
            ILaser actor;
            actor = new EnemyLaser(graphics, spriteBatch, content);

            switch (type)
            {
                case LaserType.Boss:
                    actor.Initialize(ActorTypes.Boss, pos);
                    break;

                case LaserType.Underboss:
                    actor.Initialize(ActorTypes.Underboss, pos);
                    break;

                case LaserType.Capo:
                    actor.Initialize(ActorTypes.Capo, pos);
                    break;

                case LaserType.Soldier:
                    actor.Initialize(ActorTypes.Soldier, pos);
                    break;

                case LaserType.Player:
                    actor = new PlayerLaser(graphics, spriteBatch, content);
                    actor.Initialize(ActorTypes.Player, pos);
                    break;

                default:
                    throw new ArgumentException();
            }

            actor.Update(game);
            return actor;
        }
    }
}
