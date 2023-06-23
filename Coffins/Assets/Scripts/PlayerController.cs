using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 35.0f;
    [SerializeField] private float _animationSpeed = 0.7f;
    [SerializeField] private Animator _animator;

    private float _startPositionX = 0, _targetPositionX = 0;

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
        DetermineDirection(_startPositionX, _targetPositionX);
        _startPositionX = _player.position.x;
        _targetPositionX = _startPositionX;    
    }

    private void OnMouseDown(){

        if (GameManager.Instance.GameOver)
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (GameManager.FirstGame) {

            GameManager.FirstGame = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnMouseDrag(){

        Vector3 fingerPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        fingerPos.y = _player.position.y;

        fingerPos.x = (fingerPos.x > RightMovingBorder) ? RightMovingBorder : fingerPos.x;
        fingerPos.x = (fingerPos.x < LeftMovingBorder) ? LeftMovingBorder : fingerPos.x;

        _startPositionX = _player.position.x;
        
        _player.position = Vector2.MoveTowards(_player.position, fingerPos, _speed * Time.deltaTime);

        _targetPositionX = _player.position.x;        
    }

}
