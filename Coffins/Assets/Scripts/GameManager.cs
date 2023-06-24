using System.Collections;
using FallingObjects;
using ObjectsPool;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static GameManager Instance { get; private set; } = null;
    public static bool FirstGame { get; set; } = true;
    
    [SerializeField] private FloatingObject[] _skeletons;
    [SerializeField] private FloatingObject _bomb;
    [SerializeField] private ReactiveBody _coffin;
    [SerializeField] private TMPro.TMP_Text _msg;

    private ObjectPool _bombsPool;
    private ObjectPool _skeletonsLeftPool;
    private ObjectPool _skeletonsRightPool;

    private float _currentDelay = 1.5f;
    private const float _minDelay = 0.5f;
    private float _gameSpeed = 10.0f;
    private byte _gameStage = 1;
    private const float ScreenUpperBorder = 11.0f;
    private const float ScreenLeftBorder = -4.0f;
    private const float ScreenRightBorder = 4.0f;

    private bool _gameOver;

    public bool GameOver {

        get {
            
            return _gameOver;
        }

        set {

            if (value == true)
            {
                _msg.text = "Game over!\nTap to restart";
            }
            else {

                _msg.text = "";
            }
            
            _gameOver = value;
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Создано несколько объектов SceneManager!");
        }

        Instance = this;
    }

    private void Start(){

        _bombsPool = new ObjectPool(3, _bomb);
        _skeletonsLeftPool = new ObjectPool(2, _skeletons[0]);
        _skeletonsRightPool = new ObjectPool(2, _skeletons[1]);
        _gameOver = false;

        if (FirstGame)
        {
            _msg.text = "Tap to start";
            return;
        }

        StartCoroutine(Spawn());
    }

    private void Update(){

        if (_gameOver || FirstGame) {

            return;           
        } 

        if (_coffin.GetScore() / 100 > _gameStage - 1) {

            if (_gameStage < 20){

                _gameStage++;
            }

            if (_currentDelay > _minDelay)
            {
                _currentDelay -= 0.2f;
            }
        }      
    }

    private IEnumerator Spawn(){

        byte choise;
        FallingObject pref = null;
        FloatingObject bombClone = null;
        FloatingObject skeletonClone = null;

        while (!_gameOver) {

            choise = (byte) Random.Range(0, 2);

            if (choise == 0)
            {
                bombClone = _bombsPool.GetObject() as FloatingObject; 
                if (bombClone.TryGetComponent<FallingObject>(out pref)) pref.Respawn();
                bombClone.transform.position = new Vector2(Random.Range(ScreenLeftBorder, ScreenRightBorder), ScreenUpperBorder);             
            }
            else
            {
                choise = (byte)Random.Range(0, 2);

                if (choise == 0)
                {
                    skeletonClone = _skeletonsLeftPool.GetObject() as FloatingObject;
                    if (skeletonClone.TryGetComponent<FallingObject>(out pref)) pref.Respawn();
                    skeletonClone.transform.position = new Vector2(Random.Range(ScreenLeftBorder, ScreenRightBorder), ScreenUpperBorder);
                }
                else {

                    skeletonClone = _skeletonsRightPool.GetObject() as FloatingObject;
                    if (skeletonClone.TryGetComponent<FallingObject>(out pref)) pref.Respawn();
                    skeletonClone.transform.position = new Vector2(Random.Range(ScreenLeftBorder, ScreenRightBorder), ScreenUpperBorder);
                }                             
            }
            
            yield return new WaitForSeconds(_currentDelay);
        }
    }

    public float GetGravity(){

        return _gameSpeed * (1.0f + (float)_gameStage / 10);
    }

}
