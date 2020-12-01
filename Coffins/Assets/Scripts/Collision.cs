using UnityEngine;

public class Collision : MonoBehaviour {

    public TMPro.TMP_Text t_score;

    private int score = 0;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Skeleton") {

            Destroy(other.gameObject);
            score += 10;

            t_score.text = string.Format("{0:d}", score);

        }
    }

    public int GetScore() {

        return score;
    }


}
