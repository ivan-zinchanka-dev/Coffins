using FallingObjects;
using Management;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class ControlledBody : MonoBehaviour {
        
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private AudioSource _hitSound;

        [SerializeField] private UnityEvent _onSkeletonCaught;
        [SerializeField] private UnityEvent _onBombnCaught;
        
        public UnityEvent OnSkeletonCaught => _onSkeletonCaught;
        public UnityEvent OnBombCaught => _onBombnCaught;

        private void OnEnable()
        {
            GameManager.Instance.OnSessionRestart += OnSessionRestart;
        }

        private void OnSessionRestart()
        {
            _render.color = Color.white;
        }

        private void OnTriggerEnter2D(Collider2D other) {

            if (other.TryGetComponent<Skeleton>(out Skeleton skeleton))
            {
                skeleton.OnCaught();
                _hitSound.Play();

                _onSkeletonCaught?.Invoke();
            }
            else if (other.TryGetComponent<Bomb>(out Bomb bomb)) {

                bomb.Detonate();
                _render.color = Color.gray;

                _onBombnCaught?.Invoke();
            }
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnSessionRestart -= OnSessionRestart;
        }
    }
}
