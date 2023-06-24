using FallingObjects;
using UnityEngine;

public class ReactiveBody : MonoBehaviour {

    [SerializeField] private TMPro.TMP_Text _scoreView; 
    [SerializeField] private SpriteRenderer _render;
    [SerializeField] private AudioSource _hitSound;

    private int _score = 0;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.TryGetComponent<Skeleton>(out Skeleton skeleton))
        {
            skeleton.OnCaught();
            _hitSound.Play();

            if (!GameManager.Instance.GameOver) {

                _score += 10;
                _scoreView.text = string.Format("{0:d}", _score);
            }
        }
        else if (other.TryGetComponent<Bomb>(out Bomb bomb)) {

            bomb.Detonate();
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
