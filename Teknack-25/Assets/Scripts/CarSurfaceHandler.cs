// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarSurfaceHandler : MonoBehaviour
// {
//     [Header("Surface detection")]
//     public LayerMask surfaceLayer;

//     Collider2D carCollider;

//     void Awake(){
//         carCollider = GetComponentInChildren<Collider2D>();
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         ContactFilter2D contactFilter2D = new ContactFilter2D();
//         contactFilter2D.layerMask = surfaceLayer;
//         contactFilter2D.useLayerMask = true;
//         contactFilter2D.useTriggers = true;

//         int numberOfHits = Physics2D.OverlapCollider(carCollider,contactFilter2D,surfaceCollidersHit   );
        
//         for(int i=0;i< numberOfHits;i++){
//             surfaceLayer surface = surfaceCollider
//         }
//     }
// }
