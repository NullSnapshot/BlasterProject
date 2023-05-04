using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class BobbingEnemyBehavior : EntityBehavior
    {
        protected Vector2 velocity { get; set; }

        public BobbingEnemyBehavior()
            : base()
        {
            velocity = Vector2.Zero;
        }

        public BobbingEnemyBehavior(string Direction, int speed, Vector2 startPos)
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
            float tempX = this.TargetPosition.X;
            float tempY = this.TargetPosition.Y;

            tempX += this.TargetPosition.Y;
            tempY = tempY + (-(float)Math.Cos(this.TargetPosition.X / 100) * 100);

            this.TargetPosition = EntityTools.DeltaMove(this.TargetPosition, gameTime, x: tempX, y: tempY);


            // Bullet firing logic goes here.
        }

        public override void Copy(EntityBehavior copySource)

        {
            base.Copy(copySource);
            if (copySource.GetType() == typeof(LinearEnemyBehavior))
            {
                BobbingEnemyBehavior bobbingCopySource = (BobbingEnemyBehavior)copySource;
                velocity = new Vector2(bobbingCopySource.velocity.X, bobbingCopySource.velocity.Y);
            }
        }
    }
}
