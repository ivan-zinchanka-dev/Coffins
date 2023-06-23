using FallingObjects;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ReactiveBody : MonoBehaviour {

    [SerializeField] private TMPro.TMP_Text _scoreView; 
    [SerializeField] private SpriteRenderer _render;
    [SerializeField] private AudioSource _hitSound;

    private int _score = 0;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Skeleton")
        {
            other.GetComponent<FloatingObject>().ReturnToPool();
            _hitSound.Play();

            if (!GameManager.Instance.GameOver) {

                _score += 10;
                _scoreView.text = string.Format("{0:d}", _score);
            }
        }
        else if (other.gameObject.tag == "Bomb") {

            other.GetComponent<Bomb>().Detonate();
            _render.color = Color.gray;

            if (!GameManager.Instance.GameOver) {
              
                GameManager.Instance.GameOver = true;
            }            
        }
    }

    public int GetScore() {

        return _score;
    }


}
