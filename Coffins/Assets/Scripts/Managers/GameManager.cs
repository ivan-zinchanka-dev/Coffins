using System;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour{

        public static GameManager Instance { get; private set; } = null;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private FallingObjectsGenerator _fallingObjectsGenerator;
        [SerializeField] private ControlledBody _coffin;
        [SerializeField] private TMPro.TMP_Text _msg;
        [SerializeField] private TMPro.TMP_Text _scoreView;
        
        private bool _tutorialBlocker = true;
        
        private int _score = 0;
        private float _gameSpeed = 10.0f;
        private byte _gameStage = 1;
    

        private bool _gameOver;
        public event Action OnSessionRestart;
        
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

        private void OnSkeletonCaught()
        {
            if (!_gameOver) {

                _score += 10;
                _scoreView.text = string.Format("{0:d}", _score);
            }
        }
        
        private static void OnBombCaught()
        {
            if (!Instance.GameOver) {
              
                Instance.GameOver = true;
            }   
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

        private void Start()
        {
            if (_tutorialBlocker)
            {
                _msg.text = "Tap to start";
            }
        }

        private void StartSession()
        {
            _tutorialBlocker = false;
            _msg.text = string.Empty;
            _fallingObjectsGenerator.StartSpawning();
        }

        private void RestartSession()
        {
            _gameOver = false;
            
            _score = 0;
            _scoreView.text = string.Format("{0:d}", _score);
            
            _gameSpeed = 10.0f;
            _gameStage = 1;
            
            StartSession();
            
            OnSessionRestart?.Invoke();
        }
        


        private void Update(){

            if (_gameOver || _tutorialBlocker) {

                return;           
            } 

            if (_score / 100 > _gameStage - 1) {

                if (_gameStage < 20){

                    _gameStage++;
                }

                _fallingObjectsGenerator.DecreaseDelay();
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
