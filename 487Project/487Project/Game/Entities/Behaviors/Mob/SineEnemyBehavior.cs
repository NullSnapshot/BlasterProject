using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;


namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class SineEnemyBehavior : EnemyBehavior
    {
        protected Vector2 Velocity { get; set; }
        private int amplitude = 50;
        private int period = 2;

        private bool movingLeft = false;

        public static string Name => "sine";

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
            float tempY = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * period) * amplitude * 2 + this.SourceConfig.position.y;
            float tempX = 1010;
            
            if(movingLeft)
            {
                tempX = 0;
            }

            if(this.TargetPosition.X > 1000 && this.TargetPosition.X < 1010)
            {
                movingLeft = true;
            }
            if(this.TargetPosition.X < 10 && this.TargetPosition.X > 5 )
            {
                movingLeft = false;
            }

            Vector2 targetVector = (new Vector2(tempX, tempY) - this.TargetPosition).NormalizedCopy();

            this.TargetPosition = EntityTools.DeltaMove(this.TargetPosition, gameTime, targetVector.X, targetVector.Y * amplitude);

        }

    }
}
