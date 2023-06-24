using System.Collections;
using System.Collections.Generic;
using FallingObjects;
using ObjectsPool;
using UnityEngine;

namespace Managers
{
    public class FallingObjectsGenerator : MonoBehaviour
    {
        [SerializeField] private float _currentDelay = 1.5f;
        [SerializeField] private float _minDelay = 0.5f;
        [SerializeField] private float _delayDecreasingStep = 0.2f;
        
        [Space]
        [SerializeField] private List<Skeleton> _skeletonOriginals;
        [SerializeField] private Bomb _bombOriginal;
        
        private ObjectPool _bombsPool;
        private ObjectPool _skeletonsLeftPool;
        private ObjectPool _skeletonsRightPool;
        
        private const float ScreenUpperBorder = 11.0f;
        private const float ScreenLeftBorder = -4.0f;
        private const float ScreenRightBorder = 4.0f;
        
        private void Awake()
        {
            _bombsPool = new ObjectPool(3, _bombOriginal);
            _skeletonsLeftPool = new ObjectPool(2, _skeletonOriginals[0]);
            _skeletonsRightPool = new ObjectPool(2, _skeletonOriginals[1]);
        }
        
        public void Begin(){
            
            StartCoroutine(Spawn());
        }

        public void DecreaseDelay()
        {
            if (_currentDelay > _minDelay)
            {
                _currentDelay -= _delayDecreasingStep;
            }
        }

        private static bool RandomOfTwo()
        {
            return Random.Range(0, 2) == 0;
        }

        private IEnumerator Spawn()
        {
            while (!GameManager.Instance.GameOver)
            {
                if (RandomOfTwo())
                {
                    Bomb bomb = (Bomb)_bombsPool.GetObject();
                    bomb.Respawn();
                    bomb.transform.position = new Vector2(Random.Range(ScreenLeftBorder, ScreenRightBorder),
                        ScreenUpperBorder);
                }
                else
                {
                    Skeleton skeleton = RandomOfTwo()
                        ? (Skeleton)_skeletonsLeftPool.GetObject()
                        : (Skeleton)_skeletonsRightPool.GetObject();

                    skeleton.Respawn();
                    skeleton.transform.position = new Vector2(Random.Range(ScreenLeftBorder, ScreenRightBorder),
                        ScreenUpperBorder);
                }

                yield return new WaitForSeconds(_currentDelay);
            }
        }
    }
}