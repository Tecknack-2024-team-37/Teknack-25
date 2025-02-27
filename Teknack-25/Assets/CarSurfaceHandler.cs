// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class CarSurfaceHandler : MonoBehaviour
// // {
// //     [Header("Surface Detection")]
// //     public LayerMask surfaceLayer;

// //     // Hit check
// //     private Collider2D[] surfaceCollidersHit = new Collider2D[10];
// //     private Vector3 lastSampledSurfacePosition = Vector3.one * 10000;

// //     // Surface type
// //     private Surface.SurfaceTypes drivingOnSurface = Surface.SurfaceTypes.Road;
// //     private Collider2D carCollider;

// //     void Awake()
// //     {
// //         carCollider = GetComponentInChildren<Collider2D>();
// //     }

// //     void Update()
// //     {
// //         // Check if the car moved at least 0.75 units before sampling the surface again
// //         if ((transform.position - lastSampledSurfacePosition).sqrMagnitude < 0.75f)
// //             return;

// //         // Contact filter setup
// //         ContactFilter2D contactFilter2D = new ContactFilter2D
// //         {
// //             layerMask = surfaceLayer,
// //             useLayerMask = true,
// //             useTriggers = false
// //         };

// //         int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter2D, surfaceCollidersHit);

// //         float lastSurfaceZValue = -1000;
// //         for (int i = 0; i < numberOfHits; i++)
// //         {
// //             Surface surface = surfaceCollidersHit[i].GetComponent<Surface>();

// //             if (surface != null && surface.transform.position.z > lastSurfaceZValue)
// //             {
// //                 drivingOnSurface = surface.surfaceType;
// //                 lastSurfaceZValue = surface.transform.position.z;
// //             }
// //         }

// //         // If no surface is detected, default to Road
// //         if (numberOfHits == 0)
// //         {
// //             drivingOnSurface = Surface.SurfaceTypes.Road;
// //         }

// //         // Update last sampled position only when the surface is checked
// //         lastSampledSurfacePosition = transform.position;

// //         Debug.Log($"Driving on {drivingOnSurface}");
// //     }

// //     public Surface.SurfaceTypes GetCurrentSurface(){
// //         return drivingOnSurface;
// //     }
// // }
// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class CarSurfaceHandler : MonoBehaviour
// // {
// //     [Header("Surface Detection")]
// //     [SerializeField] private LayerMask surfaceLayer;
// //     [SerializeField] private float detectionThreshold = 0.75f; // Adjusted for better control

// //     private Collider2D[] surfaceCollidersHit = new Collider2D[10];
// //     private Vector3 lastSampledSurfacePosition = Vector3.one * 10000;
// //     private Surface.SurfaceTypes drivingOnSurface = Surface.SurfaceTypes.Road;
// //     private Collider2D carCollider;

// //     void Awake()
// //     {
// //         carCollider = GetComponentInChildren<Collider2D>();
// //         if (carCollider == null)
// //         {
// //             Debug.LogError("CarSurfaceHandler: No Collider2D found in children!");
// //         }
// //     }

// //     void Update()
// //     {
// //         if (!ShouldResampleSurface()) return;

// //         DetectSurfaceType();
// //         lastSampledSurfacePosition = transform.position;
// //     }

// //     private bool ShouldResampleSurface()
// //     {
// //         return (transform.position - lastSampledSurfacePosition).sqrMagnitude >= detectionThreshold * detectionThreshold;
// //     }

// //     // private void DetectSurfaceType()
// //     // {
// //     //     ContactFilter2D contactFilter = new ContactFilter2D
// //     //     {
// //     //         layerMask = surfaceLayer,
// //     //         useLayerMask = true,
// //     //         useTriggers = false
// //     //     };

// //     //     int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter, surfaceCollidersHit);
// //     //     Surface.SurfaceTypes detectedSurface = Surface.SurfaceTypes.Road; 
// //     //     float highestSurfaceZ = float.MinValue;

// //     //     for (int i = 0; i < numberOfHits; i++)
// //     //     {
// //     //         Surface surface = surfaceCollidersHit[i]?.GetComponent<Surface>();
// //     //         if (surface != null && surface.transform.position.z > highestSurfaceZ)
// //     //         {
// //     //             detectedSurface = surface.surfaceType;
// //     //             highestSurfaceZ = surface.transform.position.z;
// //     //         }
// //     //     }

// //     //     if (drivingOnSurface != detectedSurface)
// //     //     {
// //     //         drivingOnSurface = detectedSurface;
// //     //         Debug.Log($"Driving on {drivingOnSurface}");
// //     //     }
// //     // }

// //     // public Surface.SurfaceTypes GetCurrentSurface()
// //     // {
// //     //     return drivingOnSurface;
// //     // }

// //     private void DetectSurfaceType()
// // {
// //     ContactFilter2D contactFilter = new ContactFilter2D
// //     {
// //         layerMask = surfaceLayer,
// //         useLayerMask = true,
// //         useTriggers = false
// //     };

// //     int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter, surfaceCollidersHit);
// //     Surface.SurfaceTypes detectedSurface = Surface.SurfaceTypes.Road; 
// //     float highestSurfaceZ = float.MinValue;

// //     Debug.Log($"Number of detected surfaces: {numberOfHits}");  // 🛠 Check if it's detecting anything

// //     for (int i = 0; i < numberOfHits; i++)
// //     {
// //         Surface surface = surfaceCollidersHit[i]?.GetComponent<Surface>();
// //         if (surface != null)
// //         {
// //             Debug.Log($"Detected Surface: {surface.surfaceType} at {surface.transform.position}"); // 🛠 Log surface type

// //             if (surface.transform.position.z > highestSurfaceZ)
// //             {
// //                 detectedSurface = surface.surfaceType;
// //                 highestSurfaceZ = surface.transform.position.z;
// //             }
// //         }
// //     }

// //     if (drivingOnSurface != detectedSurface)
// //     {
// //         drivingOnSurface = detectedSurface;
// //         Debug.Log($"Driving on {drivingOnSurface}");  // ✅ Should log "Driving on Oil" when on oil
// //     }

// //     ParticleSystem smoke = GetComponentInChildren<ParticleSystem>(); 
// // var main = smoke.main;

// // if (drivingOnSurface == Surface.SurfaceTypes.Oil)
// // {
// //     main.startColor = Color.black;  // Change to black smoke for oil
// // }
// // else if (drivingOnSurface == Surface.SurfaceTypes.Sand)
// // {
// //     main.startColor = Color.yellow;  // Dust effect
// // }
// // else
// // {
// //     main.startColor = Color.gray;  // Default road color
// // }

// // }

// // }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarSurfaceHandler : MonoBehaviour
// {
//     [Header("Surface Detection")]
//     [SerializeField] private LayerMask surfaceLayer;
//     [SerializeField] private float detectionThreshold = 0.75f; // Adjusted for better control

//     private Collider2D[] surfaceCollidersHit = new Collider2D[10];
//     private Vector3 lastSampledSurfacePosition = Vector3.one * 10000;
//     private Surface.SurfaceTypes drivingOnSurface = Surface.SurfaceTypes.Road;
//     private Collider2D carCollider;
//     private ParticleSystem smoke;

//     void Awake()
//     {
//         carCollider = GetComponentInChildren<Collider2D>();
//         if (carCollider == null)
//         {
//             Debug.LogError("❌ CarSurfaceHandler: No Collider2D found in children!");
//         }

//         smoke = GetComponentInChildren<ParticleSystem>();
//         if (smoke == null)
//         {
//             Debug.LogError("❌ CarSurfaceHandler: No ParticleSystem found in children!");
//         }
//     }

//     void Update()
//     {
//         if (!ShouldResampleSurface()) return;

//         DetectSurfaceType();
//         lastSampledSurfacePosition = transform.position;
//     }

//     private bool ShouldResampleSurface()
//     {
//         return (transform.position - lastSampledSurfacePosition).sqrMagnitude >= detectionThreshold * detectionThreshold;
//     }

//     private void DetectSurfaceType()
//     {
//         ContactFilter2D contactFilter = new ContactFilter2D
//         {
//             layerMask = surfaceLayer,
//             useLayerMask = true,
//             useTriggers = false
//         };

//         int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter, surfaceCollidersHit);
//         Surface.SurfaceTypes detectedSurface = Surface.SurfaceTypes.Road;
//         float highestSurfaceZ = float.MinValue;

//         Debug.Log($"🔍 Number of detected surfaces: {numberOfHits}"); // 🛠 Check if detection is working

//         for (int i = 0; i < numberOfHits; i++)
//         {
//             Surface surface = surfaceCollidersHit[i]?.GetComponent<Surface>();
//             if (surface != null)
//             {
//                 Debug.Log($"✅ Detected Surface: {surface.surfaceType} at {surface.transform.position}"); // 🛠 Log surface type

//                 if (surface.transform.position.z > highestSurfaceZ)
//                 {
//                     detectedSurface = surface.surfaceType;
//                     highestSurfaceZ = surface.transform.position.z;
//                 }
//             }
//         }

//         if (drivingOnSurface != detectedSurface)
//         {
//             drivingOnSurface = detectedSurface;
//             Debug.Log($"🚗 Driving on {drivingOnSurface}"); // ✅ Should log "Driving on Oil" when on oil
//             UpdateSmokeColor();
//         }
//     }

//     private void UpdateSmokeColor()
//     {
//         if (smoke == null) return; // Prevent null errors

//         var main = smoke.main;
//         switch (drivingOnSurface)
//         {
//             case Surface.SurfaceTypes.Oil:
//                 main.startColor = Color.black;  // Oil → Black smoke
//                 break;
//             case Surface.SurfaceTypes.Sand:
//                 main.startColor = Color.yellow;  // Sand → Dusty yellow
//                 break;
//             case Surface.SurfaceTypes.Grass:
//                 main.startColor = Color.green;  // Grass → Greenish smoke
//                 break;
//             default:
//                 main.startColor = Color.gray;  // Road → Normal gray smoke
//                 break;
//         }
//     }

//     public Surface.SurfaceTypes GetCurrentSurface()
//     {
//         return drivingOnSurface;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSurfaceHandler : MonoBehaviour
{
    [Header("Surface Detection")]
    [SerializeField] private LayerMask surfaceLayer; // Should include "Surfaces" Layer
    [SerializeField] private float detectionThreshold = 0.75f;

    private Collider2D[] surfaceCollidersHit = new Collider2D[10];
    private Vector3 lastSampledSurfacePosition = Vector3.one * 10000;
    private Surface.SurfaceTypes drivingOnSurface = Surface.SurfaceTypes.Road;
    private Collider2D carCollider;
    private ParticleSystem smoke;

    void Awake()
    {
        carCollider = GetComponentInChildren<Collider2D>();
        if (carCollider == null)
        {
            Debug.LogError("❌ No Collider2D found in children!");
        }

        smoke = GetComponentInChildren<ParticleSystem>();
        if (smoke == null)
        {
            Debug.LogError("❌ No ParticleSystem found in children!");
        }
    }

    void Update()
    {
        if (!ShouldResampleSurface()) return;

        DetectSurfaceType();
        lastSampledSurfacePosition = transform.position;
    }

    private bool ShouldResampleSurface()
    {
        return (transform.position - lastSampledSurfacePosition).sqrMagnitude >= detectionThreshold * detectionThreshold;
    }

    private void DetectSurfaceType()
    {
        ContactFilter2D contactFilter = new ContactFilter2D
        {
            layerMask = surfaceLayer,
            useLayerMask = true,
            useTriggers = false
        };

        int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter, surfaceCollidersHit);
        Surface.SurfaceTypes detectedSurface = Surface.SurfaceTypes.Road;
        float highestSurfaceZ = float.MinValue;

        Debug.Log($"🔍 Number of detected surfaces: {numberOfHits}");

        for (int i = 0; i < numberOfHits; i++)
        {
            Surface surface = surfaceCollidersHit[i]?.GetComponent<Surface>();
            if (surface != null)
            {
                Debug.Log($"✅ Detected Surface: {surface.surfaceType} at {surface.transform.position}");

                if (surface.transform.position.z > highestSurfaceZ)
                {
                    detectedSurface = surface.surfaceType;
                    highestSurfaceZ = surface.transform.position.z;
                }
            }
        }

        if (drivingOnSurface != detectedSurface)
        {
            drivingOnSurface = detectedSurface;
            Debug.Log($"🚗 Driving on {drivingOnSurface}");
            UpdateSmokeColor();
        }
    }

    private void UpdateSmokeColor()
    {
        if (smoke == null) return;

        var main = smoke.main;
        switch (drivingOnSurface)
        {
            case Surface.SurfaceTypes.Oil:
                main.startColor = Color.black;  // Oil → Black smoke
                break;
            case Surface.SurfaceTypes.Sand:
                main.startColor = Color.yellow;  // Sand → Dusty yellow
                break;
            case Surface.SurfaceTypes.Grass:
                main.startColor = Color.green;  // Grass → Greenish smoke
                break;
            default:
                main.startColor = Color.gray;  // Road → Normal gray smoke
                break;
        }
    }

    public Surface.SurfaceTypes GetCurrentSurface()
    {
        return drivingOnSurface;
    }
}
