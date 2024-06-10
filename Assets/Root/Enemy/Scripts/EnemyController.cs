using Root.Enemy.Scripts.BodySystem;
using Root.Enemy.Scripts.Data;
using Root.Enemy.Scripts.MovementSystem;
using Root.Enemy.Scripts.UI;
using Root.Pool;
using UnityEngine;

namespace Root.Enemy.Scripts
{
    public class EnemyController : PoolObjectBase,IEnemyController
    {
        public IEnemyCharacterData CharacterData { get; private set;}

        [SerializeField] private EnemyUIController _enemyUIController;
        [SerializeField] private EnemyMovementSystem _enemyMovementSystem;
        [SerializeField] private EnemyBodySystem _enemyBodySystem;

        public override void InitializeSystems(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            
            CharacterData.Initialize();
            _enemyUIController.Initialize(this);
            _enemyBodySystem.Initialize(this);
            _enemyMovementSystem.Initialize();
        }
        
        private void Awake()
        {
            CharacterData = new EnemyCharacterData();
            _enemyBodySystem.OnDied += OnOnPoolObjectDied;
            _enemyBodySystem.AnimationDie.OnAnimationDieEnded += SetActiveObject;
        }

        private void SetActiveObject()
        {
            gameObject.SetActive(false);
        }
        
        private void OnDestroy()
        {
            _enemyBodySystem.OnDied -= OnOnPoolObjectDied;
            _enemyBodySystem.AnimationDie.OnAnimationDieEnded -= SetActiveObject;
        }
    }
}