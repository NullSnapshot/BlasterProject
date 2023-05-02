using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using System.Net;
using System.Text.Json.Serialization.Metadata;

namespace MainProgram
{
    internal class UserEntity : MobEntity
    {
        UserMovement usersMovements;

        public bool isVisible = true;

        public int score { get; set; }
        public Rectangle boundingBox { get; set; }

        public int immunityLength = 0;

        public bool cheatMode = false;

        private List<Rectangle> hitList = new List<Rectangle>();


        public UserEntity(Texture2D ballTexture, UserControlledBehavior behavior, int health)
            : base(behavior, ballTexture, behavior.UsersMovements.getLocation(), health)
        {
            this.usersMovements = behavior.UsersMovements;
            this.usersMovements.setTexture(this.Texture);
            this.Speed = behavior.TargetSpeed;
            this.health = health;
            this.score = 0;
            this.boundingBox = new Rectangle((int)this.Position.X - ballTexture.Width / 2, (int)this.Position.Y - ballTexture.Height / 2, ballTexture.Width, ballTexture.Height);
        }

        public override void Draw(SpriteBatch sb)
        {
            if(immunityLength == 0)
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
            else
            {
                sb.Draw(
                this.Texture,
                this.Position,
                null,
                Color.Gray,
                0f,
                new Vector2(this.Texture.Width / 2, this.Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(immunityLength != 0)
            {
                immunityLength--;
            }
            this.score += 1;
            this.Position = this.Behavior.TargetPosition;
            this.Texture = this.usersMovements.getTexture();
            this.Speed = this.Behavior.TargetSpeed;
            this.boundingBox = new Rectangle((int)this.Position.X - Texture.Width / 2, (int)this.Position.Y - Texture.Height / 2, Texture.Width, Texture.Height);
        }

        public bool CheckCollision(Rectangle otherBoundingBox)
        {
            if(immunityLength == 0 || cheatMode == true)
            {
                if (hitList.Contains(otherBoundingBox))
                {
                    return false;
                }
                else
                {
                    if (this.boundingBox.Intersects(otherBoundingBox))
                    {
                        hitList.Add(otherBoundingBox);
                        return true;
                    }

                    return false;
                }
            }

            return false;

        }

        public void TakeDamage(int amount)
        {
            if(cheatMode != true)
            {
                this.health -= amount;
                System.Diagnostics.Debug.WriteLine("New Health" + this.health.ToString());
                if (this.health <= 0)
                {
                    this.alive = false;
                    Dead();
                }
                else
                {
                    immunityLength = 200;
                }
            }
        }

        private void Dead()
        {
            this.isVisible= false;
        }
    }
}
