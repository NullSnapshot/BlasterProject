using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class WaveConfig
    {
        public string name { get; set; }
        public int start_time { get; set; }
        public int duration { get; set;}
        public string background { get; set; }
        public List<EnemyConfig> enemies { get; set; }
        public EnemyBulletType enemyBulletType { get; set; }
        
    }
}
