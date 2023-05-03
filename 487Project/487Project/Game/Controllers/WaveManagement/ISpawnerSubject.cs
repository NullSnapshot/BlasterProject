

namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal interface ISpawnerSubject
    {
        public void Attach(ISpawnerObserver observer);
        public void Detach(ISpawnerObserver observer);
    }
}
