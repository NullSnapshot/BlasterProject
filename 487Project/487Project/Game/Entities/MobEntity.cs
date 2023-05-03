using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Behaviors;


namespace BulletBlaster.Game.Entities
{
    internal class MobEntity : Entity
    {
        public int health { get; set; }
        public bool alive { get; set; } = true;

        private BulletBehavior bulletTemplate { get; set; }

        public MobEntity(EntityBehavior behavior, Texture2D texture, Vector2 position, int health)
            : base(behavior, texture, position)
        {
            this.health = health;
        }

        public void ShootBullet()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //bulletTemplate.Update(gameTime);
        }
    }
}
