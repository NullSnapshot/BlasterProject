using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BulletBlaster.Code.Entities.Behaviors.Mob;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Bullet.Patterns;

namespace BulletBlaster.Game.Entities.User
{
    internal class UserEntity : MobEntity
    {

        private Texture2D sprite;
        private Texture2D hitboxSprite;
        public bool drawHitbox { get; set; } = false;

        public int immunityLength = 0;

        // Cheat related vars
        public bool cheatMode = false;
        public double lastColorChange = 0;
        private Color cheatColor = Color.Red;
        private Color[] cheatColorPalate = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        private int nextColor = 1;



        public UserEntity(Texture2D spriteTexture, Texture2D hitboxSprite, UserControlledBehavior behavior, List<BulletPattern> bulletPatterns, int health)
            : base(behavior, bulletPatterns, spriteTexture,  behavior.TargetPosition, health)
        {
            this.MaxSpeed = behavior.TargetSpeed;
            this.health = health;

            this.sprite = spriteTexture;
            this.hitboxSprite = hitboxSprite;
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

            // Draw main sprite
            sb.Draw(
                texture: this.sprite,
                position: this.Position,
                sourceRectangle: null, // region which texture is rendered, null draws full texture
                color: this.getTargetColor(),
                rotation:0f,
                origin:new Vector2(this.Texture.Width / 2, this.Texture.Height / 2),
                scale:Vector2.One,
                effects:SpriteEffects.None,
                layerDepth:0f);

            // Draw hitbox
            if(this.drawHitbox) 
            {
                sb.Draw(
                    texture: this.hitboxSprite,
                    position: this.Position,
                    sourceRectangle: null, // region which texture is rendered, null draws full texture
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(this.hitBox.Width / 2, this.hitBox.Height / 2),
                    scale: Vector2.One,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }

            if (config.Config.DebugMode)
                sb.Draw(Debuger.debugTexture, this.hitBox, Color.Blue);
        }

        public override void Update(GameTime gameTime)
        {
            this.Behavior.Update(gameTime);
            this.Position = this.Behavior.TargetPosition;

            if (immunityLength != 0)
            {
                immunityLength--;
            }
            this.Position = this.Behavior.TargetPosition;
            this.hitBox = new Rectangle((int)this.Position.X - this.hitboxSprite.Width / 2, 
                (int)this.Position.Y + 2 - this.hitboxSprite.Height / 2, this.hitboxSprite.Width, this.hitboxSprite.Height);

        }

        private Color getTargetColor()
        {
            if(this.cheatMode) 
            {
                return this.cheatColor;
            }
            if(this.immunityLength != 0)
            {
                return Color.Gray;
            }
            return Color.White;
        }

        public override void OnCollide(CollidableEntity entity)
        {
            if(immunityLength == 0 && !this.cheatMode)
            {
                immunityLength = 500;
                health -= entity.Damage;
                if(this.health == 0)
                {
                    this.health = -1;
                }
            }
        }
    }
}
