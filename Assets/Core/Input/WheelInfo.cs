﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Input
{
	/// <summary>
	/// Information about a zoom action (usually mouse-wheel or button based)
	/// </summary>
	public struct WheelInfo
	{
		/// <summary>
		/// Amount of zoom
		/// </summary>
		public float zoomAmount;
	}
}
