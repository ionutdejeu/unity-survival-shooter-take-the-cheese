using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Input
{
    public abstract class InputScheme : MonoBehaviour
    {
        public abstract bool ShouldActivate { get; }
        public abstract bool IsDefault { get; }

        public virtual void Activate(InputScheme prev)
        {
            if (!enabled)
            {
                enabled = true;
            }
        }

        public virtual void Deactivate(InputScheme nextSchem)
        {
            if (enabled)
            {
                enabled = false;
            }
        }

    }
}
