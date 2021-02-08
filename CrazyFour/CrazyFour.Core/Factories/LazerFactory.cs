using CrazyFour.Core.Helpers;
using CrazyFour.Core.Lazers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Factories
{
    class LazerFactory : ILazerFactory
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager content;

        public LazerFactory(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
        {
            graphics = gra;
            spriteBatch = spr;
            content = con;
        }

        public ILazer GetLazer(LazerType type, Vector2 pos, GameTime game)
        {
            switch (type)
            {
                case LazerType.Enemy:
                    EnemyLazer enemy = new EnemyLazer(graphics, spriteBatch, content);
                    enemy.Initialize(ActorTypes.Boss, pos);
                    return enemy;

                case LazerType.Player:
                    PlayerLazer player = new PlayerLazer(graphics, spriteBatch, content);
                    player.Initialize(ActorTypes.Player, pos);
                    player.Update(game);
                    return player;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
