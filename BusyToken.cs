using System;

namespace NobleMuffins.Busy
{
	internal class BusyToken: IDisposable
	{
		private readonly Action<BusyToken> onDispose;
		public readonly string description;

		public BusyToken (Action<BusyToken> onDispose, string description = null)
		{
			this.onDispose = onDispose;
			this.description = description;
		}

		#region IDisposable implementation
		public void Dispose ()
		{
			onDispose (this);
		}
		#endregion
	}
}

