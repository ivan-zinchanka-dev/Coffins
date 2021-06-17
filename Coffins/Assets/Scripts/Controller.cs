using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 35.0f;
    [SerializeField] private float _animationSpeed = 0.7f;
    [SerializeField] private Animator _animator;

    private float _startPosition = 0, _targetPosition = 0;

    private const float LeftMovingBorder = -4.5f;
    private const float RightMovingBorder = 4.5f;

    private int DetermineDirection(float begin, float end) {

        if (begin < end)
        {
            _animator.speed = _animationSpeed;
            _animator.SetBool("Left", false);
            _animator.SetBool("Right", true); 
            return 1;
        }
        else if (begin > end)
        {
            _animator.speed = _animationSpeed;
            _animator.SetBool("Right", false);
            _animator.SetBool("Left", true);
            return -1;
        }
        else {

            _animator.speed = 0;
            return 0;
        }
    }

    private void Update()
    {
        DetermineDirection(_startPosition, _targetPosition);
        _startPosition = _player.position.x;
        _targetPosition = _player.position.x;    
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
        fingerPos.y = _player.position.y;

        fingerPos.x = (fingerPos.x > RightMovingBorder) ? RightMovingBorder : fingerPos.x;
        fingerPos.x = (fingerPos.x < LeftMovingBorder) ? LeftMovingBorder : fingerPos.x;

        _startPosition = _player.position.x;
        
        _player.position = Vector2.MoveTowards(_player.position, fingerPos, _speed * Time.deltaTime);

        _targetPosition = _player.position.x;        
    }

}
