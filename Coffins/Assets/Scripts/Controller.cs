using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private float speed = 35.0f;
    [SerializeField] private float animation_speed = 0.7f;
    public Animator animator;

    private float begin = 0, end = 0;

    private int detDirection(float begin, float end) {

        if (begin < end)
        {
            animator.speed = animation_speed;
            animator.SetBool("Left", false);
            animator.SetBool("Right", true); 
            return 1;
        }
        else if (begin > end)
        {
            animator.speed = animation_speed;
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
            return -1;
        }
        else {

            animator.speed = 0;
            return 0;
        }

    }


    private void Start(){

        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        detDirection(begin, end);
        begin = player.position.x;
        end = player.position.x;
        
    }

    private void OnMouseDown(){

        if (SceneManager.Instance.GameOver)
        {
            Application.LoadLevel("MainScene");
        }
        else if (SceneManager.FirstGame) {

            SceneManager.FirstGame = false;
            Application.LoadLevel("MainScene");
        }

    }

    private void OnMouseDrag(){

        Vector3 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPos.y = player.position.y;

        fingerPos.x = (fingerPos.x > 5.5f) ? 5.5f : fingerPos.x;
        fingerPos.x = (fingerPos.x < -5.5f) ? -5.5f : fingerPos.x;

        begin = player.position.x;
        
        player.position = Vector2.MoveTowards(player.position, fingerPos, speed * Time.deltaTime);

        end = player.position.x;        

    }


}
