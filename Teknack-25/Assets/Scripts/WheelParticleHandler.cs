using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    //local variables
    float particleEmissionRate = 0;

    //components
    TopDownCarController1 topDownCarController;
    
    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController1>();

        particleSystemSmoke = GetComponent<ParticleSystem>();

        particleSystemEmissionModule = particleSystemSmoke.emission;

        particleSystemEmissionModule.rateOverTime = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //reduce the particle over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        if (topDownCarController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
        {
            if(isBraking)
                particleEmissionRate = 30;
            else particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
        }
    }
}
