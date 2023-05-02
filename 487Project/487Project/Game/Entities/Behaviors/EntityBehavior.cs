using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    public abstract class EntityBehavior
    {
        public EntityBehavior()
        {
            // TODO
        }

       public Vector2 TargetPosition { get; protected set; }
       public float TargetSpeed { get; protected set; }

        public bool Visible { get; protected set; } = true;

        public abstract void Update(GameTime gameTime);
    }
}
