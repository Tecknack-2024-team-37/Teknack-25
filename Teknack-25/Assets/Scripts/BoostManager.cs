using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public GameObject boostPrefab;
    public Transform[] spawnPoints; // Assign waypoints manually in Inspector
    public float spawnInterval = 10f;

   private void Start()
{
    Debug.Log("BoostManager Started");
    InvokeRepeating(nameof(SpawnBoostRandomly), 0, spawnInterval);
}


    // private void SpawnBoostRandomly()
    // {
    //     if (spawnPoints.Length == 0) return;

    //     int randomIndex = Random.Range(0, spawnPoints.Length);
    //     Vector3 spawnPosition = spawnPoints[randomIndex].position;

    //     if (!BoostExistsAt(spawnPosition))
    //     {
    //         Instantiate(boostPrefab, spawnPosition, Quaternion.identity);
    //     }
    // }
    private void SpawnBoostRandomly()
{
    if (spawnPoints.Length == 0)
    {
        Debug.LogError("No spawn points assigned!");
        return;
    }

    int randomIndex = Random.Range(0, spawnPoints.Length);
    Vector3 spawnPosition = spawnPoints[randomIndex].position;

    Debug.Log("Trying to spawn boost at: " + spawnPosition);

    if (!BoostExistsAt(spawnPosition))
    {
        Instantiate(boostPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Boost spawned at: " + spawnPosition);
    }
    else
    {
        Debug.Log("Boost already exists at this position.");
    }
}


    // private bool BoostExistsAt(Vector3 position)
    // {
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.CompareTag("Boost")) // Ensure boost objects have the "Boost" tag
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }
    private bool BoostExistsAt(Vector3 position)
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.2f);
    foreach (Collider2D collider in colliders)
    {
        if (collider.CompareTag("Boost")) 
        {
            Debug.Log("A boost already exists at this location.");
            return true;
        }
    }
    return false;
}

}
