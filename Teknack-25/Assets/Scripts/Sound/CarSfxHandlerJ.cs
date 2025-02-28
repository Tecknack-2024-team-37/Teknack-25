using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSfxHandlerJ : MonoBehaviour
{
    [Header("Audio sources")]
    public AudioSource tiresScreeachingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    //components
    CarController topDownCarController;


    void Awake(){
        topDownCarController = GetComponentInParent<CarController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTiresScreechingSFX();
    }

    void UpdateEngineSFX(){
        //Handle engine SFX
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();

        //Increase the engine volumne as the car is faster
        float desiredEngineVolume = velocityMagnitude * 0.09f;

        //But keep a minimum level so it plays even if the car is idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume,0.3f,1.0f);


        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume,desiredEngineVolume,Time.deltaTime * 10);

        //To add more variation to the engine sound we also change the pitch

        desiredEnginePitch = velocityMagnitude * 0.3f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch,0.6f,2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX(){
        // if(topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        if (topDownCarController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))

        {
            //If car is braking we want the tire  screech to be louder and also change the pitch
            if(isBraking){
                    tiresScreeachingAudioSource.volume = Mathf.Lerp(tiresScreeachingAudioSource.volume, 1.0f,Time.deltaTime*10);
                    tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.6f, Time.deltaTime * 10);
            }
            else{
                // If we are not braking we still want to play this screech sound if the player is drifting.
                tiresScreeachingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.06f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else tiresScreeachingAudioSource.volume = Mathf.Lerp(tiresScreeachingAudioSource.volume,0, Time.deltaTime * 10);
    }
}
