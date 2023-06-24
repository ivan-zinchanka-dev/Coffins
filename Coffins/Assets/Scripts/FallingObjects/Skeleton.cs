using ObjectsPool;
using UnityEngine;

namespace FallingObjects
{
    public class Skeleton : FallingObject
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _bonesSound;

        private static readonly int IsFallenParam = Animator.StringToHash("isFallen");

        protected override void OnFell()
        {
            _animator.SetBool(IsFallenParam, true);
            _audioSource.PlayOneShot(_bonesSound);
            
            GameManager.Instance.GameOver = true;
        }

        public void OnCaught()
        {
            ReturnToPool();
        }
    }
}