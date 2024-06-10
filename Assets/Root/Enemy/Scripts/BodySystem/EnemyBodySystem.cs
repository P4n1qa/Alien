using System;
using Root.Enemy.Scripts.Data;
using UnityEngine;

namespace Root.Enemy.Scripts.BodySystem
{
    public class EnemyBodySystem :MonoBehaviour,IDamageable
    {
        public event Action OnDied;
        public Transform MouthPlayer { get; set; }
        public bool IsDied { get; private set; }

        public AnimationDie AnimationDie;

        private IEnemyCharacterData _enemyCharacterData;
        
        public void Initialize(IEnemyController enemyController)
        {
            _enemyCharacterData = enemyController.CharacterData;
            IsDied = false;
        }
        
        public void ApplyDamage(float amount)
        {
            if (_enemyCharacterData.CurrentCharacteristics.Health > _enemyCharacterData.CurrentCharacteristics.MinHealth)
            {
                _enemyCharacterData.CurrentCharacteristics.Health -= amount;
            }
            else
            {
                if (IsDied)return;
                IsDied = true;
                OnDied?.Invoke();
            }
        }

        private void Start()
        {
            OnDied += StartAnimationDie;
        }

        private void StartAnimationDie()
        {
            AnimationDie.StartAnimationDie(MouthPlayer);
        }

        private void OnDestroy()
        {
            OnDied -= StartAnimationDie;
        }
    }
}