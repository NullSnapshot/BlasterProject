using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletBlaster.Game.Entities.User;


namespace BulletBlaster.Game.UI
{
    internal class SideBar
    {
        SpriteFont testFont;
        UserEntity user;
        int health;
        int score;
        int highScore;

        public SideBar(SpriteFont font, UserEntity user, int highScore)
        {
            testFont = font;
            health = user.health;
            score = user.score;
            this.highScore = highScore;
            this.user = user;
        }

        public void draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(testFont, "High Score: " + highScore.ToString(), new Vector2(1050, 115), Color.Gray);
            _spriteBatch.DrawString(testFont, "Score: " + score.ToString(), new Vector2(1050, 165), Color.White);
            _spriteBatch.DrawString(testFont, "Health: " + health.ToString(), new Vector2(1050, 215), Color.Red);
        }

        public void update()
        {
            score = user.score;
            health = user.health;
        }
    }
}
