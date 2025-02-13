using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> carSpriteRenderers = new List<SpriteRenderer>();
    bool isOnOverpass = false; // Ensures the car stays in the correct layer

    [SerializeField] private Transform bridgeLeftLimit;  // Initial bridge limits (assigned in Inspector)
    [SerializeField] private Transform bridgeRightLimit; // Can change dynamically

    private Rigidbody2D rb;

    // ✅ Lists to store GameObjects whose colliders should be toggled
    [SerializeField] private List<GameObject> objectsToDisableOnEntry; 
    [SerializeField] private List<GameObject> objectsToEnableOnExit;

    void Awake()
    {
        carSpriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
        rb = GetComponent<Rigidbody2D>();

        Debug.Log($"CarLayerHandler initialized. Found {carSpriteRenderers.Count} SpriteRenderers.");
    }

    void UpdateSortingAndCollisionLayers()
    {
        string newLayer = isOnOverpass ? "RaceTrackOverpass" : "Default";
        SetSortingLayer(newLayer);
        Debug.Log($"Sorting layer updated: {newLayer}");
    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in carSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
            Debug.Log($"SpriteRenderer {spriteRenderer.gameObject.name} now in layer: {layerName}");
        }
    }

    void FixedUpdate()
    {
        if (isOnOverpass && bridgeLeftLimit != null && bridgeRightLimit != null)
        {
            // Restrict car's X movement within the bridge boundaries
            float clampedX = Mathf.Clamp(transform.position.x, bridgeLeftLimit.position.x, bridgeRightLimit.position.x);
            transform.position = new Vector2(clampedX, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log($"Trigger entered by: {collider2d.gameObject.name} (Tag: {collider2d.tag})");

        if (collider2d.CompareTag("OverpassEntryTrigger"))  // Entry point of bridge
        {
            // Get the BridgeLimits component from the trigger
            BridgeLimits bridgeLimits = collider2d.GetComponent<BridgeLimits>();
            if (bridgeLimits != null)
            {
                bridgeLeftLimit = bridgeLimits.leftLimit;
                bridgeRightLimit = bridgeLimits.rightLimit;
                Debug.Log($"🚗 Updating bridge limits: Left={bridgeLeftLimit.position.x}, Right={bridgeRightLimit.position.x}");
            }
            else
            {
                Debug.LogError($"❌ No BridgeLimits component found on {collider2d.gameObject.name}!");
            }

            isOnOverpass = true;
            UpdateSortingAndCollisionLayers();
            ToggleColliders(objectsToDisableOnEntry, false); // Disable colliders on entry
            Debug.Log("🚗 Car entered Overpass - Switching to RaceTrackOverpass Layer");
        }
        else if (collider2d.CompareTag("OverpassExitTrigger"))  // Exit point of bridge
        {
            isOnOverpass = false;
            UpdateSortingAndCollisionLayers();
            ToggleColliders(objectsToEnableOnExit, true); // Enable colliders on exit
            Debug.Log("🏁 Car exited Overpass - Switching to Default Layer");
        }
    }

    private void ToggleColliders(List<GameObject> objects, bool state)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                Collider2D col = obj.GetComponent<Collider2D>(); // Get the Collider2D component
                if (col != null)
                {
                    col.enabled = state;
                    Debug.Log($"Collider on {obj.name} set to {state}");
                }
            }
        }
    }
}
