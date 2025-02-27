// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Surface : MonoBehaviour
// {
//     public enum SurfaceTypes {Road, Sand, Grass, Oil};
//     [Header("Surface")]
//     public  SurfaceTypes surfaceType;

//     //Start is called before frame update
//     void Start(){
         
//     }
// }
using UnityEngine;

public class Surface : MonoBehaviour
{
    public enum SurfaceTypes { Road, Sand, Grass, Oil }

    [Header("Surface Type")]
    [SerializeField] public SurfaceTypes surfaceType;

    public SurfaceTypes SurfaceType => surfaceType; // Read-only property
    
void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Car entered: " + gameObject.tag);
}

}
