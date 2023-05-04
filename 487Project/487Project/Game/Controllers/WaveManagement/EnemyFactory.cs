using BulletBlaster.Code.Entities;
using BulletBlaster.Game.Entities;
using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.Entities.Behaviors.Mob;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal class EnemyFactory
    {
        private string spritePath;
        private EnemyBehavior enemyBehaviorTemplate;
        //BulletEntity Entity
        private Vector2 startPos;
        private int health;
        private ContentManager content;
        private List<BulletPattern> attackPatterns;

        internal double lastSpawn { get; set; }

        public EnemyFactory(string spritePath, EnemyBehavior enemyBehavior, List<BulletPattern> attackPatterns, Vector2 startPos, int health, ContentManager content)
        {
            this.spritePath = spritePath;
            enemyBehaviorTemplate = enemyBehavior;
            this.startPos = startPos;
            this.health = health;
            this.content = content;
            this.attackPatterns = attackPatterns;
        }

        public MobEntity Create()
        {
            Type behaviorType = enemyBehaviorTemplate.GetType();
            EnemyBehavior newInstanceBehavior = (EnemyBehavior)Activator.CreateInstance(behaviorType, enemyBehaviorTemplate.SourceConfig);
            //newInstanceBehavior.Copy(enemyBehaviorTemplate);
            MobEntity newEntity = new MobEntity(
                newInstanceBehavior,
                EntityTools.CopyBulletPatterns(this.attackPatterns),
                content.Load<Texture2D>(spritePath),
                startPos,
                health);
            return newEntity;
        }
    }
}
