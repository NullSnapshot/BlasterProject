using Microsoft.Xna.Framework;


namespace BulletBlaster.Game.Entities.Behaviors.Bullet
{
    internal class BulletBehavior : EntityBehavior
    {
        protected Vector2 velocity { get; set; }
        public BulletBehavior(Vector2 velocity)
        {
            this.velocity = velocity;
            TargetSpeed = this.velocity.Length();
        }

        public override void Update(GameTime gameTime)
        {
            TargetPosition += velocity;

            if (TargetPosition.Y > 1200 || TargetPosition.Y < 104) // off screen Y direction
            {
                Visible = false;
            }
        }

        public void Copy(BulletBehavior copySource)
        {
            base.Copy(copySource);
            velocity = copySource.velocity;
        }
    }
}
