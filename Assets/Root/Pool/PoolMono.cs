using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Root.Pool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public bool AutoExpand { get; set; }
        private T _prefab { get; }
        private Transform _container { get; }

        private List<T> _pool;

        public PoolMono(T prefab, int count , Transform container)
        {
            _prefab = prefab;
            _container = container;
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;
        
            if (AutoExpand)
                return CreateObject(true);
        
            throw new Exception($"No free element in pool {typeof(T)}");

        }
    }
}