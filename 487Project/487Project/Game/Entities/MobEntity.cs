using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class MobEntity : Entity
    {
        public int health { get; set; }
        public bool alive { get; set; }

        private EntityBehavior behavior { get; set; }
        
        public MobEntity(EntityBehavior behavior) 
        {
            this.behavior = behavior;
        }

    }
}
