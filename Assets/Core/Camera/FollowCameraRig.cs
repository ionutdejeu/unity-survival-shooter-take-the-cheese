using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace Assets.Core.Camera
{
    
    public class FollowCameraRig:CameraRig
    {
        public float smoothing = 5f;
        public GameObject targetToFollow;
        private Plane groundPlane;

        private Quaternion InitialRotation;

        void Awake()
        {
            CachedCamera = GetComponent<UnityEngine.Camera>();
            groundPlane = new Plane(Vector3.up, targetToFollow.transform.position);
            TrackedObject = targetToFollow;
            InitialRotation = Quaternion.Euler(45f, 45f, 0);
            transform.SetPositionAndRotation(transform.position, InitialRotation);
            CameraPosition = CachedCamera.transform.position;

            Ray lookRay = new Ray(CachedCamera.transform.position, CachedCamera.transform.forward);
            float dist;
            if (groundPlane.Raycast(lookRay, out dist))
            {
                CurrentLookPosition = LookPosition = lookRay.GetPoint(dist);
            }
            RawCurrentZoomDistance = CurrentZoomDistance = 5f;
            StartTracking(TrackedObject);
        }

        Vector3 GetCameraLookPosition()
        {
            Vector3 lookPos = Vector3.zero;
            Ray lookRay = new Ray(CachedCamera.transform.position, CachedCamera.transform.forward);
            float dist;
            if (groundPlane.Raycast(lookRay, out dist))
            {
                lookPos = lookRay.GetPoint(dist);
            }

            return lookPos;
        }

        void FixedUpdate()
        {
            // Approach look position
            Vector3 prevLookPos = CurrentLookPosition;
            CurrentLookPosition = Vector3.SmoothDamp(CurrentLookPosition, LookPosition, ref m_CurrentLookVelocity,
                                                     lookDampFactor);
            Vector3 offset = CurrentLookPosition- prevLookPos;
            Debug.DrawRay(CurrentLookPosition, offset, Color.green, 10000f);

            CameraPosition += offset;
            CachedCamera.transform.position = CameraPosition;
           
           
        }

        
        public override Vector3 GetRaycastWorldPointOnTargetSurface(Vector2 screenPoint, Vector3? defaultValue)
        {
            Ray r = CachedCamera.ScreenPointToRay(screenPoint);
          
            Vector3 result = defaultValue.HasValue ? defaultValue.Value : Vector3.zero;

            float dist;

            if (groundPlane.Raycast(r, out dist))
            {
                result = r.GetPoint(dist);
            }
            return result;
        }
        Vector3 GetVectorFromPlaneToCamera()
        {
            return InitialRotation * Vector3.up;
        }

        
        
        public override void PanCamera(Vector3 panDelta)
        {
            LookPosition += panDelta;
        }

        public override void PanTo(Vector3 position)
        {
            LookPosition = position;
        }

        public override void StopTracking()
        {  
        }

        public void StartTracking(GameObject gO)
        {
            targetToFollow = gO;
            PanTo(gO.transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(CurrentLookPosition, 1f);
            //Gizmos.DrawSphere(groundPlane.ClosestPointOnPlane(targetToFollow.transform.position), 1f);
        }
    }
}
