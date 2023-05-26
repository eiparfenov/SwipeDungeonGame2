using Utils.LiveData;
using Zenject;

namespace Entities.Stats
{
    public class EntityStats
    {
        private readonly StartEntityStats _startPlayerStats;
        public MutableLiveData<float> Damage { get; } = new();
        public MutableLiveData<float> Speed { get; } = new();
        public MutableLiveData<float> AttackRate { get; } = new();
        public MutableLiveData<float> MaxHealth { get; } = new();
        public MutableLiveData<float> KnockBack { get; } = new();
        public MutableLiveData<float> Health { get; } = new();
        public MutableLiveData<int> Coins { get; } = new();

        [Inject]
        public EntityStats(StartEntityStats startPlayerStats)
        {
            _startPlayerStats = startPlayerStats;
            Reset(true);
        }

        public void Reset(bool resetHealth = false)
        {
            Damage.Value = _startPlayerStats.Damage;
            Speed.Value = _startPlayerStats.Speed;
            AttackRate.Value = _startPlayerStats.AttackRate;
            MaxHealth.Value = _startPlayerStats.Health;
            KnockBack.Value = _startPlayerStats.KnockBack;

            if (resetHealth)
            {
                Health.Value = _startPlayerStats.Health;
                Coins.Value = 0;
            }
        }
    }
}