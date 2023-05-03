using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BulletBlaster.Game.Entities.Behaviors;


namespace BulletBlaster.Game.Entities
{
    public class Entity
    {

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public EntityBehavior Behavior { get; set; }

        public Entity(EntityBehavior behavior, Texture2D texture, Vector2 position)
        {
            Behavior = behavior;
            Texture = texture;
            Position = position;
        }

        public virtual void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public virtual void Update(GameTime gameTime)
        {
            Behavior.Update(gameTime);
            Position = Behavior.TargetPosition;
            Speed = Behavior.TargetSpeed;
        }

    }
}
