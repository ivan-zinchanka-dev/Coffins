using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance = null;
    [SerializeField] private FloatingObject smoke = null;
    [SerializeField] private FloatingObject explosion_spark = null;
    [SerializeField] private float spark_duration = 0.25f;

    ObjectPool SmokePool;
    ObjectPool ExplosionPool;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Создано несколько объектов SpecialEffects!");
        }

        Instance = this;
    }

    private void Start()
    {
        SmokePool = new ObjectPool(3, smoke);
        ExplosionPool = new ObjectPool(3, explosion_spark);
    }

    public void CreateExplosion(Vector3 position) {

        this.GetComponent<AudioSource>().Play();

        FloatingObject smoke_clone = SmokePool.GetObject() as FloatingObject;
        smoke_clone.transform.position = position;
        StartCoroutine(smoke_clone.ReturnToPool(smoke.GetComponent<ParticleSystem>().duration * 2));

        FloatingObject explosion_clone = ExplosionPool.GetObject() as FloatingObject;
        explosion_clone.transform.position = position;
        StartCoroutine(explosion_clone.ReturnToPool(spark_duration));
    }

}
