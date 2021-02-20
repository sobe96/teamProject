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

        public ILaser GetLazer(LazerType type, Vector2 pos, GameTime game)
        {
            ILaser actor;

            switch (type)
            {
                case LazerType.Boss:
                    EnemyLaser enemyBoss = new EnemyLaser(graphics, spriteBatch, content);
                    enemyBoss.Initialize(ActorTypes.Boss, pos);
                    enemyBoss.Update(game);
                    return enemyBoss;

                case LazerType.Soldier:
                    EnemyLaser enemySoldier = new EnemyLaser(graphics, spriteBatch, content);
                    enemySoldier.Initialize(ActorTypes.Soldier, pos);
                    enemySoldier.Update(game);
                    return enemySoldier;

                case LazerType.Player:
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
