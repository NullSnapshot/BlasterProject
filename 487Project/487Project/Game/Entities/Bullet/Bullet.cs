using BulletBlaster.Game.Entities.Behaviors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet
{
    internal class Bullet : CollidableEntity
    {
        public Bullet(EntityBehavior bulletBehavior, Texture2D sprite, Vector2 startPos)
            :base(bulletBehavior, sprite, startPos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(this.Position.X > 1100 || this.Position.X < 50 || this.Position.Y > 1300 || this.Position.Y < 50)
            {
                this.RemovalFlag = true;
            }
        }
    }
}
