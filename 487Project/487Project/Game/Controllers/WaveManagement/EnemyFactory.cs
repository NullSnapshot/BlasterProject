using BulletBlaster.Code.Entities;
using BulletBlaster.Game.Entities;
using BulletBlaster.Game.Entities.Behaviors;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal class EnemyFactory
    {
        private string spritePath;
        private EntityBehavior enemyBehaviorTemplate;
        //BulletEntity Entity
        private Vector2 startPos;
        private int health;
        private ContentManager content;

        internal double lastSpawn { get; set; }

        public EnemyFactory(string spritePath, EntityBehavior enemyBehavior, Vector2 startPos, int health, ContentManager content)
        {
            this.spritePath = spritePath;
            enemyBehaviorTemplate = enemyBehavior;
            this.startPos = startPos;
            this.health = health;
            this.content = content;
        }

        public MobEntity Create()
        {
            Type behaviorType = enemyBehaviorTemplate.GetType();
            EntityBehavior newInstanceBehavior = (EntityBehavior)Activator.CreateInstance(behaviorType);
            newInstanceBehavior.Copy(enemyBehaviorTemplate);
            MobEntity newEntity = new MobEntity(
                newInstanceBehavior,
                new BulletBehavior(),
                content.Load<Texture2D>(spritePath),
                startPos,
                health);
            return newEntity;
        }
    }
}
