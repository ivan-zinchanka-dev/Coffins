using UnityEngine;

public class Collision : MonoBehaviour {

    public TMPro.TMP_Text t_score;
    private int score = 0;
    private SpriteRenderer render;

    private void Start()
    {
        render = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Skeleton")
        {
            other.GetComponent<FloatingObject>().ReturnToPool();
            this.GetComponent<AudioSource>().Play();

            if (!SceneManager.Instance.GameOver) {

                score += 10;
                t_score.text = string.Format("{0:d}", score);
            }
        }
        else if (other.gameObject.tag == "Bomb") {

            other.gameObject.GetComponent<PrefsPhysics>().BombDetonation();
            render.color = Color.gray;

            if (!SceneManager.Instance.GameOver) {
              
                SceneManager.Instance.GameOver = true;
            }            
        }
    }

    public int GetScore() {

        return score;
    }


}
