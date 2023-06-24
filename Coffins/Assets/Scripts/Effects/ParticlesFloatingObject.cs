using ObjectsPool;
using UnityEngine;

namespace Effects
{
    public class ParticlesFloatingObject : FloatingObject
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public float Phase => _particleSystem.main.duration;
    }
}