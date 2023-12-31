﻿using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities
{
    internal class CollidableEntity : Entity
    {
        public Rectangle hitBox { get; protected set; }

        public int Damage { get; set; } = 1;

        public CollidableEntity(EntityBehavior behavior, Texture2D texture, Vector2 position)
            : base(behavior, texture, position)
        {
            this.hitBox = EntityTools.BuildCollisionBox(this);
        }

        // ON COLLIDE
        public virtual void OnCollide(CollidableEntity entity)
        {
            if(entity.GetType() == typeof(UserEntity))
            {
                UserEntity user = (UserEntity)entity;
                if (user.immunityLength > 0)
                    return;
            }
            this.RemovalFlag = true;
        }

        //Death Logic
        //public abstract void OnDeath();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.hitBox = EntityTools.BuildCollisionBox(this);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            base.Draw(sb, gameTime);
            if(config.Config.DebugMode)
                sb.Draw(Debuger.debugTexture, this.hitBox, Color.Red);
        }
    }
}
