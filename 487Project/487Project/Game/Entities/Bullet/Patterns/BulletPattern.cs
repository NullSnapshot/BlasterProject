using BulletBlaster.Game.config;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Bullet.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet.Patterns
{
    internal class BulletPattern
    {
        protected int bullet_count = 1;

        protected BulletFactory bulletFactory;

        protected BulletPatternConfig config;

        public string Pattern { get; set; } = "single";

        public BulletPattern() { }
        
        public BulletPattern(BulletPatternConfig config, Texture2D sprite) 
        {
            this.config = config;
            if (config.bullet_type == "player-bullet")
                this.bulletFactory = new PlayerBulletFactory(config, sprite);
            else
                this.bulletFactory = new EnemyBulletFactory(config, sprite);
        }

        public virtual void FirePattern(Vector2 source, GameTime gameTime)
        {
            this.bulletFactory.Create(new Vector2(0, this.config.bullet_speed), source);
        }
    }
}
