using ObjectsPool;
using UnityEngine;

public class ParticlesFloatingObject : FloatingObject
{
    [SerializeField] private ParticleSystem _particleSystem;

    public float Phase => _particleSystem.main.duration;
}