using BulletBlaster.Game.Entities;
using BulletBlaster.Game.Entities.Bullet;
using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BulletBlaster.Game.Controllers
{
    internal static class EntityManager
    {
        private static List<Entity> entities = new List<Entity>();
        private static List<CollidableEntity> PlayerCollidableEntities = new List<CollidableEntity>();
        private static List<CollidableEntity> PlayerBullets = new List<CollidableEntity>();
        private static List<CollidableEntity> Enemies = new List<CollidableEntity>();
        private static UserEntity User { get; set; }

        public static int EntityCount { get; private set; } = 0;

        public static void RegisterUser(UserEntity user)
        {
            entities.Add(user);
            User = user;
        }

        // Register PlayerCollidable
        // Anything that can collide with the player

        //This collision group checks if they are actively colliding with the player
        // Enemy Bullets
        // Powerups
        public static void RegisterPlayerCollidableEntity(CollidableEntity collidable)
        {
            entities.Add(collidable);
            PlayerCollidableEntities.Add(collidable);

        }

        // Register collidable by player bullets
        // Anything that player bullets are allowed to collide with.

        public static void RegisterPlayerBullet(CollidableEntity collidable)
        {
            entities.Add(collidable);
            PlayerBullets.Add(collidable);

        }

        // Register enemies to compare to player bullets for collision
        public static void RegisterEnemy(CollidableEntity collidable)
        {
            entities.Add(collidable);
            Enemies.Add(collidable);
            PlayerCollidableEntities.Add(collidable);
        }

        public static void Update(GameTime gameTime)
        {
            // Update positional/firing logic
            for(int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
            HandleCollisions();
            HandleRemovals();
            EntityCount = entities.Count;
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Entity entity in entities)
            {
                entity.Draw(spriteBatch, gameTime);
            }
        }

        public static void Remove(Entity entity)
        {
            entities.Remove(entity);
            // PlayerCollidableEntities
            //  Powerups, Enemy Bullets, and Enemies
            if(entity.GetType() == typeof(MobEntity))
            {
                PlayerCollidableEntities.Remove((MobEntity)entity);
                Enemies.Remove((MobEntity)entity);
            }

            // Enemy Bullets
            if (entity.GetType() == typeof(EnemyBullet)) 
            {
                PlayerCollidableEntities.Remove((CollidableEntity)entity);
            }
            // Player Bullets
            if (entity.GetType() == typeof(PlayerBullet))
            {
                PlayerBullets.Remove((CollidableEntity)entity);
            }
            // Powerups
            if (false)
            {
                PlayerCollidableEntities.Remove((CollidableEntity)entity);
            }
        }

        private static void HandleCollisions()
        {
            // Group 1: Entities that can collide with the player
            for(int i = 0; i < PlayerCollidableEntities.Count; i++)
            {
                if(PlayerCollidableEntities[i].hitBox.Intersects(User.hitBox) && User.cheatMode == false)
                {
                    PlayerCollidableEntities[i].OnCollide(User);
                    User.OnCollide(PlayerCollidableEntities[i]);
                }
            }
            // Group 2: Player bullets colliding with enemies
            for(int i = 0; i < PlayerBullets.Count; i++)
            {
                for(int j = 0; j < Enemies.Count; j++)
                {
                    if (PlayerBullets[i].hitBox.Intersects(Enemies[j].hitBox))
                    {
                        PlayerBullets[i].OnCollide(Enemies[j]);
                        Enemies[j].OnCollide(PlayerBullets[i]);
                        break; // Don't want to hit more than one!
                    }
                }
            }
        }
       
        private static void HandleRemovals()
        {
            for(int i = 0; i < entities.Count; i++)
            {
                if (entities[i].RemovalFlag)
                    Remove(entities[i]);
            }
        }


        public static Vector2 GetPlayerPos()
        {
            return User.Position;
        }

        public static UserEntity GetPlayer()
        {
            return User;
        }
    }
}
