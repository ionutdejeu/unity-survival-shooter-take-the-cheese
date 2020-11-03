using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRotator : MonoBehaviour
{
    SphereCollider planetCollider;
    Camera cam;
    bool startDrag = false;
    Vector3 prevHitPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        planetCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, planetCollider.radius);
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        if (currentEvent != null)
        {
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            Ray r = cam.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            Gizmos.DrawRay(r);
            if (Input.GetMouseButton(0))
            {
                startDrag = true;
                Ray spherIntersectRay = new Ray(transform.position, point);
                Gizmos.DrawLine(spherIntersectRay.origin, spherIntersectRay.GetPoint(planetCollider.radius));
                Gizmos.DrawSphere(spherIntersectRay.GetPoint(planetCollider.radius), 1f);
                RaycastHit hit;
                Vector3 currentHitPoint = prevHitPos;
                if(planetCollider.Raycast(r,out hit, 100f))
                {
                    Gizmos.DrawSphere(hit.point, 1f);
                    currentHitPoint = hit.point;
                }

                Quaternion angle = Quaternion.FromToRotation(planetCollider.center-currentHitPoint, planetCollider.center-prevHitPos);
                Debug.Log(angle);
                Vector3 dir = planetCollider.transform.up - cam.transform.position;
                dir = angle * dir;
                cam.transform.position =  dir + planetCollider.center;
                cam.transform.LookAt(planetCollider.center);
                prevHitPos = currentHitPoint;
            }
            else
            {
                startDrag = false;
            }
        }

         
    }
    Vector3 RotatePointAroundPivot(Vector3 point2, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point2 - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point2 = dir + pivot; // calculate rotated point
        return point2; // return it
    }
}
