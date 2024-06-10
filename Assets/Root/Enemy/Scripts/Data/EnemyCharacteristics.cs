using System;

namespace Root.Enemy.Scripts.Data
{
    [Serializable]
    public class EnemyCharacteristics
    {
        public event Action<string, float> OnStatChanged;
        
        private float _health;

        public float MinHealth;
        public float Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    OnStatChanged?.Invoke(nameof(Health), _health);
                }
            }
        }
        
        public EnemyCharacteristics(float health,float minHealth)
        {
            Health = health;
            MinHealth = minHealth;
        }
    }
}