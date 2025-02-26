using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject[] cars; // Assign existing cars in Inspector
    public FixedJoystick[] joysticks; // Assign different joysticks

    void Start()
    {
        AssignJoysticks();
    }

    void AssignJoysticks()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (i >= joysticks.Length) break; // Prevent index error

            CarController carController = cars[i].GetComponent<CarController>();
            if (carController != null)
            {
                carController.Joystick = joysticks[i]; // Assign joystick to each car
            }
        }
    }
}
