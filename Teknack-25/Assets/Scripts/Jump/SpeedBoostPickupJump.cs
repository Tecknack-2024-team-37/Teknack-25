using UnityEngine;

public class SpeedBoostPickupJump : MonoBehaviour
{
    public float boostAmount = 1.0f; // Amount of boost to add

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the car has "Player" tag
        {
            CarControllerJump car = other.GetComponent<CarControllerJump>();
            if (car != null)
            {
                car.CollectBoost(boostAmount);
                Destroy(gameObject); // Remove the pickup after collection
            }
        }
    }
}
