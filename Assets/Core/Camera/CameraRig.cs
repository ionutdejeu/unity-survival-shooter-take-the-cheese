using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Camera
{
    public abstract class CameraRig : MonoBehaviour
    {
        public float lookDampFactor;
        public float movementDampFactor;
        public float nearestZoom = 15;
        public float furthestZoom = 40;
        public float maxZoom = 60;
        protected Vector3 m_CurrentLookVelocity;
        protected Vector3 m_CurrentCamVelocity;
        /// <summary>
        /// Factor for zoom decay beyound furthest
        /// </summary>
        public float zoomLogFactor = 10;
        /// <summary>
        /// How fast zoom reovers to normal
        /// </summary>
        public float zoomRecoveryFactor = 20;
        public bool springyZoom = false;

        public Vector3 LookPosition { get; protected set; }
        public Vector3 CurrentLookPosition { get; protected set; }

        public Vector3 CameraPosition { get; protected set; }

        public Rect LookBounds { get; protected set; }

        public float CurrentZoomDistance { get; protected set; }
        public float RawCurrentZoomDistance { get; protected set; }

        public GameObject TrackedObject { get; protected set; }

        public UnityEngine.Camera CachedCamera { get; protected set; }

        public void ZoomDecay()
        {
            if (springyZoom)
            {
                if (RawCurrentZoomDistance > furthestZoom)
                {
                    float recover = RawCurrentZoomDistance - furthestZoom;
                    SetZoom(Mathf.Max(furthestZoom, RawCurrentZoomDistance - (recover * zoomRecoveryFactor * Time.deltaTime)));
                }
                else if (RawCurrentZoomDistance < nearestZoom)
                {
                    float recover = nearestZoom - RawCurrentZoomDistance;
                    SetZoom(Mathf.Min(nearestZoom, RawCurrentZoomDistance + (recover * zoomRecoveryFactor * Time.deltaTime)));
                }
            }
        }
        public void ZoomCameraRelative(float zoomDelta)
        {
            SetZoom(RawCurrentZoomDistance + zoomDelta);
        }
        /// <summary>
        /// Compute the zoom leve based on NearestZoom, FurthestZoom and Current Zoom level
        /// </summary>
        /// <returns>Clamped Zoom level</returns>
        public float CalculateZoomRation()
        {
            return Mathf.Clamp01(Mathf.InverseLerp(nearestZoom, furthestZoom, CurrentZoomDistance));
        }
        public Vector3 GetScreenPosition(Vector3 worldPosition)
        {
            return CachedCamera.WorldToScreenPoint(worldPosition);
        }
        /// <summary>
        /// Stop tracking a unit
        /// </summary>
        
        

        
        public virtual void SetZoom(float newZoom)
        {
            if (springyZoom)
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
        }


        public Vector3 GetUpVector()
        {
            return transform.up;
        }

        public Vector3 GetRightVector()
        {
            return transform.right;
        }

        public Vector3 GetDownVector()
        {
            return -transform.up;
        }

        public Vector3 GetLeftVector()
        {
            return -transform.right;
        }
        public abstract void StopTracking();
        public abstract void PanCamera(Vector3 panDelta);
        public abstract void PanTo(Vector3 position);
        public abstract Vector3 GetRaycastWorldPointOnTargetSurface(Vector2 screenPoint, Vector3? defaultValue);

        
    }
}
