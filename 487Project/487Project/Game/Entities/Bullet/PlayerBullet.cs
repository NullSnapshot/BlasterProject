using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Bullet
{
    internal class PlayerBullet : Bullet
    {

        public PlayerBullet(BulletBehavior bulletBehavior, Texture2D sprite, Vector2 startPos)
            : base(bulletBehavior, sprite, startPos)
        {

        }

        public override void OnCollide(CollidableEntity entity)
        {
            base.OnCollide(entity);
            this.RemovalFlag = true;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            sb.Draw(
                Texture,
                Position,
                null,
                Color.Cyan,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            if (config.Config.DebugMode)
                sb.Draw(Debuger.debugTexture, this.hitBox, Color.Red);
        }
    }
}
