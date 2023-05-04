using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.Bullet.Patterns;

namespace BulletBlaster.Game.Entities
{
    internal class MobEntity : CollidableEntity
    {
        public int health { get; set; }
        public bool alive { get; set; } = true;
        private Random random;

        protected List<BulletPattern> attackPatterns{ get; set; }

        public MobEntity(EntityBehavior behavior, List<BulletPattern> attackPatterns, Texture2D texture, Vector2 position, int health)
            : base(behavior, texture, position)
        {
            this.health = health;
            this.attackPatterns = attackPatterns;
            this.random = new Random(behavior.PatternSeed);
        }

        public void Shoot(GameTime gameTime)
        {
            int i = this.random.Next(0, this.attackPatterns.Count);
            this.attackPatterns[i].FirePattern(this.Position, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollide(CollidableEntity collidable)
        {
            this.health -= collidable.Damage;
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
