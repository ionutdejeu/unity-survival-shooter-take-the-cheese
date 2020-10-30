using Assets.Core.Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Input
{
	public abstract class CameraInputScheme : InputScheme
	{
        public CameraRig cameraRig;
        public float nearZoomPanSpeedModifier = 0.2f;
        protected float GetPanSpeedForZoomLevel()
        {
            return cameraRig != null ?
                Mathf.Lerp(nearZoomPanSpeedModifier, 1, cameraRig.CalculateZoomRation()):
                1.0f;
        }

        protected void PanWithScreenCoordinates(Vector2 screenPos,float screenEdgeThreshold, float panSpeed)
        {
            // Calculate zoom ratio
            float zoomRatio = GetPanSpeedForZoomLevel();

            // Left
            if ((screenPos.x < screenEdgeThreshold))
            {
                float panAmount = (screenEdgeThreshold - screenPos.x) / screenEdgeThreshold;
                panAmount = Mathf.Clamp01(Mathf.Log(panAmount) + 1);

                if (cameraRig.TrackedObject == null)
                {
                    cameraRig.PanCamera(Vector3.left * Time.deltaTime * panSpeed * panAmount * zoomRatio);

                    cameraRig.StopTracking();
                }
            }

			// Right
			if ((screenPos.x > Screen.width - screenEdgeThreshold))
			{
				float panAmount = ((screenEdgeThreshold - Screen.width) + screenPos.x) / screenEdgeThreshold;
				panAmount = Mathf.Clamp01(Mathf.Log(panAmount) + 1);

				if (cameraRig.TrackedObject == null)
				{
					cameraRig.PanCamera(Vector3.right * Time.deltaTime * panSpeed * panAmount * zoomRatio);
				}
				cameraRig.StopTracking();
			}

			// Down
			if ((screenPos.y < screenEdgeThreshold))
			{
				float panAmount = (screenEdgeThreshold - screenPos.y) / screenEdgeThreshold;
				panAmount = Mathf.Clamp01(Mathf.Log(panAmount) + 1);

				if (cameraRig.TrackedObject == null)
				{
					cameraRig.PanCamera(Vector3.back * Time.deltaTime * panSpeed * panAmount * zoomRatio);

					cameraRig.StopTracking();
				}
			}

			// Up
			if ((screenPos.y > Screen.height - screenEdgeThreshold))
			{
				float panAmount = ((screenEdgeThreshold - Screen.height) + screenPos.y) / screenEdgeThreshold;
				panAmount = Mathf.Clamp01(Mathf.Log(panAmount) + 1);

				if (cameraRig.TrackedObject== null)
				{
					cameraRig.PanCamera(Vector3.forward * Time.deltaTime * panSpeed * panAmount * zoomRatio);

					cameraRig.StopTracking();
				}
			}
		}
    }
}
