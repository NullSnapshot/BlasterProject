using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using BulletBlaster.Game.Controllers;
using BulletBlaster.Game.Entities.User;

namespace BulletBlaster.Game.Entities
{
    internal class MobEntity : CollidableEntity
    {
        public int health { get; set; }
        public int maxHealth { get; set; }
        public bool alive { get; set; } = true;
        private Random random;

        protected List<BulletPattern> attackPatterns{ get; set; }

        protected double lastAttackTrigger = 0;

        public MobEntity(EntityBehavior behavior, List<BulletPattern> attackPatterns, Texture2D texture, Vector2 position, int health)
            : base(behavior, texture, position)
        {
            this.health = health;
            this.maxHealth = health;
            this.attackPatterns = attackPatterns;
            this.random = new Random(behavior.PatternSeed);
        }

        public void Shoot(GameTime gameTime)
        {
            int i = this.random.Next(0, this.attackPatterns.Count);
            if (this.attackPatterns[i].ShouldFire(gameTime))
            {
                this.attackPatterns[i].FirePattern(this.Position, gameTime);
                this.lastAttackTrigger = gameTime.TotalGameTime.Seconds;
            }
                    
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Shoot(gameTime);
            foreach (BulletPattern pattern in attackPatterns)
                pattern.Update(gameTime, this.Position);
        }

        public override void OnCollide(CollidableEntity collidable)
        {
            if(collidable.GetType() == typeof(UserEntity))
                return;
            this.health -= collidable.Damage;
            if(this.health <= 0)
            {
                this.RemovalFlag = true;
                this.OnDeath();
            }
                
            

        }

        public void OnDeath()
        {
            ScoreSystem.addPoints(this.maxHealth * 100);
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
