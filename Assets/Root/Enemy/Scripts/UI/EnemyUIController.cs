using Root.Enemy.Scripts.Data;
using UnityEngine;

namespace Root.Enemy.Scripts.UI
{
    public class EnemyUIController : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        
        private IEnemyCharacterData _enemyCharacterData;
        
        public void Initialize(IEnemyController enemyController)
        {
            _enemyCharacterData = enemyController.CharacterData;
            _hpBar.Initialize(enemyController.CharacterData);
            Subscribe();
        }

        private void Subscribe()
        {
            _enemyCharacterData.CurrentCharacteristics.OnStatChanged += OnStatChangedHandler;
        }

        private void OnStatChangedHandler(string statName, float newValue)
        {
            if (statName != nameof(EnemyCharacteristics.Health)) return;
            _hpBar.ChangeSliderValue(newValue);
        }

        private void UnSubscribe()
        {
            _enemyCharacterData.CurrentCharacteristics.OnStatChanged -= OnStatChangedHandler;
        }
    }
}