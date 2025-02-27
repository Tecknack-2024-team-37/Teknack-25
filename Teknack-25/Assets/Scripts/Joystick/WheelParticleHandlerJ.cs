// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class WheelParticleHandlerJ : MonoBehaviour
// // {
// //     // Local variables
// //     private float particleEmissionRate = 0;

// //     // Components
// //     private CarController carController;
// //     private ParticleSystem particleSystemSmoke;
// //     private ParticleSystem.EmissionModule particleSystemEmissionModule;
// //     ParticleSystem.MainModule particleSystemMainModule;
// //     void Awake()
// //     {
// //         carController = GetComponentInParent<CarController>();
// //         particleSystemSmoke = GetComponent<ParticleSystem>();

// //         if (carController == null)
// //         {
// //             Debug.LogError("WheelParticleHandler: No CarController found on parent of " + gameObject.name);
// //         }

// //         if (particleSystemSmoke == null)
// //         {
// //             Debug.LogError("WheelParticleHandler: No ParticleSystem found on " + gameObject.name);
// //         }
// //         else
// //         {
// //             particleSystemEmissionModule = particleSystemSmoke.emission;
// //             particleSystemMainModule = particleSystemSmoke.main;
// //             particleSystemEmissionModule.rateOverTime = 0;

// //         }
// //     }

// //     void Update()
// //     {
// //         if (carController == null || particleSystemSmoke == null) return; // Prevent errors

// //         // Reduce the particle emission over time
// //         particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
// //         particleSystemEmissionModule.rateOverTime = particleEmissionRate;

// //         switch(carController.GetSurface()){
// //             case Surface.SurfaceTypes.Road : 
// //                 particleSystemMainModule.startColor = new Color(0.83f, 0.83f,0.83f);
// //                 break;

// //             case Surface.SurfaceTypes.Sand :
// //                 particleEmissionRate = carController.GetVelocityMagnitude(); 
// //                 particleSystemMainModule.startColor = new Color(0.64f, 0.42f, 0.24f);
// //                 break;
            
// //             case Surface.SurfaceTypes.Grass :
// //                 particleEmissionRate = carController.GetVelocityMagnitude(); 
// //                 particleSystemMainModule.startColor = new Color(0.15f, 0.4f, 0.13f);
// //                 break;

// //             case Surface.SurfaceTypes.Oil : 
// //                 particleSystemMainModule.startColor = new Color(0.2f, 0.2f, 0.2f);
// //                 break;
// //         }

// //         if (carController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
// //         {
// //             if (isBraking)
// //                 particleEmissionRate = 30;
// //             else
// //                 particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
// //         }
// //     }
// // }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WheelParticleHandlerJ : MonoBehaviour
// {
//     // Local variables
//     private float particleEmissionRate = 0;
//     private Surface.SurfaceTypes lastSurface = Surface.SurfaceTypes.Road; // Cache last surface

//     // Components
//     private CarController carController;
//     private ParticleSystem particleSystemSmoke;
//     private ParticleSystem.EmissionModule particleSystemEmissionModule;
//     // private ParticleSystem.MainModule particleSystemMainModule;

//     void Awake()
//     {
//         carController = GetComponentInParent<CarController>();
//         particleSystemSmoke = GetComponent<ParticleSystem>();

//         if (carController == null)
//         {
//             Debug.LogError("WheelParticleHandler: No CarController found on parent of " + gameObject.name);
//         }

//         if (particleSystemSmoke == null)
//         {
//             Debug.LogError("WheelParticleHandler: No ParticleSystem found on " + gameObject.name);
//         }
//         else
//         {
//             particleSystemEmissionModule = particleSystemSmoke.emission;
//             particleSystemMainModule = particleSystemSmoke.main;
//             particleSystemEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0);
//         }
//     }

//     void Update()
//     {
//         if (carController == null || particleSystemSmoke == null) return; // Prevent errors

//         // Reduce the particle emission over time smoothly
//         particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
//         particleSystemEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(particleEmissionRate);

//         Surface.SurfaceTypes currentSurface = carController.GetSurface();

//         // Only update color if the surface has changed
//         if (currentSurface != lastSurface)
//         {
//             switch (currentSurface)
//             {
//                 case Surface.SurfaceTypes.Road:
//                     particleSystemMainModule.startColor = new Color(0.83f, 0.83f, 0.83f);
//                     break;
//                 case Surface.SurfaceTypes.Sand:
//                     particleSystemMainModule.startColor = new Color(0.64f, 0.42f, 0.24f);
//                     break;
//                 case Surface.SurfaceTypes.Grass:
//                     particleSystemMainModule.startColor = new Color(0.15f, 0.4f, 0.13f);
//                     break;
//                 case Surface.SurfaceTypes.Oil:
//                     particleSystemMainModule.startColor = new Color(0.2f, 0.2f, 0.2f);
//                     break;
//             }
//             lastSurface = currentSurface; // Update last surface
//         }

//         float velocity = carController.GetVelocityMagnitude();
//         if (currentSurface == Surface.SurfaceTypes.Sand || currentSurface == Surface.SurfaceTypes.Grass)
//         {
//             particleEmissionRate = velocity;
//         }

//         if (carController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
//         {
//             particleEmissionRate = isBraking ? 30 : Mathf.Abs(lateralVelocity) * 2;
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandlerJ : MonoBehaviour
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
