using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram
{
    public class Entity
    {

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public EntityBehavior Behavior { get; set; }

        public Entity(EntityBehavior behavior, Texture2D texture, Vector2 position)
        {
            this.Behavior = behavior;
            Texture = texture;
            Position = position;
        }

        public virtual void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Draw(
                this.Texture,
                this.Position,
                null,
                Color.White,
                0f,
                new Vector2(this.Texture.Width / 2, this.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Behavior.Update(gameTime);
            this.Position = this.Behavior.TargetPosition;
            this.Speed = this.Behavior.TargetSpeed;
        }

    }
}
