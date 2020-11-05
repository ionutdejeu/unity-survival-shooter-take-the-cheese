using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Currency
{
    public class Currency
    {
		/// <summary>
		/// Occurs when currency changed.
		/// </summary>
		public event Action currencyChanged;

		public int currentCurrency { get; protected set; }

		/// <summary>
		/// Adds the currency.
		/// </summary>
		/// <param name="increment">the change in currency</param>
		public void AddCurrency(int increment)
		{
			ChangeCurrency(increment);

		}

		/// <summary>
		/// Changes the currency.
		/// </summary>
		/// <param name="increment">the change in currency</param>
		protected void ChangeCurrency(int increment)
		{
			if (increment != 0)
			{
				currentCurrency += increment;
				if (currencyChanged != null)
				{
					currencyChanged();
				}
			}
		}
	}
}
