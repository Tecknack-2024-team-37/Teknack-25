using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public float boostAmount = 0.7f; // Amount of boost to add

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the car has "Player" tag
        {
            CarController car = other.GetComponent<CarController>();
            if (car != null)
            {
                car.CollectBoost(boostAmount);
                Destroy(gameObject); // Remove the pickup after collection
            }
        }
    }
}
