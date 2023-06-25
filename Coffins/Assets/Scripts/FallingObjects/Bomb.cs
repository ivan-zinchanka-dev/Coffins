using Management;
using UnityEngine;

namespace FallingObjects
{
    public class Bomb : FallingObject
    {
        [SerializeField] private AudioSource _audioSource;

        public void Detonate()
        {
            EffectsSpawner.Instance.CreateExplosion(transform.position); 
            ReturnToPool();  
        }

        protected override void OnRespawn()
        {
            _audioSource.Play();
        }

        protected override void OnFell()
        {
            Detonate();
        }
    }
}