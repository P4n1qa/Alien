using System;
using UnityEngine;

namespace Root.Pool
{
    public abstract class PoolObjectBase : MonoBehaviour
    {
        public event Action OnPoolObjectDied;
        public abstract void InitializeSystems(Vector3 spawnPosition);

        protected void OnOnPoolObjectDied()
        {
            OnPoolObjectDied?.Invoke();
        }
    }
}