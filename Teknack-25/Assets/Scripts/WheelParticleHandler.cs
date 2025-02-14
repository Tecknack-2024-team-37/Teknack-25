using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    // Local variables
    private float particleEmissionRate = 0;

    // Components
    private CarController carController;
    private ParticleSystem particleSystemSmoke;
    private ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake()
    {
        carController = GetComponentInParent<CarController>();
        particleSystemSmoke = GetComponent<ParticleSystem>();

        if (carController == null)
        {
            Debug.LogError("WheelParticleHandler: No CarController found on parent of " + gameObject.name);
        }

        if (particleSystemSmoke == null)
        {
            Debug.LogError("WheelParticleHandler: No ParticleSystem found on " + gameObject.name);
        }
        else
        {
            particleSystemEmissionModule = particleSystemSmoke.emission;
            particleSystemEmissionModule.rateOverTime = 0;
        }
    }

    void Update()
    {
        if (carController == null || particleSystemSmoke == null) return; // Prevent errors

        // Reduce the particle emission over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        if (carController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
                particleEmissionRate = 30;
            else
                particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
        }
    }
}
