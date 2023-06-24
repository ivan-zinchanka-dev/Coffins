using Managers;
using UnityEngine;

namespace FallingObjects
{
    public class Bomb : FallingObject
    {
        [SerializeField] private AudioSource _audioSource;

        public void Detonate()
        {
            EffectsManager.Instance.CreateExplosion(this.transform.position); 
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