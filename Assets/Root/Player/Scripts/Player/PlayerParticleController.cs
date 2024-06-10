using UnityEngine;

namespace Root.Player.Scripts.Player
{
    public class PlayerParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particleSystems;

        public void PlayParticleSystems()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }
    }
}