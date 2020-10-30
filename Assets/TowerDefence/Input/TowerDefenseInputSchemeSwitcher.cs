using Assets.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.TowerDefence.Input
{
    public class TowerDefenseInputSchemeSwitcher : InputSchemeSwitcher
    {
		/// <summary>
		/// Gets whether the game is in a paused state
		/// </summary>
		public bool isPaused
		{
			get { return false; }
		}

		/// <summary>
		/// Register GameUI's stateChanged event
		/// </summary>
		protected virtual void Start()
		{
			 
		}

		/// <summary>
		/// Do nothing when game is paused
		/// </summary>
		protected override void Update()
		{
			if (isPaused)
			{
				return;
			}

			base.Update();
		}

		/// <summary>
		/// Unregister from GameUI's stateChanged event
		/// </summary>
		protected virtual void OnDestroy()
		{
			 
		}
	}
}
