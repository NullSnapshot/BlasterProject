using Microsoft.Xna.Framework;


namespace BulletBlaster.Game.Entities.Behaviors
{
    public abstract class EntityBehavior
    {
        public EntityBehavior()
        {
            // Empty constructor for subtype overloading
            TargetPosition = new Vector2(0, 0);
            TargetSpeed = 0;
        }
        public EntityBehavior(EntityBehavior copySource)
        {
            Copy(copySource);
        }

        public Vector2 TargetPosition { get; protected set; }
        public float TargetSpeed { get; protected set; }

        public bool Visible { get; protected set; } = true;

        public abstract void Update(GameTime gameTime);

        public virtual void Copy(EntityBehavior copySource)
        {
            TargetPosition = new Vector2(copySource.TargetPosition.X, copySource.TargetPosition.Y);
            TargetSpeed = copySource.TargetSpeed;
        }
    }
}
