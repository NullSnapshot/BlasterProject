using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using BulletBlaster.Game.config;
using BulletBlaster.Game.Entities;
using BulletBlaster.Game.Entities.Behaviors.Mob;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using Microsoft.Xna.Framework.Graphics;

namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal class WaveSpawner : ISpawnerSubject
    {
        WaveConfig config;
        List<EnemyConfig> enemyGroups;
        List<EnemyFactory> enemyFactories = new List<EnemyFactory>();
        List<ISpawnerObserver> observers = new List<ISpawnerObserver>();
        int offset = 1000; // TODO: change to use value from JSON file
        double lastSpawn = 0; // time since last enemy spawn

        public WaveSpawner(WaveConfig config, ContentManager content)
        {
            this.config = config;
            enemyGroups = this.config.enemies;

            // build factories
            if (enemyGroups == null || enemyGroups.Count <= 0)
                return;
            foreach (EnemyConfig conf in enemyGroups)
            {
                List<BulletPattern> attackPatterns = new List<BulletPattern>();
                foreach (BulletPatternConfig pattern in conf.attackPatterns)
                {
                    attackPatterns.Add(EntityTools.BuildBulletPattern(pattern, content.Load<Texture2D>(pattern.bullet_sprite)));
                }
                EnemyFactory newFactory;
                switch (conf.enemyMovement.movement_type)
                {
                    case "linear":
                        newFactory = new EnemyFactory(
                           conf.enemy_sprite,
                           new LinearEnemyBehavior(conf.enemyMovement.direction,
                               conf.enemyMovement.movement_speed, new Vector2(conf.position.x, conf.position.y)),
                           attackPatterns,
                           new Vector2(conf.position.x, conf.position.y),
                           conf.maxHealth,
                           content);
                        enemyFactories.Add(newFactory);
                        break;

                    case "sine":
                        newFactory = new EnemyFactory(
                           conf.enemy_sprite,
                           new SineEnemyBehavior(conf.enemyMovement.amplitude, conf.enemyMovement.movement_speed),
                           attackPatterns,
                           new Vector2(conf.position.x, conf.position.y),
                           conf.maxHealth,
                           content);
                        enemyFactories.Add(newFactory);
                        break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            // time to spawn new enemies.
            if (gameTime.TotalGameTime.TotalMilliseconds - lastSpawn > offset)
            {
                for(int i = 0; i < enemyFactories.Count; i++)
                {
                    EnemyFactory factory = enemyFactories[i];
                    if (enemyGroups[i].enemyAmount > 0)
                    {
                        if(gameTime.TotalGameTime.TotalMilliseconds - factory.lastSpawn > enemyGroups[i].offset)
                        {
                            MobEntity newEnemy = factory.Create();
                            EntityManager.RegisterEnemy(newEnemy);
                            factory.lastSpawn = gameTime.TotalGameTime.TotalMilliseconds;
                            enemyGroups[i].enemyAmount -= 1;
                        }
                        
                    }
                }
            }

            if (enemyGroups != null && enemyGroups.Count > 0)
            {
                // Clear check
                bool done = true;
                foreach (EnemyConfig conf in enemyGroups)
                {
                    if (conf.enemyAmount > 0)
                        done = false;
                }
                if (done)
                {
                    foreach (ISpawnerObserver observer in observers)
                    {
                        observer.UpdateObservers(this);
                    }
                }
            }

        }

        public void Attach(ISpawnerObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(ISpawnerObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
