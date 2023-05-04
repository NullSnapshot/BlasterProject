using Microsoft.Xna.Framework;


namespace BulletBlaster.Game.Entities.Behaviors
{
    public abstract class EntityBehavior
    {
        public Vector2 TargetPosition { get; protected set; } = Vector2.Zero;
        public float TargetSpeed { get; protected set; } = 0;
        public bool Visible { get; protected set; } = true;


        public EntityBehavior() { }
        public EntityBehavior(EntityBehavior copySource)
        {
            Copy(copySource);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Copy(EntityBehavior copySource)
        {
            TargetPosition = new Vector2(copySource.TargetPosition.X, copySource.TargetPosition.Y);
            TargetSpeed = copySource.TargetSpeed;
        }
    }
}
