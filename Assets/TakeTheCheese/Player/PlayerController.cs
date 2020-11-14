using Assets.Core.Camera;
using Assets.Core.Input;
using Assets.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TakeTheCheese.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
		FollowCameraRig rig;
		
		public event Action jumpEvent;
        public event Action shootEvent;
		public float speed = 3f;
		private Vector3 movement;
		private Rigidbody playerRigidbody;
		private int floorMask;
		private float camRayLength = 100f;

		void Awake()
        {
            base.Awake();
			

		}
        void Start()
        {
			playerRigidbody = GetComponent<Rigidbody>();
			PlayerInputController.instance.mouseMoved += mouseMove;
			PlayerInputController.instance.axisMoved += movementInput;
			rig = Camera.main.gameObject.GetComponent<FollowCameraRig>();
			rig.StartTracking(this.gameObject);
		}

        private void Update()
        {
            
        }

		void movementInput(MovemenAxisInfo info)
        {
			
			Vector3 f = rig.GetForwardVector();
			f.y = 0f;
			Vector3 r = rig.GetRightVector();
			r.y = 0f;
			f.Normalize();
			r.Normalize();
			movement = f * info.verticalValue + r * info.horizontalValue;
			movement = movement.normalized * speed * Time.deltaTime;
			
			playerRigidbody.MovePosition(transform.position + movement);
		}
		void shoot()
        {

        }
		void mouseMove(PointerInfo info)
		{

			Vector3 floorPos = rig.GetRaycastWorldPointOnTargetSurface(info.currentPosition, transform.position);
			Turning(floorPos);

		}
		void Turning(Vector3 turnTwards)
		{ 
			Vector3 playerToMouse = turnTwards - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);	
		}

	}
}
