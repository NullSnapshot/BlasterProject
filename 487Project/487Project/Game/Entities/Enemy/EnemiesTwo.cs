using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace BulletBlaster.Game.Entities.Enemy
{
    class EnemiesTwo
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool isVisible = true;

        List<Bullets> bullets = new List<Bullets>();
        public Texture2D bulletTexture;

        public EnemiesTwo(Texture2D newtexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            texture = newtexture;
            position = newPosition;
            bulletTexture = newBulletTexture;
            velocity = new Vector2(2, 0);
        }

        public Rectangle BoundingBox // Add this property
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public float shoot = 0;
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {
            position += velocity;
            if (position.Y <= 1266 - texture.Height || position.Y >= 104)
            {
                velocity.Y = -velocity.Y;
            }
            if (position.X > 1013 - texture.Width || position.X < 89)
            {
                isVisible = false;
            }
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                Vector2 startPosition = new Vector2(position.X + velocity.X, position.Y + texture.Height / 2 - bulletTexture.Height / 2);
                Bullets.ShootBullets(bullets, bulletTexture, startPosition, velocity + new Vector2(0, 7f), 6);
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