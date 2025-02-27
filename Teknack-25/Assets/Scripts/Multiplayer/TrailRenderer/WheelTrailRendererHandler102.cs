using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRenderHandler102 : MonoBehaviour
{
    // Components
    private CarController102 carController;
    private TrailRenderer trailRenderer;

    

    void Awake()
{
    carController = GetComponentInParent<CarController102>();
    trailRenderer = GetComponent<TrailRenderer>();

    if (trailRenderer == null)
    {
        Debug.LogError($"[WheelTrailRenderHandler] No TrailRenderer found on {gameObject.name}");
    }
    else
    {
        trailRenderer.emitting = false; // Ensure it's off at the start
        trailRenderer.startWidth = 0.1f; // Adjust width
        trailRenderer.endWidth = 0.05f;  // Adjust width
    }
}


    void Update()
    {
        if (trailRenderer == null || carController == null) return; // Prevent errors

        if (carController.IsTyreScreeching(out float lateralVelocity, out bool isBraking))
        {
            Debug.Log("Tires screeching!"); // Debugging output
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
