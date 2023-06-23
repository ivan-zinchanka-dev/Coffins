using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(FloatingObject))]
public class PrefsPhysics : MonoBehaviour {

    [SerializeField] private AudioClip _bonesSound; 

    [SerializeField] private float _gravity = 9.0f;                      
    [SerializeField] private float _groundLevel = -8.5f;

    private bool _isGrounded = false;

    private AudioSource _audioSource;
    private Animator _animator;
    private FloatingObject _floatingObject;

    public void Restart() {

        _gravity = GameManager.Instance.GetGravity();
        _animator = GetComponent<Animator>();
        _audioSource = this.GetComponent<AudioSource>();
        _floatingObject = GetComponent<FloatingObject>();
        _audioSource.Play();
    }

    public void BombDetonation()
    {
        _isGrounded = false;
        SpecialEffectsManager.Instance.CreateExplosion(this.transform.position); 
        _floatingObject.ReturnToPool();      
    }

    private void Update(){

        if (!_isGrounded)
        {
            this.transform.position -= new Vector3(0, _gravity * Time.deltaTime, 0);

            if (this.transform.position.y < _groundLevel)
            {
                _isGrounded = true;

                if (this.gameObject.tag == "Skeleton")
                {                 
                    _animator.SetBool("isFallen", true);

                    _audioSource.clip = _bonesSound;
                    _audioSource.Play();                  

                    GameManager.Instance.GameOver = true;
                }
                else if (this.gameObject.tag == "Bomb") {

                    BombDetonation();
                }            
            }
          
        }   

    }
}
