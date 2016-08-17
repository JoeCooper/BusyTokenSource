using System;

namespace NobleMuffins.Busy
{
	public class BusynessEventArgs: EventArgs
	{
		public BusynessEventArgs(bool isBusy)
		{
			IsBusy = isBusy;
		}

		public bool IsBusy { get; private set; }
	}
}

