using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Input
{
	/// <summary>
	/// Class to track information about a passive pointer input
	/// </summary>
	public class PointerInfo
    {
		/// <summary>
		/// Current pointer position
		/// </summary>
		public Vector2 currentPosition;

		/// <summary>
		/// Previous frame's pointer position
		/// </summary>
		public Vector2 previousPosition;

		/// <summary>
		/// Movement delta for this frame
		/// </summary>
		public Vector2 delta;

		/// <summary>
		/// Tracks if this pointer began over UI
		/// </summary>
		public bool startedOverUI;
	}
}
