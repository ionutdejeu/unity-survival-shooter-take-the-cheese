using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Input
{
    public class InputSchemeSwitcher: MonoBehaviour
    {
        protected InputScheme[] m_InputSchemes;
        protected InputScheme m_CurrentScheme;
        protected InputScheme m_DefaultScheme;
        protected virtual void Start()
        {
            m_InputSchemes = GetComponents<InputScheme>();
            foreach(InputScheme schem in m_InputSchemes)
            {
                schem.Deactivate(null);
                if (m_CurrentScheme == null && schem.IsDefault)
                {
                    m_DefaultScheme = schem;
                }
            }
            if (m_DefaultScheme == null)
            {
                Debug.LogError("[InputSchemeSwitcher] Default scheme not set.");
                return;
            }
            m_DefaultScheme.Activate(null);
            m_CurrentScheme = m_DefaultScheme;

        }

        protected virtual void Update()
        {
            foreach (InputScheme schem in m_InputSchemes)
            {
                if (schem.enabled || !schem.ShouldActivate)
                {
                    continue;
                }
                if (m_CurrentScheme != null)
                {
                    m_CurrentScheme.Deactivate(schem);
                }
                schem.Activate(m_CurrentScheme);
                m_CurrentScheme = schem;
                break;
            }
        }
    }
}
