using MainProgram;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class BulletBehavior : EntityBehavior
    {
        protected Vector2 velocity { get; set; }
        public BulletBehavior(Vector2 velocity)
        {
            this.velocity = velocity;
            this.TargetSpeed = this.velocity.Length();
        }

        public override void Update(GameTime gameTime)
        {
            this.TargetPosition += this.velocity;

            if(this.TargetPosition.Y > 1200 || this.TargetPosition.Y < 104) // off screen Y direction
            {
                this.Visible = false;
            }
        }

        public void Copy(BulletBehavior copySource)
        {
            base.Copy(copySource);
            this.velocity = copySource.velocity;
        }
    }
}
