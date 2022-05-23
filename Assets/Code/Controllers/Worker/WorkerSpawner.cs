using Controllers.BaseUnit;
using UnityEngine;

namespace Controllers.Worker
{
    public sealed class WorkerSpawner: BaseUnitSpawner
    {
        private readonly GameConfig _gameConfig;
        private readonly GameObject _unitPrefab;

        public WorkerSpawner(GameConfig gameConfig, GameObject unitPrefab) : base(gameConfig, unitPrefab)
        {
            _gameConfig = gameConfig;
            _unitPrefab = unitPrefab;
        }

        public override GameObject Spawn()
        {
            return BaseUnitFactory.CreateUnit(_unitPrefab,WhereToSpawn);
        }
    }
}