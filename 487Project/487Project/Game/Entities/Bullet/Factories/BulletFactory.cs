using BulletBlaster.Game.config;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet.Factories
{
    internal abstract class BulletFactory
    {
        protected Texture2D texture;
        protected BulletPatternConfig config;
        protected BulletBehavior behavior;

        public BulletFactory(BulletPatternConfig pattern, Texture2D sprite)
        {
            texture = sprite;
            config = pattern;
        }

        public abstract Bullet Create(Vector2 velocity, Vector2 startPos);
    }
}
