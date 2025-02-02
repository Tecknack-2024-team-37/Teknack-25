using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public FixedJoystick Joystick;
    Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed;

    private void Start() {
        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update() {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;

        //rotation
        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3 (0f, 0f, -zAxis);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
