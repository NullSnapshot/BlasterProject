

using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class PlayerTargetBulletBehavior : BulletBehavior
    {
        private DateTime spawnTime;
        public PlayerTargetBulletBehavior() { }

        private bool LockedOn = false;

        public PlayerTargetBulletBehavior(Vector2 position, BulletPatternConfig bulletConfig) : base(position, bulletConfig)
        {
        }

        public override void Fire()
        {
            this.isActive = true;
            this.spawnTime = DateTime.Now;
        }

        public void SwitchToPlayer()
        {
            this.Velocity = (EntityManager.GetPlayerPos() - this.TargetPosition).NormalizedCopy() * this.TargetSpeed / 10;
            this.LockedOn = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(!this.LockedOn)
            {
                DateTime timeCheck = DateTime.Now;
                if (this.isActive && (timeCheck - this.spawnTime).TotalSeconds > 1.5)
                {
                    this.SwitchToPlayer();
                }
            }
            
        }
    }
}
