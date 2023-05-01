using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MainProgram
{
    class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool isVisible = true;

        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;

        public Enemies(Texture2D newtexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            bulletTexture = newBulletTexture;
            velocity = new Vector2(0, 3);
        }

        public Rectangle BoundingBox // Add this property
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public float shoot = 0;
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {
            position += velocity;
            if (position.X <= 0 || position.X >= 1013 || position.X <= 89 + texture.Width)
            {
                velocity.X = -velocity.X;
            }
            if (position.Y > 1266 - texture.Height || position.Y < 104)
            {
                isVisible = false;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                Vector2 startPosition = new Vector2(position.X + (texture.Height / 2) - (bulletTexture.Height / 2), position.Y + velocity.Y);
                Bullets.ShootBullets(bullets, bulletTexture, startPosition, velocity + new Vector2(0, 6f), 3);
            }
            Bullets.UpdateBullets(bullets);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, position, Color.Red);
        }
    }
}