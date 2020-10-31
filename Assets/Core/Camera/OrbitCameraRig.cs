using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Camera
{
    public class OrbitCameraRig : CameraRig
    {
        public GameObject OrbitArownd;
        protected BoundingSphere trackedObjBoundingSphere;
        protected virtual void Awake()
        {

            TrackedObject = OrbitArownd;
            CachedCamera = GetComponent<UnityEngine.Camera>();
            if (TrackedObject == null)
            {
                Debug.LogError("TrackedObject must be specified This component requires a target object ");
                return;
            }
            trackedObjBoundingSphere = new BoundingSphere(TrackedObject.transform.position, nearestZoom);
            CurrentLookPosition = LookPosition = trackedObjBoundingSphere.position;
            lastPointChecked = CameraPosition;
            CameraPosition = CachedCamera.transform.position;
            RawCurrentZoomDistance = CurrentZoomDistance = (CurrentLookPosition - CameraPosition).magnitude;
        }

        
        public override void PanCamera(Vector3 panDelta)
        {
            Vector3 pos = CameraPosition + panDelta; 
            CameraPosition = CurrentZoomDistance * pos.normalized; 
            //CameraPosition = transform.RotateAround(TrackedObject.transform.position,pos*)
            //CameraPosition = LookPosition + (GetToCamVector() * CurrentZoomDistance);

        }


        public override void PanTo(Vector3 position)
        {
            Vector3 pos = LookPosition - position;
            // Camera position calculated from look position with view vector and zoom dist
            CameraPosition = CurrentZoomDistance * pos;
        }


        protected virtual void Start()
        {
             
        }

        protected virtual void Update()
        {
            
            Vector3 worldPos = transform.position;
            worldPos = Vector3.SmoothDamp(worldPos, CameraPosition, ref m_CurrentCamVelocity,
                                          movementDampFactor);

            transform.position = worldPos;
            transform.LookAt(CurrentLookPosition);
            
       
        }
 
        /// <summary>
        /// Debug bounds area gizmo
        /// </summary>
        void OnDrawGizmos()
        {
            
            if (CachedCamera == null)
            {
                CachedCamera = GetComponent<UnityEngine.Camera>();
            }
     
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(trackedObjBoundingSphere.position, trackedObjBoundingSphere.radius);
            
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(transform.position, CurrentLookPosition);
            Gizmos.DrawSphere(lastPointChecked, 1f);
             
        }

        Vector3 lastPointChecked;
        public override Vector3 GetRaycastWorldPointOnTargetSurface(Vector2 screenPoint, Vector3? defaultValue)
        {
            Vector3 worldScreenPoint = CachedCamera.ScreenToWorldPoint(screenPoint);
            Ray center_ray = new Ray(trackedObjBoundingSphere.position, worldScreenPoint- trackedObjBoundingSphere.position);
            lastPointChecked = center_ray.GetPoint(CurrentZoomDistance);
            return lastPointChecked;
        }
 

        public override void StopTracking()
        {
            
        }
    }
}
