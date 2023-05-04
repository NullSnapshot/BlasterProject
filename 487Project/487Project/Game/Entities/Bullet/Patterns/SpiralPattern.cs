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
    internal class SpiralPattern : BulletPattern
    {
        public new static string Pattern => "spiral";
        public SpiralPattern() { }

        private double LastFire = 0;
        private double bulletsFired = 0;
        private bool firing = false;
        

        public SpiralPattern(BulletPatternConfig config, Texture2D sprite)
            : base(config, sprite)
        {
            this.CoolDownCoefficient = 5;
            this.CoolDown = 15;
        }

        public override void FirePattern(Vector2 source, GameTime gameTime)
        {
            this.firing = true;
           
        }

        public override void Update(GameTime gameTime, Vector2 Position)
        {
            base.Update(gameTime, Position);
            if (bulletsFired < this.config.bullet_count && this.firing)
            {
                double magnitude = Math.Sqrt(this.config.bullet_speed / 10);
                if (gameTime.TotalGameTime.TotalMilliseconds - this.LastFire > 10)
                {
                    Vector2 outboundVector = new Vector2((float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 5 % (Math.PI * 2)), (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 5 % (Math.PI * 2)));
                    this.bulletFactory.Create(outboundVector * new Vector2((float)magnitude, (float)magnitude), Position);
                    this.bulletFactory.Create(-outboundVector * new Vector2((float)magnitude, (float)magnitude), Position);
                    this.LastFire = gameTime.TotalGameTime.TotalMilliseconds;
                    bulletsFired++;
                }
            }
            else
            {
                this.firing = false;
                this.bulletsFired = 0;
            }
                
        }
    }
}
