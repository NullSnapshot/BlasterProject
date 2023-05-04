using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using System;


namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class SineEnemyBehavior : EnemyBehavior
    {
        protected Vector2 Velocity { get; set; }
        private int amplitude;
        private int period;
        public SineEnemyBehavior()
            : base()
        {
            amplitude = 0;
            period = 0;
            Velocity = Vector2.Zero;
        }

        public SineEnemyBehavior(EnemyConfig behavior)
            : base(behavior)
        {
            this.amplitude = behavior.enemyMovement.amplitude;
            this.period = behavior.enemyMovement.period;
        }

        public override void Update(GameTime gameTime)
        {
            // Double check this math, it might be wrong.
            //Velocity = new Vector2(100 / period, (float)Math.Sin(gameTime.TotalGameTime.Seconds) * amplitude);
            Velocity = Vector2.Zero;
            TargetPosition += Velocity;

            // TODO: Turnaround logic for mid boss to switch right to left.
        }

        public override void Copy(EntityBehavior copySource)
        {
            base.Copy(copySource);
            if (copySource.GetType() == typeof(SineEnemyBehavior))
            {
                SineEnemyBehavior sineCopySource = (SineEnemyBehavior)copySource;
                amplitude = sineCopySource.amplitude;
                period = sineCopySource.period;
                Velocity = new Vector2(sineCopySource.Velocity.X, sineCopySource.Velocity.Y);
            }
        }

    }
}
