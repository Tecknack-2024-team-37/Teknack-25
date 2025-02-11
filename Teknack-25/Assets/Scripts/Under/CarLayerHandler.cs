using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> carSpriteRenderers = new List<SpriteRenderer>();
    bool isOnOverpass = false; // Ensures the car stays in the correct layer

    [SerializeField] private Transform bridgeLeftLimit;  // Assign in Inspector
    [SerializeField] private Transform bridgeRightLimit; // Assign in Inspector

    private Rigidbody2D rb;

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
            isOnOverpass = true;
            UpdateSortingAndCollisionLayers();
            Debug.Log("🚗 Car entered Overpass - Switching to RaceTrackOverpass Layer");
        }
        else if (collider2d.CompareTag("OverpassExitTrigger"))  // Exit point of bridge
        {
            isOnOverpass = false;
            UpdateSortingAndCollisionLayers();
            Debug.Log("🏁 Car exited Overpass - Switching to Default Layer");
        }
    }
}
