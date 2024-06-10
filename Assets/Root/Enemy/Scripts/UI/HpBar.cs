using Root.Enemy.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Root.Enemy.Scripts.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private IEnemyCharacterData _enemyCharacterData;
        
        private const int minSliderValue = 0;

        public void Initialize(IEnemyCharacterData characterData)
        {
            _enemyCharacterData = characterData;
            InitializeSlider();
        }

        private void LateUpdate()
        {
            var transform1 = UnityEngine.Camera.main.transform;
            transform.LookAt(new Vector3(transform.position.x,transform1.position.y,transform1.position.z));
            transform.Rotate(0,180,0);
        }

        public void ChangeSliderValue(float value)
        {
            if (_slider.value <= value)
            {
                _slider.value = minSliderValue;
            }
            else
            {
                _slider.value = value;
            }
        }
        
        private void InitializeSlider()
        {
            _slider.maxValue = _enemyCharacterData.CurrentCharacteristics.Health;
            _slider.value = _enemyCharacterData.CurrentCharacteristics.Health;
        }
    }
}