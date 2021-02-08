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
            switch (type)
            {
                case LazerType.Enemy:
                    EnemyLaser enemy = new EnemyLaser(graphics, spriteBatch, content);
                    enemy.Initialize(ActorTypes.Boss, pos);
                    return enemy;

                case LazerType.Player:
                    PlayerLaser player = new PlayerLaser(graphics, spriteBatch, content);
                    player.Initialize(ActorTypes.Player, pos);
                    player.Update(game);
                    return player;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
