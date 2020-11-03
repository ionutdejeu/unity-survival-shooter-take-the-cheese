using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.UI
{
    /// <summary>
    /// Abstract base class for menu pages that has animations process 
    /// Animation are used for showing and hiding 
    /// This class handles activation / deactivation of menu pages 
    /// </summary>
    public abstract class AnimatingMainMenuPage : MonoBehaviour, IMainMenuPage
    {
        /// <summary>
		/// Canvas to disable. If this object is set, then the canvas is disabled instead of the game object 
		/// </summary>
		
        public Canvas canvas;

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        protected abstract void BeginDeactivationPage();

		/// <summary>
		/// Ends the deactivation process and turns off the associated gameObject/canvas
		/// </summary>
		protected virtual void FinishedDeactivatingPage()
		{
			if (canvas != null)
			{
				canvas.enabled = false;
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// Starts the activation process by turning on the associated gameObject/canvas.  Call FinishedActivatingPage when done
		/// </summary>
		protected virtual void BeginActivatingPage()
		{
			if (canvas != null)
			{
				canvas.enabled = true;
			}
			else
			{
				gameObject.SetActive(true);
			}
		}

		/// <summary>
		/// Finishes the activation process. e.g. Turning on input
		/// </summary>
		protected abstract void FinishedActivatingPage();

	}
}
