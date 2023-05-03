using Microsoft.Xna.Framework;


namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class LinearEnemyBehavior : EntityBehavior
    {
        protected Vector2 velocity { get; set; }
        public LinearEnemyBehavior()
            : base()
        {
            velocity = Vector2.Zero;
        }

        public LinearEnemyBehavior(string Direction, int speed, Vector2 startPos)
        {
            TargetPosition = startPos;
            switch (Direction)
            {
                case "left":
                    velocity = new Vector2(-speed, 0);
                    break;
                case "down":
                    velocity = new Vector2(0, speed);
                    break;
                default: // Assume right direction
                    velocity = new Vector2(speed, 0);
                    break;

            }
        }

        public override void Update(GameTime gameTime)
        {
            TargetPosition += velocity;

            if (TargetPosition.Y > 1200 || TargetPosition.Y < 104) // off screen Y direction
            {
                Visible = false;
            }

            // Bullet firing logic goes here.
        }

        public override void Copy(EntityBehavior copySource)

        {
            base.Copy(copySource);
            if (copySource.GetType() == typeof(LinearEnemyBehavior))
            {
                LinearEnemyBehavior linearCopySource = (LinearEnemyBehavior)copySource;
                velocity = new Vector2(linearCopySource.velocity.X, linearCopySource.velocity.Y);
            }
        }
    }
}
