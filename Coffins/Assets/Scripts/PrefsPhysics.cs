using UnityEngine;

public class PrefsPhysics : MonoBehaviour {

    [SerializeField] private AudioClip bones; 

    [SerializeField] private float gravity = 9.0f;                      
    [SerializeField] private float ground_level = -8.5f;
    private int id;
    private bool isGrounded = false;

    private AudioSource audioSource;

    public void Restart() {

        gravity = SceneManager.Instance.GetGravity();
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void BombDetonation()
    {
        isGrounded = false;
        SpecialEffectsManager.Instance.CreateExplosion(this.transform.position); 
        this.GetComponent<FloatingObject>().ReturnToPool();      
    }

    private void Update(){

        if (!isGrounded)
        {
            this.transform.position -= new Vector3(0, gravity * Time.deltaTime, 0);

            if (this.transform.position.y < ground_level)
            {
                isGrounded = true;

                if (this.gameObject.tag == "Skeleton")
                {                 
                    this.GetComponent<Animator>().SetBool("isFallen", true);

                    audioSource.clip = bones;
                    audioSource.Play();                  

                    SceneManager.Instance.GameOver = true;
                }
                else if (this.gameObject.tag == "Bomb") {

                    BombDetonation();
                }            
            }
          
        }   

    }
}
