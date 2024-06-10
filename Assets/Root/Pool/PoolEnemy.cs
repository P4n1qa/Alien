using UnityEngine;

namespace Root.Pool
{
    public class PoolEnemy : PoolUse
    {
        [SerializeField] private PoolObjectBase _poolObjectBase;
        
        private PoolMono<PoolObjectBase> _pool;
        
        protected override void CreatePool()
        {
            _pool = new PoolMono<PoolObjectBase>(_poolObjectBase, PoolCount, transform)
            {
                AutoExpand = AutoExpand
            };
        }

        public override PoolObjectBase CreateObject(Vector3 spawnPosition)
        {
            var poolObjectBase = _pool.GetFreeElement();
            poolObjectBase.InitializeSystems(spawnPosition);
            return poolObjectBase;
        }
    }
}