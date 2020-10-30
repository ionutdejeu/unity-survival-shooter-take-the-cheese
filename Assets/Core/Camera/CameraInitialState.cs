using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Camera
{
    public class CameraInitialState: MonoBehaviour
    {
        public enum StartZoomMode
        {
            NoChange,
            FutherstZoom,
            NearestZoom
        }

        public StartZoomMode startZoomMode;
        public Transform initialLookAt;


        public virtual void Start()
        {
            var rig = GetComponent<CameraRig>();
            switch (startZoomMode)
            {
                case StartZoomMode.NoChange:
                    break;
                case StartZoomMode.FutherstZoom:
                    rig.SetZoom(rig.furthestZoom);
                    break;
                case StartZoomMode.NearestZoom:
                    rig.SetZoom(rig.nearestZoom);
                    break;
                default:
                    break;
            }
            if (initialLookAt != null)
            {
                rig.PanTo(initialLookAt.transform.position);
            }
        }
    }
}
