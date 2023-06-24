using ObjectsPool;
using UnityEngine;

namespace FallingObjects
{
    public class FallingObject : FloatingObject {
        
        [SerializeField] private float _gravity = 9.0f;                      
        [SerializeField] private float _groundLevel = -8.5f;

        private bool _isGrounded = false;
        
        public void Respawn() {

            _gravity = GameManager.Instance.GetGravity();
            _isGrounded = false;
            
            OnRespawn();
        }

        protected virtual void OnRespawn() { }
        
        protected virtual void OnFell() { }
        
        private void Update(){

            if (!_isGrounded)
            {
                transform.position -= new Vector3(0, _gravity * Time.deltaTime, 0);

                if (transform.position.y < _groundLevel)
                {
                    _isGrounded = true;
                    OnFell();
                }
            }
        }
    }
}
