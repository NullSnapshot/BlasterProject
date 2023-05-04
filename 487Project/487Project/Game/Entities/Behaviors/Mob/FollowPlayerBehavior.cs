using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletBlaster.Game.Entities.Behaviors.Mob
{
    internal class FollowPlayerBehavior : EnemyBehavior
    {
        private int targetY = 0;

        public static string Name => "follow-player";
        public FollowPlayerBehavior() { }
        public FollowPlayerBehavior(EnemyConfig config) : base(config) 
        {
            targetY = config.position.y;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 playerPos = EntityManager.GetPlayerPos();
            playerPos.Y = targetY;
            Vector2 currentPos = this.TargetPosition;

            Vector2 wantedPositionVec = (playerPos - currentPos).NormalizedCopy() * new Vector2((float)Math.Sqrt(this.TargetSpeed),0);
            this.TargetPosition = EntityTools.DeltaMove(this.TargetPosition, gameTime, x: wantedPositionVec.X, y: wantedPositionVec.Y);
        }
    }
}
