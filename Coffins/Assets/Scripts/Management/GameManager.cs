using System;
using Controllers;
using TMPro;
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour{

        public static GameManager Instance { get; private set; } = null;

        [SerializeField] private float _defaultGravity = 10.0f;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;
        [SerializeField] private ControlledBody _coffin;
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _gameStateText;
        
        private bool _gameOver;
        private bool _tutorialBlocker = true;
        
        private byte _gameStage = 0;
        private int _score = 0;

        private int _skeletonPrice = 10;
        private int _skeletonsToNextStage = 100;
        private int _maxGameStages = 20;
        
        
        public event Action OnSessionRestart;
        
        public bool GameOver {

            get => _gameOver;

            set
            {
                _gameOver = value;
                _gameStateText.text = _gameOver ? "Game over!\nTap to restart" : string.Empty;
            }
        }

        public float GetGravity(){

            return _defaultGravity * (1.0f + (float)_gameStage / 10);
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

        private void OnEnable()
        {
            _playerController.OnPointerDown += OnControllerSignal;
            _coffin.OnSkeletonCaught.AddListener(OnSkeletonCaught);
            _coffin.OnBombCaught.AddListener(OnBombCaught);
        }
        
        private void Start()
        {
            if (_tutorialBlocker)
            {
                _gameStateText.text = "Tap to start";
            }
        }

        private void OnControllerSignal()
        {
            if (_gameOver)
            {
                RestartSession();
            }
            else if (_tutorialBlocker) 
            {
                StartSession();
            }
        }
        
        private void StartSession()
        {
            _tutorialBlocker = false;
            _gameStateText.text = string.Empty;
            _fallingObjectsSpawner.StartSpawning();
        }

        private void RestartSession()
        {
            _gameOver = false;
            
            _score = 0;
            _scoreText.text = string.Format("{0:d}", _score);
            
            _gameStage = 0;
            StartSession();
            
            OnSessionRestart?.Invoke();
        }
        
        private void Update(){

            if (_gameOver || _tutorialBlocker) {

                return;           
            } 

            if (_score / _skeletonsToNextStage > _gameStage) {

                if (_gameStage < _maxGameStages){

                    _gameStage++;
                }

                _fallingObjectsSpawner.DecreaseDelay();
            }      
        }
        
        private void OnSkeletonCaught()
        {
            if (!_gameOver) {

                _score += _skeletonPrice;
                _scoreText.text = string.Format("{0:d}", _score);
            }
        }
        
        private static void OnBombCaught()
        {
            if (!Instance.GameOver) {
              
                Instance.GameOver = true;
            }   
        }
        
        private void OnDisable()
        {
            _coffin.OnBombCaught.RemoveListener(OnBombCaught);
            _coffin.OnSkeletonCaught.RemoveListener(OnSkeletonCaught);
            _playerController.OnPointerDown -= OnControllerSignal;
        }
    }
}
