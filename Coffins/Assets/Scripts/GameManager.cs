using Managers;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static GameManager Instance { get; private set; } = null;
    
    [SerializeField] private FallingObjectsGenerator _fallingObjectsGenerator;
    [SerializeField] private ReactiveBody _coffin;
    [SerializeField] private TMPro.TMP_Text _msg;

    public static bool FirstGame { get; set; } = true;

    
    private float _gameSpeed = 10.0f;
    private byte _gameStage = 1;
    

    private bool _gameOver;

    public bool GameOver {

        get => _gameOver;

        set
        {
            _gameOver = value;
            _msg.text = _gameOver ? "Game over!\nTap to restart" : string.Empty;
        }
    }

    public float GetGravity(){

        return _gameSpeed * (1.0f + (float)_gameStage / 10);
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple SceneManager objects created!");
        }

        Instance = this;
        
        _gameOver = false;
    }

    private void Start()
    {
        if (FirstGame)
        {
            _msg.text = "Tap to start";
        }
        else
        {
            _fallingObjectsGenerator.Begin();
        }
    }


    private void Update(){

        if (_gameOver || FirstGame) {

            return;           
        } 

        if (_coffin.GetScore() / 100 > _gameStage - 1) {

            if (_gameStage < 20){

                _gameStage++;
            }

            _fallingObjectsGenerator.DecreaseDelay();
        }      
    }
}
