using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MainProgram
{
    class FinalBoss
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        float rotation = 0f;
        bool right;
        bool up;
        float distanceX;
        float distanceY;
        float oldDistanceX;
        float oldDistanceY;
        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;
        public int health = 10;
        public bool isVisible = true;

        public FinalBoss(Texture2D newtexture, Vector2 newPosition, float newDistanceX, float newDistanceY, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            distanceX = newDistanceX;
            distanceY = newDistanceY;
            oldDistanceX = distanceX;
            oldDistanceY = distanceY;
            bulletTexture = newBulletTexture;
        }

        public Rectangle BoundingBox // Add this property
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public float shoot = 0;

        public void Update(GameTime gameTime)
        {
            position += velocity;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            if (distanceX <= 0)
            {
                right = true;
                up = true;
                velocity.X = 4f;
                velocity.Y = 4f;
            }
            else if (distanceX >= oldDistanceX)
            {
                right = false;
                up = false;
                velocity.X = -4f;
                velocity.Y = -4f;
            }
            if (right)
            {
                distanceX += 1;
                distanceY += 1;
            }
            else
            {
                distanceX -= 1;
                distanceY -= 1;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                Vector2 startPosition = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height / 2), position.Y + velocity.Y);
                Bullets.ShootMultipleBullets(bullets, bulletTexture, startPosition, new Vector2[] { velocity + new Vector2(0, 3f), velocity + new Vector2(1f, 6f), velocity + new Vector2(-0.5f, 4f), velocity + new Vector2(-0.5f, 2f) }, 21);
            }
            Bullets.UpdateBullets(bullets);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            if (velocity.X > 0)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isVisible = false;
            }
        }
    }
}