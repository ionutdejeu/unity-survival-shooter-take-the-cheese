using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Input
{
    /// <summary>
    /// Info for mouse
    /// </summary>
    public class MouseButtonInfo: PointerActionInfo
    {
        /// <summary>
        /// Is the mouse button down
        /// </summary>
        public bool isDown;
        /// <summary>
        /// Unique identier for mouse button
        /// </summary>
        public int mouseButtonId;
    }
}
