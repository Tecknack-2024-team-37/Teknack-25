using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRenderHandler : MonoBehaviour
{
    // Components
    TopDownCarController topDownCarController;
    TrailRenderer trailRenderer; // Fixed type

    void Awake() 
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();
        
        trailRenderer = GetComponent<TrailRenderer>(); // Fixed method usage
        
        trailRenderer.emitting = false; // No error now
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (topDownCarController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
