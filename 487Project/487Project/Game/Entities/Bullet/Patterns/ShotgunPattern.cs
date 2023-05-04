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
    internal class ShotgunPattern : BulletPattern
    {
        public new static string Pattern => "shotgun";
        public ShotgunPattern() { }

        public ShotgunPattern(BulletPatternConfig config, Texture2D sprite)
            : base(config, sprite) 
        {
            this.CoolDownCoefficient = 5;
            this.CoolDown = 2;
        }

        public override void FirePattern(Vector2 source, GameTime gameTime)
        {
            for(int i = 0; i < this.config.bullet_count; i++)
            {
                this.bulletFactory.Create(new Vector2((float)(this.random.NextDouble()*1 - 0.5), 
                    Math.Max((float)this.random.NextDouble() * this.config.bullet_speed / 10, this.config.bullet_speed / 10f)), source);
            }
        }
    }
}
