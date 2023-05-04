using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Behaviors;


namespace BulletBlaster.Game.Entities
{
    internal class MobEntity : CollidableEntity
    {
        public int health { get; set; }
        public bool alive { get; set; } = true;

        protected BulletBehavior bulletTemplate { get; set; }

        public MobEntity(EntityBehavior behavior, BulletBehavior bulletBehavior, Texture2D texture, Vector2 position, int health)
            : base(behavior, texture, position)
        {
            this.health = health;
            this.bulletTemplate = bulletBehavior;
        }

        public void ShootBullet()
        {
            // We'll get there!
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bulletTemplate.Update(gameTime);
        }

        public override void OnCollide()
        {
            this.health -= 1;
            if(this.health <= 0)
                this.RemovalFlag = true;

        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            if(this.RemovalFlag != true)
            {
                base.Draw(sb, gameTime);
            }
        }
    }
}
