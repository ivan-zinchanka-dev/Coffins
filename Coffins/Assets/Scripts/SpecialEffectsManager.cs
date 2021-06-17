using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance { get; private set; } = null;

    [SerializeField] private FloatingObject _smoke = null;
    [SerializeField] private FloatingObject _explosionSpark = null;
    [SerializeField] private float _sparkDuration = 0.1f;
    [SerializeField] private AudioSource _audioSource = null;

    private ObjectPool _smokePool;
    private ObjectPool _explosionPool;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple SpecialEffects objects created!");
        }

        Instance = this;
    }

    private void Start()
    {
        _smokePool = new ObjectPool(3, _smoke);
        _explosionPool = new ObjectPool(3, _explosionSpark);
    }

    public void CreateExplosion(Vector3 position) {

        _audioSource.Play();

        FloatingObject smoke_clone = _smokePool.GetObject() as FloatingObject;
        smoke_clone.transform.position = position;
        StartCoroutine(smoke_clone.ReturnToPool(_smoke.GetComponent<ParticleSystem>().duration * 2));

        FloatingObject explosion_clone = _explosionPool.GetObject() as FloatingObject;
        explosion_clone.transform.position = position;
        StartCoroutine(explosion_clone.ReturnToPool(_sparkDuration));
    }

}
