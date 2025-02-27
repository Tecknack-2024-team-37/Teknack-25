
// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class FixedJoystick : Joystick
// {
// public override void OnPointerDown(PointerEventData eventData)
//     {
//         CarController.PointerDown = false;
//         CarControllerJump.PointerDown = false;
//         base.OnPointerDown(eventData);
//     }

//     public override void OnPointerUp(PointerEventData eventData)
//     {
//         CarController.PointerDown = true;
//         CarControllerJump.PointerDown = true;
//         base.OnPointerUp(eventData);
//     }
// }

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
public override void OnPointerDown(PointerEventData eventData)
    {
        CarController.PointerDown = false;
        CarControllerJump.PointerDown = false;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        CarController.PointerDown = true;
        CarControllerJump.PointerDown = true;
        base.OnPointerUp(eventData);
    }
}