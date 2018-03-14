using System;

namespace Busyness
{
	public class BusynessEventArgs : EventArgs
	{
		public BusynessEventArgs(bool isBusy)
		{
			IsBusy = isBusy;
		}

		public bool IsBusy { get; private set; }
	}
}

