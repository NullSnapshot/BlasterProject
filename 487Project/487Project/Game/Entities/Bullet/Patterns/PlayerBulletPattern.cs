using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet.Patterns
{
    internal class PlayerBulletPattern : BulletPattern
    {
        Vector2 targetVelocity;

        private double lastFireTime = 0;

        public new string Pattern { get; set; } = "player-single";

        public PlayerBulletPattern() { }
        public PlayerBulletPattern(BulletPatternConfig config, Texture2D texture)
            : base(config, texture)
        {
            this.targetVelocity = new Vector2(0, -config.bullet_speed); // shoot straight up
        }

        public override void FirePattern(Vector2 source, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastFireTime > 250)
            {
                this.bulletFactory.Create(targetVelocity, source);
                this.lastFireTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
                
        }
    }
}
