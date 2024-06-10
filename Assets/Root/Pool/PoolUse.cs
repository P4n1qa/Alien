using UnityEngine;

namespace Root.Pool
{
    public abstract class PoolUse : MonoBehaviour
    {
        public int PoolCount;
        public bool AutoExpand;

        private void Awake()
        {
            CreatePool();
        }

        protected abstract void CreatePool();

        public abstract PoolObjectBase CreateObject(Vector3 spawnPosition);
    }
}