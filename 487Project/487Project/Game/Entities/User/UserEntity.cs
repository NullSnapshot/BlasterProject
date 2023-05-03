using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BulletBlaster.Code.Entities.Behaviors.Mob;


namespace BulletBlaster.Game.Entities.User
{
    internal class UserEntity : MobEntity
    {
        UserMovement usersMovements;

        public bool isVisible = true;

        public int score { get; set; }
        public Rectangle boundingBox { get; set; }

        public int immunityLength = 0;

        private List<Rectangle> hitList = new List<Rectangle>();

        // Cheat related vars
        public bool cheatMode = false;

        public double lastColorChange = 0;
        private Color cheatColor = Color.Red;
        private Color[] cheatColorPalate = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        private int nextColor = 1;



        public UserEntity(Texture2D ballTexture, UserControlledBehavior behavior, int health)
            : base(behavior, ballTexture, behavior.UsersMovements.getLocation(), health)
        {
            usersMovements = behavior.UsersMovements;
            usersMovements.setTexture(Texture);
            Speed = behavior.TargetSpeed;
            this.health = health;
            score = 0;
            boundingBox = new Rectangle((int)Position.X - ballTexture.Width / 2, (int)Position.Y - ballTexture.Height / 2, ballTexture.Width, ballTexture.Height);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            if (cheatMode && gameTime != null)
            {
                // Don't want to change colors more than twice a second
                if (gameTime.TotalGameTime.TotalMilliseconds - lastColorChange > 50)
                {
                    cheatColor = cheatColorPalate[nextColor];
                    nextColor = (nextColor + 1) % cheatColorPalate.Length;
                    lastColorChange = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            if (immunityLength == 0)
            {
                sb.Draw(
                Texture,
                Position,
                null,
                cheatMode ? cheatColor : Color.White,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
            else
            {
                sb.Draw(
                Texture,
                Position,
                null,
                Color.Gray,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (immunityLength != 0)
            {
                immunityLength--;
            }
            score += 1;
            Position = Behavior.TargetPosition;
            Texture = usersMovements.getTexture();
            Speed = Behavior.TargetSpeed;
            boundingBox = new Rectangle((int)Position.X - Texture.Width / 2, (int)Position.Y - Texture.Height / 2, Texture.Width, Texture.Height);
        }


        // UNTESTED CODE
        public bool CheckCollision(Rectangle otherBoundingBox)
        {
            if (immunityLength == 0 || cheatMode == true)
            {
                if (hitList.Contains(otherBoundingBox))
                {
                    return false;
                }
                else
                {
                    if (boundingBox.Intersects(otherBoundingBox))
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
            if (cheatMode != true)
            {
                health -= amount;
                System.Diagnostics.Debug.WriteLine("New Health" + health.ToString());
                if (health <= 0)
                {
                    alive = false;
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
            isVisible = false;
        }
    }
}
