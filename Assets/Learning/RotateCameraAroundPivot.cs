using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAroundPivot : MonoBehaviour
{
    public GameObject cameraDummy;
    public Transform pivotTransform;
    Camera cam;
    public bool DebugInfo = true;

    SphereCollider planetCollider;
    Vector3 prevHitPos;
    Vector3 newCamPos;
    bool released = true;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;
    void Start()
    {
        cam = Camera.main;
        planetCollider = pivotTransform.gameObject.GetComponent<SphereCollider>();
        newCamPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            //prevHitPos = Input.mousePosition;
            released = true;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
             
            Ray r = cam.ScreenPointToRay(mousePos);
            Debug.DrawRay(r.origin, r.direction);
             
            RaycastHit hit;

            Vector3 currentHitPoint = prevHitPos;
            if (planetCollider.Raycast(r, out hit, 100f))
            {
                currentHitPoint = hit.point;
                if (released)
                {
                    prevHitPos = currentHitPoint;
                    released = false;
                }
            }

            Quaternion angleDelta = Quaternion.FromToRotation(currentHitPoint-planetCollider.center, prevHitPos-planetCollider.center);
            Debug.DrawLine(prevHitPos,currentHitPoint);
            Debug.DrawRay(planetCollider.center,(currentHitPoint-planetCollider.center)*2,Color.red);
            
            
            newCamPos = angleDelta * newCamPos;
            Debug.DrawRay(planetCollider.center, newCamPos- planetCollider.center, Color.blue);

            float fracComplete = (Time.time - startTime) / journeyTime;
            newCamPos = Vector3.Slerp(newCamPos, cam.transform.position, fracComplete);
            cameraDummy.transform.position = newCamPos;
            cameraDummy.transform.LookAt(planetCollider.center);
            cam.transform.position = newCamPos;
            cam.transform.LookAt(planetCollider.center);
            prevHitPos = currentHitPoint;

             
        }
    }
}
