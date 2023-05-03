namespace BulletBlaster.Game.Controllers.WaveManagement
{
    internal interface ISpawnerObserver
    {
        public void UpdateObservers(WaveSpawner originator);
    }
}
