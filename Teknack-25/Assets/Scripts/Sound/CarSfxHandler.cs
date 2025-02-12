// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarSfxHandler : MonoBehaviour
// {
//     [Header("Audio sources")]
//     public AudioSource tiresScreeachingAudioSource;
//     public AudioSource engineAudioSource;
//     public AudioSource carHitAudioSource;

//     //components
//     TopDownCarController1 topDownCarController;


//     void Awake(){
//         topDownCarController = GetComponentInParent<TopDownCarController1>();

//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         UpdateEngineSFX();
//         UpdateTiresScreechingSFX();
//     }

//     void UpdateEngineSFX(){
//         //Handle engine SFX
//         float velocityMagnitude = topDownCarController.GetVelocityMagnitude();
//     }
// }
