using MainProgram;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class SineEnemyBehavior : EntityBehavior
    {
        protected Vector2 Velocity { get; set; }
        private int amplitude;
        private int period;
        public SineEnemyBehavior()
            :base()
        {
            this.amplitude = 0;
            this.period = 0;
            this.Velocity = Vector2.Zero;
        }
        public SineEnemyBehavior(int amplitude, int period)
        {
            this.amplitude = amplitude;
            this.period = period;
        }

        public override void Update(GameTime gameTime)
        {
            // Double check this math, it might be wrong.
            this.Velocity = new Vector2(100 / this.period, (float)Math.Sin(gameTime.TotalGameTime.Seconds) * amplitude);
            this.TargetPosition += this.Velocity;

            // TODO: Turnaround logic for mid boss to switch right to left.
        }

        public override void Copy(EntityBehavior copySource)
        {
            base.Copy(copySource);
            if (copySource.GetType() == typeof(SineEnemyBehavior))
            {
                SineEnemyBehavior sineCopySource = (SineEnemyBehavior)copySource;
                this.amplitude = sineCopySource.amplitude;
                this.period = sineCopySource.period;
                this.Velocity = new Vector2(sineCopySource.Velocity.X, sineCopySource.Velocity.Y);
            }
        }

    }
}
