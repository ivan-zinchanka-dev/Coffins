using Effects;
using ObjectsPool;
using UnityEngine;

namespace Managers
{
    public class EffectsManager : MonoBehaviour
    {
        public static EffectsManager Instance { get; private set; } = null;

        [SerializeField] private ParticlesFloatingObject _smokeOriginal = null;
        [SerializeField] private FloatingObject _explosionSparkOriginal = null;
        [SerializeField] private float _sparkDuration = 0.1f;
        [SerializeField] private AudioSource _audioSource = null;

        private ObjectPool _smokePool;
        private ObjectPool _explosionPool;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple SpecialEffects objects created!");
            }

            Instance = this;
        
            _smokePool = new ObjectPool(3, _smokeOriginal);
            _explosionPool = new ObjectPool(3, _explosionSparkOriginal);
        }

        public void CreateExplosion(Vector3 position) {
        
            ParticlesFloatingObject smoke = (ParticlesFloatingObject)_smokePool.GetObject();
            smoke.transform.position = position;
            StartCoroutine(smoke.ReturnToPool(smoke.Phase * 2));

            FloatingObject explosion = _explosionPool.GetObject();
            explosion.transform.position = position;
            StartCoroutine(explosion.ReturnToPool(_sparkDuration));
        
            _audioSource.Play();
        }

    }
}
