using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> carSpriteRenderers = new List<SpriteRenderer>();
    bool isOnOverpass = false; // Ensures the car stays in the correct layer

    void Awake()
    {
        carSpriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());

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

