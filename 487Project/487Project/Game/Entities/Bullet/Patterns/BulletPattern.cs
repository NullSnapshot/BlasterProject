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

        public BulletPatternConfig config;

        public Texture2D BulletSprite;

        protected double lastFire = 0;

        protected Random random;

        protected int CoolDownCoefficient = 4;

        protected float CoolDown = 0.5f;

        public static string Pattern => "single";

        public BulletPattern() { }
        
        public BulletPattern(BulletPatternConfig config, Texture2D sprite) 
        {
            this.config = config;
            this.BulletSprite = sprite;
            if (config.bullet_type == "player-bullet")
                this.bulletFactory = new PlayerBulletFactory(config, sprite);
            else
                this.bulletFactory = new EnemyBulletFactory(config, sprite);

            this.random = new Random(EntityTools.GetPseudoRandomSeed());
        }

        public virtual void FirePattern(Vector2 source, GameTime gameTime)
        {
            this.bulletFactory.Create(new Vector2(0, this.config.bullet_speed / 10), source);
        }

        public virtual bool ShouldFire(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastFire > this.CoolDown)
            {
                this.lastFire = gameTime.TotalGameTime.TotalSeconds;
                return true;
            }
            return false;
        }

        public virtual void Update(GameTime gameTime, Vector2 Position)
        {
            // Does nothing usually
        }
    }
}
