using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class LinearEnemyBehavior : EntityBehavior
    {
        protected Vector2 velocity { get; set; }
        public LinearEnemyBehavior(string Direction, int speed, Vector2 startPos) 
        { 
            this.TargetPosition = startPos;
            switch(Direction)
            {
                case "left": 
                    this.velocity = new Vector2(-speed, 0); 
                    break;
                case "down":
                    this.velocity = new Vector2(0, speed);
                    break;
                default: // Assume right direction
                    this.velocity = new Vector2(speed, 0);
                    break;

            }
        }

        public override void Update(GameTime gameTime)
        {
            this.TargetPosition += this.velocity;

            if (this.TargetPosition.Y > 1200 || this.TargetPosition.Y < 104) // off screen Y direction
            {
                this.Visible = false;
            }

            // Bullet firing logic goes here.
        }
    }
}
