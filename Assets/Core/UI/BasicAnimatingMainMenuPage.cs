using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.UI
{
    public class BasicAnimatingMainMenuPage : AnimatingMainMenuPage
    {
        /// <summary>
        /// Simply just finalizez the animation
        /// </summary>
        protected override void BeginDeactivationPage()
        {
            FinishedActivatingPage();
        }

        /// <summary>
        /// Empty method for this simple page 
        /// </summary>
        protected override void FinishedActivatingPage()
        {
            
        }
    }
}
