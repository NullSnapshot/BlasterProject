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
    }
}
