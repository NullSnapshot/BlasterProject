﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class EnemyFactory
    {
        private string spritePath;
        private EntityBehavior enemyBehaviorTemplate;
        //BulletEntity Entity
        private Vector2 startPos;
        private int health;
        private ContentManager content;
        public EnemyFactory(string spritePath, EntityBehavior enemyBehavior, Vector2 startPos, int health, ContentManager content)
        {
            this.spritePath = spritePath;
            this.enemyBehaviorTemplate = enemyBehavior;
            this.startPos = startPos;
            this.health = health;
            this.content = content;
        }

        public Entity Create()
        {
            Type behaviorType = enemyBehaviorTemplate.GetType();
            EntityBehavior newInstanceBehavior = (EntityBehavior)Activator.CreateInstance(behaviorType);
            newInstanceBehavior.Copy(enemyBehaviorTemplate);
            Entity newEntity = new MobEntity(
                newInstanceBehavior,
                this.content.Load<Texture2D>(this.spritePath),
                this.startPos,
                this.health);
            return newEntity;
        }
    }
}