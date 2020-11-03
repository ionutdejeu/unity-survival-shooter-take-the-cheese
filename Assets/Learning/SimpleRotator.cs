using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    float handleSpherRadius = 10f;
    private Camera cam;
    public bool startDrag = false;
    public Transform target;
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void OnGUI()
    {


        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(target.position, handleSpherRadius);
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
            Debug.DrawRay(r.origin,r.direction);
            if (Input.GetMouseButton(0))
            {
                startDrag = true;
                Ray spherIntersectRay = new Ray(target.position, point);
                //Debug.DrawLine(spherIntersectRay.origin, spherIntersectRay.GetPoint(handleSpherRadius));
                //Gizmos.DrawSphere(spherIntersectRay.GetPoint(handleSpherRadius), 1f);
            }
            else
            {
                startDrag = false;
            }
        }
    }
}
