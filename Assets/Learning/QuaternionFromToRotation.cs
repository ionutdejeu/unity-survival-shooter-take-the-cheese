using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionFromToRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Sets the rotation so that the transform's y-axis goes along the z-axis
        transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
