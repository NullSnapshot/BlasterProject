using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet.Patterns
{
    internal class CirclePattern : BulletPattern
    {
        public new static string Pattern => "circle";
        public CirclePattern() { }

        public CirclePattern(BulletPatternConfig config, Texture2D sprite)
            : base(config, sprite)
        {
            this.CoolDownCoefficient = 10;
            this.CoolDownMinimum = 10;
        }

        public override void FirePattern(Vector2 source, GameTime gameTime)
        {
            double magnitude = Math.Sqrt(this.config.bullet_speed / 10);
            for (int i = 0; i < this.config.bullet_count; i++)
            {
                Vector2 outboundVector = new Vector2((float)Math.Cos(i), (float)Math.Sin(i));
                this.bulletFactory.Create(outboundVector * new Vector2((float)magnitude, (float)magnitude), source);
            }
        }
    }
}
