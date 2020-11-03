using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.UI
{
    /// <summary>
    /// Basic class for simple main menu page that just turns or off 
    /// </summary>
    public class SimpleMainMenuPage : MonoBehaviour, IMainMenuPage
    {
        /// <summary>
        /// Specific canvas to disable. If this obj is set it's going to be disabled enabled insted of the gameObject 
        /// </summary>
        public Canvas canvas;
        public void Hide()
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

        public void Show()
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
    }
}
