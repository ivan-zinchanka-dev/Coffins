using UnityEngine;

namespace FallingObjects
{
    public class Bomb : FallingObject
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private FloatingObject _floatingObject;

        public void Detonate()
        {
            SpecialEffectsManager.Instance.CreateExplosion(this.transform.position); 
            _floatingObject.ReturnToPool();  
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