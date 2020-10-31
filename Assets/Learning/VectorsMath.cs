using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorsMath : MonoBehaviour
{

    public Camera c;
    // Start is called before the first frame update
    void Start()
    {
         
    }
    Vector3 a = new Vector3(10, 0, 0);
    Vector3 b = new Vector3(0, 10, 0);
    // Update is called once per frame
    Vector2 pointOnScreenStart = new Vector2(10, 10);
    Vector2 pointOnScreenTarget = new Vector2(800, 400);
    void Update()
    {
        

        
        //Debug.DrawRay(Vector3.zero, a+b, Color.green);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, 10f);
        Gizmos.DrawRay(Vector3.zero, a + b);
        Ray r = new Ray(Vector3.zero, a + b);
        Gizmos.DrawSphere(r.GetPoint(10f), 1f);
        Gizmos.color = Color.red;

        Ray r1 = c.ScreenPointToRay(pointOnScreenStart);
        Ray r2 = c.ScreenPointToRay(pointOnScreenTarget);

        Gizmos.DrawLine(r1.origin, r1.GetPoint(20f));
        
        Gizmos.DrawLine(r2.origin, r2.GetPoint(20f));
        
        Vector3 startV = c.ScreenToWorldPoint(new Vector3(pointOnScreenStart.x, pointOnScreenStart.y, c.nearClipPlane));
        Vector3 endV = c.ScreenToWorldPoint(new Vector3(pointOnScreenTarget.x, pointOnScreenTarget.y, c.nearClipPlane));
        Ray spherIntersectRay = new Ray(Vector3.zero,startV);
        Gizmos.DrawLine(spherIntersectRay.origin,spherIntersectRay.GetPoint(10f));
        Gizmos.DrawSphere(spherIntersectRay.GetPoint(10f), 1f);
        Gizmos.color = Color.green;
         
        Gizmos.DrawLine(endV, Vector3.zero);
        Gizmos.DrawLine(startV, startV-endV);


    }
}
