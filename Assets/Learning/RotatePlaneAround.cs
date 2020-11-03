using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlaneAround : MonoBehaviour
{
    Plane planeToIntersect;
    // Start is called before the first frame update
    Camera cam;
    public GameObject pointerHit;
    void Start()
    {
        cam = Camera.main;   
        planeToIntersect = new Plane(transform.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray r = cam.ScreenPointToRay(mousePos);
            Debug.DrawRay(r.origin, r.direction);

            float dist;
            if (planeToIntersect.Raycast(r, out dist))
            {
                Vector3 hitPoint = r.GetPoint(dist);
                pointerHit.transform.position = hitPoint;
            }

        }
    }
}
