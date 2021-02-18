using CrazyFour.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFour.Core.Lasers
{
    public class SoldierLaser : ILaser
    {
        private const string LASER_IMAGE = "Images/Lazers/GreenLazer";

        private Texture2D projectile;
        private Vector2 position;
        private string img;
        private int radius = 6;



        public SoldierLaser(GraphicsDeviceManager gra, SpriteBatch spr, ContentManager con)
        {
            graphics = gra;
            spriteBatch = spr;
            content = con;
        }

        public override void Initialize(ActorTypes type, Vector2 pos)
        {
            position = pos;
        }

        public override void Draw(GameTime game)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime game)
        {
            throw new NotImplementedException();
        }
    }
}
