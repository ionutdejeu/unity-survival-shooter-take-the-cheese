using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Utilities
{
    public class Singleton<T>: MonoBehaviour where T: Singleton<T>
    {
        public static T instance { get; protected set; }
        public static bool instanceExists
        {
            get { return instance != null; }
        }
		 
		protected virtual void Awake()
		{
			if (instanceExists)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = (T)this;
			}
		}

		protected virtual void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}
	}
}
