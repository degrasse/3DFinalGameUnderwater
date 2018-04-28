
 
using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour {
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 pos1 = new Vector3 ();
    Vector3 pos2 = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        pos1 = transform.position;
    }
     
  
    void Update () {

        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
  
        pos2 = pos1;
        pos2.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = pos2;
    }
}