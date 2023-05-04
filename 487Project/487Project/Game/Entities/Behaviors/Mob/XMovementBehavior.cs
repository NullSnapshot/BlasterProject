using BulletBlaster.Game.config;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class XMovementBehavior : EnemyBehavior
    {
        private double LastMove = 0;
        private double MoveDelay = 4;

        Vector2 InitialPosition = new Vector2(500, 500);
        Vector2 selectedTarget = new Vector2(500, 500);
        private int amplitude = 20;
        Random random = new Random(EntityTools.GetPseudoRandomSeed());


        public static string Name => "x-pattern";
        public XMovementBehavior() { }

        
        
        public XMovementBehavior(EnemyConfig config) : base(config) 
        {
            this.MoveDelay = config.enemyMovement.cooldown;
            this.InitialPosition = new Vector2(config.position.x, config.position.y);
            this.amplitude = config.enemyMovement.amplitude;

        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds - this.LastMove > this.MoveDelay)
            {
                int[] dirs = new int[2] { -1, 1 };
                // Pick new target
                int xDir, yDir;
                xDir = dirs[random.Next(0, 2)];
                yDir = dirs[random.Next(0, 2)];

                this.selectedTarget = new Vector2(xDir * amplitude + this.InitialPosition.X, yDir * amplitude + this.InitialPosition.Y);
                this.LastMove = gameTime.TotalGameTime.TotalSeconds;
                
            }
            Vector2 movementVector = (this.selectedTarget - this.TargetPosition).NormalizedCopy();
            this.TargetPosition = EntityTools.DeltaMove(this.TargetPosition, gameTime, x: movementVector.X * (float)Math.Sqrt(this.TargetSpeed),
                y: movementVector.Y * (float)Math.Sqrt(this.TargetSpeed));
        }
    }
}
