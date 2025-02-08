using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> carSpriteRenderers = new List<SpriteRenderer>();
    bool isDrivingOnOverpass = false;

    void Awake()
    {
        carSpriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    void UpdateSortingAndCollisionLayers()
    {
        string newLayer = isDrivingOnOverpass ? "RaceTrackOverpass" : "Default";
        SetSortingLayer(newLayer);
    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in carSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log("Triggered by: " + collider2d.gameObject.name); // Debug log

        if (collider2d.CompareTag("UnderpassTrigger"))
        {
            isDrivingOnOverpass = false;
            UpdateSortingAndCollisionLayers();
            Debug.Log("Exited Overpass - Switching to Default Layer"); // Debug log
        }
        else if (collider2d.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverpass = true;
            UpdateSortingAndCollisionLayers();
            Debug.Log("Entered Overpass - Switching to RaceTrackOverpass Layer"); // Debug log
        }
    }

    void OnTriggerExit2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverpass = false;
            UpdateSortingAndCollisionLayers();
            Debug.Log("Exited Overpass, Switched to Default Layer");
        }
    }
}
