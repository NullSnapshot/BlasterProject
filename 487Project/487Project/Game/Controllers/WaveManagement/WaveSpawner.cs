using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
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
            this.enemyGroups = this.config.enemies;

            // build factories
            if (this.enemyGroups == null || this.enemyGroups.Count <= 0)
                return;
            foreach (EnemyConfig conf in this.enemyGroups)
            {
                EnemyFactory newFactory;
                switch (conf.enemyMovement.movement_type)
                {
                    case "linear":
                         newFactory = new EnemyFactory(
                            conf.enemy_sprite,
                            new LinearEnemyBehavior(conf.enemyMovement.direction,
                                conf.enemyMovement.movement_speed, new Vector2(conf.position.x, conf.position.y)), // TODO: Add bullet spec to behavior type
                            new Vector2(conf.position.x, conf.position.y),
                            conf.maxHealth,
                            content);
                        enemyFactories.Add(newFactory);
                        break;
                    case "sine":
                         newFactory = new EnemyFactory(
                            conf.enemy_sprite,
                            new SineEnemyBehavior(conf.enemyMovement.amplitude, conf.enemyMovement.movement_speed), // TODO: see above.
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
            if(gameTime.TotalGameTime.TotalMilliseconds - lastSpawn > offset)
            {
                int i = 0;
                foreach(EnemyFactory factory in enemyFactories)
                {
                    if (this.enemyGroups[i].enemyAmount > 0)
                    {
                        Entity newEnemy = factory.Create();
                        EntityManager.RegisterCollidableEntity(newEnemy);
                    }
                    this.enemyGroups[i].enemyAmount -= 1;
                    i++;
                }
                this.lastSpawn = gameTime.TotalGameTime.TotalMilliseconds;
            }

            // Clear check
            bool done = true;
            foreach(EnemyConfig conf in this.enemyGroups)
            {
                if (conf.enemyAmount > 0)
                    done = false;
            }
            if(done)
            {
                foreach(ISpawnerObserver observer in this.observers)
                {
                    observer.UpdateObservers(this);
                }
            }
        }

        public void Attach(ISpawnerObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Detach(ISpawnerObserver observer)
        {
            this.observers.Remove(observer);
        }
    }
}
