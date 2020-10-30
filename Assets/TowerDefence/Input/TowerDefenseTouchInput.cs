using Assets.Core.Input;
using Assets.TowerDefence.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TowerDefence.Input
{
	[RequireComponent(typeof(GameUI))]
	public class TowerDefenseTouchInput : TouchInput
	{
		/// <summary>
		/// A percentage of the screen where panning occurs while dragging
		/// </summary>
		[Range(0, 0.5f)]
		public float panAreaScreenPercentage = 0.2f;

		 
		/// <summary>
		/// The attached Game UI object
		/// </summary>
		GameUI m_GameUI;

		/// <summary>
		/// Keeps track of whether or not the ghost tower is selected
		/// </summary>
		bool m_IsGhostSelected;

		/// <summary>
		/// The pointer at the edge of the screen
		/// </summary>
		TouchInfo m_DragPointer;

		/// <summary>
		/// Called by the confirm button on the UI
		/// </summary>
		public void OnTowerPlacementConfirmation()
		{
			 
			 
		}

		/// <summary>
		/// Called by the close button on the UI
		/// </summary>
		public void Cancel()
		{
			 
		}

		/// <summary>
		/// Register input events
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();

			m_GameUI = GetComponent<GameUI>();

			 

			// Register tap event
			if (InputController.instanceExists)
			{
				InputController.instance.tapped += OnTap;
				InputController.instance.startedDrag += OnStartDrag;
			}

			 

		}

		/// <summary>
		/// Deregister input events
		/// </summary>
		protected override void OnDisable()
		{
			base.OnDisable();

			 
			if (InputController.instanceExists)
			{
				InputController.instance.tapped -= OnTap;
				InputController.instance.startedDrag -= OnStartDrag;
			}
			 
		}

		/// <summary>
		/// Hide UI 
		/// </summary>
		protected virtual void Awake()
		{
			 
		}

		/// <summary>
		/// Decay flick
		/// </summary>
		protected override void Update()
		{
			base.Update();

			// Edge pan
			if (m_DragPointer != null)
			{
				EdgePan();
			}

			 
		}

		/// <summary>
		/// Called on input press
		/// </summary>
		protected override void OnPress(PointerActionInfo pointer)
		{
			base.OnPress(pointer);
			var touchInfo = pointer as TouchInfo;
			// Press starts on a ghost? Then we can pick it up
			if (touchInfo != null)
			{
				 
			}
		}

		/// <summary>
		/// Called on input release, for flicks
		/// </summary>
		protected override void OnRelease(PointerActionInfo pointer)
		{
			// Override normal behaviour. We only want to do flicks if there's no ghost selected
			// For this reason, we intentionally do not call base
			var touchInfo = pointer as TouchInfo;

			if (touchInfo != null)
			{
				 
			}
		}

		/// <summary>
		/// Called on tap,
		/// calls confirmation of tower placement
		/// </summary>
		protected virtual void OnTap(PointerActionInfo pointerActionInfo)
		{
			var touchInfo = pointerActionInfo as TouchInfo;
			if (touchInfo != null)
			{
				 
			}
		}

		/// <summary>
		/// Assigns the drag pointer and sets the UI into drag mode
		/// </summary>
		/// <param name="pointer"></param>
		protected virtual void OnStartDrag(PointerActionInfo pointer)
		{
			var touchInfo = pointer as TouchInfo;
			if (touchInfo != null)
			{
				 
			}
		}


		/// <summary>
		/// Called when we drag
		/// </summary>
		protected override void OnDrag(PointerActionInfo pointer)
		{
			// Override normal behaviour. We only want to pan if there's no ghost selected
			// For this reason, we intentionally do not call base
			var touchInfo = pointer as TouchInfo;
			if (touchInfo != null)
			{
				// Try to pick up the tower if it was dragged off
				 
			}
		}

		/// <summary>
		/// Drags the ghost
		/// </summary>
		void DragGhost(TouchInfo touchInfo)
		{
			if (touchInfo.touchId == m_DragPointer.touchId)
			{
				 
			}
		}

		/// <summary>
		/// pans at the edge of the screen
		/// </summary>
		void EdgePan()
		{
			float edgeWidth = panAreaScreenPercentage * Screen.width;
			PanWithScreenCoordinates(m_DragPointer.currentPosition, edgeWidth, panSpeed);
		}


		/// <summary>
		/// If the new state is <see cref="GameUI.State.Building"/> then move the ghost to the center of the screen
		/// </summary>
		/// <param name="previousState">
		/// The previous the GameUI was is in
		/// </param>
		/// <param name="currentState">
		/// The new state the GameUI is in
		/// </param>
		 
		/// <summary>
		/// Displays the correct confirmation buttons when the tower has become valid
		/// </summary>
		 
	}
}
