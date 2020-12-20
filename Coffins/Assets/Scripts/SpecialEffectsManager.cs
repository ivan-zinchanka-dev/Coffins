using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance = null;
    [SerializeField] private ParticleSystem smoke = null;
    [SerializeField] private SpriteRenderer explosion_spark = null;
    [SerializeField] private float spark_duration = 0.25f;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Создано несколько объектов SpecialEffects!");
        }

        Instance = this;
    }

    public void CreateExplosion(Vector3 position) {

        ParticleSystem smoke_clone = Instantiate(smoke, position, Quaternion.identity) as ParticleSystem;
        Destroy(smoke_clone.gameObject, smoke.duration * 2);

        SpriteRenderer explosion_clone = Instantiate(explosion_spark, position, Quaternion.identity) as SpriteRenderer;
        Destroy(explosion_clone.gameObject, spark_duration);

    }

}
