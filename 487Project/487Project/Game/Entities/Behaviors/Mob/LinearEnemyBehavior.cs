using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class LinearEnemyBehavior : EnemyBehavior
    {

        public static string Name => "linear";

        protected Vector2 velocity { get; set; }
        public LinearEnemyBehavior()
            : base()
        {
            velocity = Vector2.Zero;
        }
        public LinearEnemyBehavior(EnemyConfig behavior)
        {
            this.TargetPosition = new Vector2(behavior.position.x, behavior.position.y);
            float speed = behavior.enemyMovement.movement_speed;
            switch (behavior.enemyMovement.direction)
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
            this.TargetPosition = EntityTools.DeltaMove(this.TargetPosition, gameTime, x: this.velocity.X, y: this.velocity.Y);
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
