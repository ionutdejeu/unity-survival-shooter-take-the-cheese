using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Camera
{
    public class PlaneCameraRig : CameraRig
    {
        public float floorY;

        public Transform zoomCamAngles;
        /// <summary>
        /// Map size in world coordinates 
        /// </summary>
        
        [HideInInspector]
        public Rect mapSize = new Rect(-10, -10, 20, 20);
        public bool sprinyZoom = false;
        

        Quaternion m_MinZoomRotation;
        Quaternion m_MaxZoomRotation;

        Plane m_FloorPlane;

        public Plane FloorPlane
        {
            get { return m_FloorPlane; }
        }

        protected virtual void Awake()
        {
            
            CachedCamera = GetComponent<UnityEngine.Camera>();
            m_FloorPlane = new Plane(Vector3.up, new Vector3(0f, floorY, 0f));

            Ray lookRay = new Ray(CachedCamera.transform.position, CachedCamera.transform.forward);
            float dist;
            if(m_FloorPlane.Raycast(lookRay,out dist))
            {
                CurrentLookPosition = LookPosition = lookRay.GetPoint(dist);
            }
            CameraPosition = CachedCamera.transform.position;
            m_MinZoomRotation = Quaternion.FromToRotation(Vector3.up, -CachedCamera.transform.forward);
            m_MaxZoomRotation = Quaternion.FromToRotation(Vector3.up, -zoomCamAngles.transform.forward);

            RawCurrentZoomDistance = CurrentZoomDistance = (CurrentLookPosition - CameraPosition).magnitude;
        }

       
         
         

        public override void SetZoom(float newZoom)
        {
            if (sprinyZoom)
            {
                RawCurrentZoomDistance = newZoom;

                if (newZoom > furthestZoom)
                {
                    CurrentZoomDistance = furthestZoom;
                    CurrentZoomDistance += Mathf.Log((Mathf.Min(CurrentZoomDistance, maxZoom) - furthestZoom) + 1, zoomLogFactor);
                }
                else if (RawCurrentZoomDistance < nearestZoom)
                {
                    CurrentZoomDistance = nearestZoom;
                    CurrentZoomDistance -= Mathf.Log((nearestZoom - CurrentZoomDistance) + 1, zoomLogFactor);
                }
                else
                {
                    CurrentZoomDistance = RawCurrentZoomDistance;
                }
            }
            else
            {
                CurrentZoomDistance = RawCurrentZoomDistance = Mathf.Clamp(newZoom, nearestZoom, furthestZoom);
            }

            // Update bounding rectangle, which is based on our zoom level
            RecalculateBoundingRect();

            // Force recalculated CameraPosition
            PanCamera(Vector3.zero);
        }

        public override void PanTo(Vector3 position)
        {
            Vector3 pos = position;

            // Look position is floor height
            pos.y = floorY;

            // Clamp to look bounds
            pos.x = Mathf.Clamp(pos.x, LookBounds.xMin, LookBounds.xMax);
            pos.z = Mathf.Clamp(pos.z, LookBounds.yMin, LookBounds.yMax);
            LookPosition = pos;

            // Camera position calculated from look position with view vector and zoom dist
            CameraPosition = LookPosition + (GetToCamVector() * CurrentZoomDistance);
        }

        public override void PanCamera(Vector3 panDelta)
        {
            Vector3 pos = LookPosition;
            pos += panDelta;

            pos.x = Mathf.Clamp(pos.x, LookBounds.xMin, LookBounds.xMax);
            pos.y = Mathf.Clamp(pos.y, LookBounds.yMin, LookBounds.yMax);
            LookPosition = pos;

            CameraPosition = LookPosition + (GetToCamVector() * CurrentZoomDistance);

        }



        Vector3 GetToCamVector()
        {
            float t = Mathf.Clamp01((CurrentZoomDistance- nearestZoom) / (furthestZoom - nearestZoom));
            t = 1 - ((1 - t) * (1 - t));
            Quaternion interpolatedRotation = Quaternion.Slerp(
                m_MaxZoomRotation, m_MinZoomRotation,
                t);
            return interpolatedRotation * Vector3.up;
        }
        Vector3 GetToCamVector(
            float currZoomDist,
            float nearestZoomDist,
            float furthersZoomDist,
            Quaternion minZoomRotation,
            Quaternion maxZoomRotation)
        {
            float t = Mathf.Clamp01((currZoomDist - nearestZoomDist) / (furthersZoomDist - currZoomDist));
            t = 1 - ((1 - t) * (1 - t));
            Quaternion interpolatedRoation = Quaternion.Slerp(maxZoomRotation, minZoomRotation, t);

            return interpolatedRoation* Vector3.up;
        }

        
        void RecalculateBoundingRect()
        {
            Rect mapsize = mapSize;
            // Get some world space projections at this zoom level
            // Temporarily move camera to final look position
            Vector3 prevCameraPos = transform.position;
            transform.position = CameraPosition;
            transform.LookAt(LookPosition);

            // Project screen corners and center
            var bottomLeftScreen = new Vector3(0, 0);
            var topLeftScreen = new Vector3(0, Screen.height);
            var centerScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);

            Vector3 bottomLeftWorld = Vector3.zero;
            Vector3 topLeftWorld = Vector3.zero;
            Vector3 centerWorld = Vector3.zero;
            float dist;

            Ray ray = CachedCamera.ScreenPointToRay(bottomLeftScreen);
            if (m_FloorPlane.Raycast(ray, out dist))
            {
                bottomLeftWorld = ray.GetPoint(dist);
            }

            ray = CachedCamera.ScreenPointToRay(topLeftScreen);
            if (m_FloorPlane.Raycast(ray, out dist))
            {
                topLeftWorld = ray.GetPoint(dist);
            }

            ray = CachedCamera.ScreenPointToRay(centerScreen);
            if (m_FloorPlane.Raycast(ray, out dist))
            {
                centerWorld = ray.GetPoint(dist);
            }

            Vector3 toTopLeft = topLeftWorld - centerWorld;
            Vector3 toBottomLeft = bottomLeftWorld - centerWorld;

            LookBounds = new Rect(
                mapsize.xMin - toBottomLeft.x,
                mapsize.yMin - toBottomLeft.z,
                Mathf.Max(mapsize.width + (toBottomLeft.x * 2), 0),
                Mathf.Max((mapsize.height - toTopLeft.z) + toBottomLeft.z, 0));

            // Restore camera position
            transform.position = prevCameraPos;
            transform.LookAt(CurrentLookPosition);
        }

        /// <summary>
		/// Cause the camera to follow a unit
		/// </summary>
		/// <param name="objectToTrack"></param>
		public void TrackObject(GameObject objectToTrack)
        {
            TrackedObject = objectToTrack;
            PanTo(TrackedObject.transform.position);
        }

        

        protected virtual void Start()
        {
            RecalculateBoundingRect();
        }

        protected virtual void Update()
        {
            RecalculateBoundingRect();

            // Tracking?
            if (TrackedObject != null)
            {
                PanTo(TrackedObject.transform.position);

                if (!TrackedObject.activeInHierarchy)
                {
                    StopTracking();
                }
            }

            // Approach look position
            CurrentLookPosition = Vector3.SmoothDamp(CurrentLookPosition, LookPosition, ref m_CurrentLookVelocity,
                                                     lookDampFactor);

            Vector3 worldPos = transform.position;
            worldPos = Vector3.SmoothDamp(worldPos, CameraPosition, ref m_CurrentCamVelocity,
                                          movementDampFactor);

            transform.position = worldPos;
            transform.LookAt(CurrentLookPosition);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Debug bounds area gizmo
        /// </summary>
        void OnDrawGizmosSelected()
        {
            // We dont want to display this in edit mode
            if (!Application.isPlaying)
            {
                return;
            }
            if (CachedCamera == null)
            {
                CachedCamera = GetComponent<UnityEngine.Camera>();
            }
            RecalculateBoundingRect();

            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                new Vector3(LookBounds.xMin, 0.0f, LookBounds.yMin),
                new Vector3(LookBounds.xMax, 0.0f, LookBounds.yMin));
            Gizmos.DrawLine(
                new Vector3(LookBounds.xMin, 0.0f, LookBounds.yMin),
                new Vector3(LookBounds.xMin, 0.0f, LookBounds.yMax));
            Gizmos.DrawLine(
                new Vector3(LookBounds.xMax, 0.0f, LookBounds.yMax),
                new Vector3(LookBounds.xMin, 0.0f, LookBounds.yMax));
            Gizmos.DrawLine(
                new Vector3(LookBounds.xMax, 0.0f, LookBounds.yMax),
                new Vector3(LookBounds.xMax, 0.0f, LookBounds.yMin));

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(transform.position, CurrentLookPosition);
        }



#endif
        public override Vector3 GetRaycastWorldPointOnTargetSurface(Vector2 screenPoint, Vector3? defaultValue)
        {
            Ray currRay = CachedCamera.ScreenPointToRay(screenPoint);

            Vector3 result = defaultValue.HasValue ? defaultValue.Value : Vector3.zero;

            float dist;

            if (FloorPlane.Raycast(currRay, out dist))
            {
                result = currRay.GetPoint(dist);
            }
            return result;
        }

        public override void StopTracking()
        {
            TrackedObject = null;  
        }
    }
}
