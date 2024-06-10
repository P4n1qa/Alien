using System;
using System.Collections;
using UnityEngine;

namespace Root.Enemy.Scripts
{
    public class AnimationDie : MonoBehaviour
    {
        public event Action OnAnimationDieEnded;
        [SerializeField] private float _animationDuration = 1.0f;
        [SerializeField] private float _jumpHeight = 1.0f;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        public void StartAnimationDie(Transform mouthPlayer)
        {
            _startPosition = transform.position;
            _targetPosition = mouthPlayer.position;
            StartCoroutine(AnimateMovement());
        }
        
        private IEnumerator AnimateMovement()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _animationDuration)
            {
                float t = Mathf.Clamp01(elapsedTime / _animationDuration);
                
                float y = Mathf.Sin(t * Mathf.PI) * _jumpHeight;
                
                Vector3 position = Vector3.Lerp(_startPosition, _targetPosition, t);
                position.y += y;
                
                transform.position = position;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.position = _targetPosition;
            OnAnimationDieEnded?.Invoke();
        }
    }
}