using System;
using System.Collections.Generic;
using Root.Enemy.Scripts.BodySystem;
using UnityEngine;

namespace Root.Player.Scripts.Player
{
    public class ArmSystem : MonoBehaviour
    {
        [SerializeField] private Transform _mouthPlayer;
        [SerializeField] private PlayerParticleController _playerParticleController;
        
        [SerializeField] private int _countEnemyCanAttack;
        [SerializeField] private float _damage;

        private List<IDamageable> _enemyInRange = new();
        private List<IDamageable> _enemyTargets = new();

        private Dictionary<IDamageable, Action> _eventHandlers = new();

        private void Update()
        {
            FindTargets();
            ApplyDamageToEnemy();
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<IDamageable>();
            if (enemy == null || enemy.IsDied) return;
            if (_enemyInRange.Contains(enemy)) return;
            enemy.MouthPlayer = _mouthPlayer;
            _enemyInRange.Add(enemy);
            Subscribe(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.GetComponent<IDamageable>();
            if (enemy == null) return;
            RemoveEnemyFromTargets(enemy);
            UnSubscribe(enemy);
        }

        private void Subscribe(IDamageable enemy)
        {
            if (_eventHandlers.ContainsKey(enemy)) return;
            Action handler = () => RemoveEnemyFromTargets(enemy);
            enemy.OnDied += handler;
            enemy.OnDied += _playerParticleController.PlayParticleSystems;
            _eventHandlers.Add(enemy, handler);
        }

        private void UnSubscribe(IDamageable enemy)
        {
            if (!_eventHandlers.ContainsKey(enemy)) return;
            enemy.OnDied -= _eventHandlers[enemy];
            enemy.OnDied -= _playerParticleController.PlayParticleSystems;
            _eventHandlers.Remove(enemy);
        }

        private void FindTargets()
        {
            if (_enemyInRange.Count <= 0 || _enemyTargets.Count >= _countEnemyCanAttack) return;
            var countEnemyNeed = Mathf.Min(_countEnemyCanAttack - _enemyTargets.Count, _enemyInRange.Count);
            for (int i = 0; i < countEnemyNeed; i++)
            {
                _enemyTargets.Add(_enemyInRange[i]);
                _enemyInRange.RemoveAt(i);
            }
        }

        private void RemoveEnemyFromTargets(IDamageable enemy)
        {
            if (_enemyTargets.Remove(enemy) || _enemyInRange.Remove(enemy))
            {
                UnSubscribe(enemy);
            }
        }

        private void ApplyDamageToEnemy()
        {
            float damageThisFrame = _damage * Time.deltaTime;
            
            for (int i = _enemyTargets.Count - 1; i >= 0; i--)
            {
                _enemyTargets[i].ApplyDamage(damageThisFrame);
            }
        }
    }
}
