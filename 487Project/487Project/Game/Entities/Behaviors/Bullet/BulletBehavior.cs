using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;


namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class BulletBehavior : EntityBehavior
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        public int Damage { get; protected set; } = 1;

        protected bool isActive = false;

        public BulletBehavior() { }
        public BulletBehavior(Vector2 position, BulletPatternConfig bulletConfig)
        {
            this.TargetPosition = position;
            this.Damage = bulletConfig.damage;
            this.TargetSpeed = bulletConfig.bullet_speed;
        }

        public override void Update(GameTime gameTime)
        {
            if(this.isActive)
            {
                TargetPosition += Velocity;

                if (TargetPosition.Y > 1200 || TargetPosition.Y < 104) // off screen Y direction
                {
                    Visible = false;
                }
            }
        }

        public void Copy(BulletBehavior copySource)
        {
            base.Copy(copySource);
            Velocity = copySource.Velocity;
        }

        // Called to start Bullet behavior
        public virtual void Fire()
        {
            this.isActive = true;
        }
    }
}
