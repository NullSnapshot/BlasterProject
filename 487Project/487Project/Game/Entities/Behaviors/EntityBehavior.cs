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
            // Empty constructor for subtype overloading
            this.TargetPosition = new Vector2(0, 0);
            this.TargetSpeed = 0;
        }
        public EntityBehavior(EntityBehavior copySource)
        {
            this.Copy(copySource);
        }

       public Vector2 TargetPosition { get; protected set; }
       public float TargetSpeed { get; protected set; }

        public bool Visible { get; protected set; } = true;

        public abstract void Update(GameTime gameTime);

        public virtual void Copy(EntityBehavior copySource)
        {
            this.TargetPosition = new Vector2(copySource.TargetPosition.X, copySource.TargetPosition.Y);
            this.TargetSpeed = copySource.TargetSpeed;
        }
    }
}
