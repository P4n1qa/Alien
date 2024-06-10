using System;
using System.Collections;
using System.Collections.Generic;
using Root.Pool;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Root.Enemy.Scripts.Spawners
{
    public class EnemySpawner :MonoBehaviour
    {
        [SerializeField] private PoolEnemy _poolEnemy;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private LayerMask _navMeshLayer;

        [SerializeField] private int _radius;
        [SerializeField] private int _enemyCount;
        [SerializeField] private int _spawnInterval;
        
        private Dictionary<PoolObjectBase, Action> _eventHandlers = new();

        private int _currentEnemyCount;
        private int _maxEnemyCount;

        private void Start()
        {
            _maxEnemyCount = _enemyCount;
            SpawnEnemies();
            StartCoroutine(SpawnEnemiesWithDelay());
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            while (true)
            {
                while (_currentEnemyCount < _maxEnemyCount)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(_spawnInterval);
                }
                yield return null;
            }
        }

        private void SpawnEnemy()
        {
            var enemy = _poolEnemy.CreateObject(GetRandomPointOnNavMesh());
            _currentEnemyCount++;
            Subscribe(enemy);
        }
        
        private void SpawnEnemies()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                SpawnEnemy();
            }
        }
        
        private Vector3 GetRandomPointOnNavMesh()
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle * _radius;
            Vector3 randomPoint = _spawnPosition.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

            NavMeshHit hit;
            return NavMesh.SamplePosition(randomPoint, out hit, _radius, _navMeshLayer) ? hit.position : _spawnPosition.position;
        }

        private void CheckEnemyCount(PoolObjectBase enemy)
        {
            _currentEnemyCount--;
            UnSubscribe(enemy);
        }
        
        private void Subscribe(PoolObjectBase enemy)
        {
            Action handler = () => CheckEnemyCount(enemy);
            enemy.OnPoolObjectDied += handler;
            _eventHandlers.Add(enemy, handler);
        }

        private void UnSubscribe(PoolObjectBase enemy)
        {
            if (!_eventHandlers.ContainsKey(enemy)) return;
            enemy.OnPoolObjectDied -= _eventHandlers[enemy];
            _eventHandlers.Remove(enemy);
        }
    }
}