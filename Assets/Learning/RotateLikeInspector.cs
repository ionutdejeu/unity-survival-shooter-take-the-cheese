using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLikeInspector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        mPosDelta = Input.mousePosition - mPrevPos;
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
        }
        mPrevPos = Input.mousePosition;
    }
}
