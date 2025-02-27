using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler101 : MonoBehaviour
{
    // Local variables
    private float particleEmissionRate = 0;

    // Components
    private CarController101 carController;
    private ParticleSystem particleSystemSmoke;
    private ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake()
    {
        carController = GetComponentInParent<CarController101>();
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
            particleSystemEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0);
        }
    }

    void Update()
    {
        if (carController == null || particleSystemSmoke == null) return; // Prevent errors

        // Reduce the particle emission over time smoothly
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(particleEmissionRate);

        // Tyre screeching effect
        if (carController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
        {
            particleEmissionRate = isBraking ? 30 : Mathf.Abs(lateralVelocity) * 2;
        }
    }
}
