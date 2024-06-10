using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Root.Enemy.Scripts.MovementSystem
{
    public class EnemyMovementSystem : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _radiusFindPath = 3f;

        private Vector3 _spawnPosition;
        
        public void Initialize()
        {
            _spawnPosition = transform.position;
            _navMeshAgent.ResetPath();
            MoveToRandomPoint();
        }
        
        private void Update()
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
            {
                MoveToRandomPoint();
            }
        }
        
        private void MoveToRandomPoint()
        {
            if (TryGetRandomPointOnNavMesh(_spawnPosition, _radiusFindPath, out var randomPoint))
            {
                _navMeshAgent.SetDestination(randomPoint);
            }
        }

        private bool TryGetRandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * radius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }

            result = Vector3.zero;
            return false;
        }
    }
}