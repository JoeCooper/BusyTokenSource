using System;
using System.Collections.Generic;
using System.Linq;

namespace Busyness
{
	public class BusyTokenSource
	{
		readonly ICollection<BusyToken> liveTokens = new LinkedList<BusyToken>();

		public event EventHandler<BusynessEventArgs> StateChanged;

		public bool IsAnythingBusy
		{
			get
			{
				lock (liveTokens)
				{
					return liveTokens.Count > 0;
				}
			}
		}

		public string Description
		{
			get
			{
				lock (liveTokens)
				{
					return liveTokens.Select(t => t.description).LastOrDefault(s => s != null) ?? string.Empty;
				}
			}
		}

		void HandleTokenDisposal(BusyToken token)
		{
			lock (liveTokens)
			{
				liveTokens.Remove(token);
				RaiseStateChanged(IsAnythingBusy);
			}
		}

		void RaiseStateChanged(bool isBusy)
		{
			StateChanged?.Invoke(this, new BusynessEventArgs(isBusy));
		}

		public IDisposable GetToken(string description = null)
		{
			var token = new BusyToken(HandleTokenDisposal, description);
			lock (liveTokens)
			{
				liveTokens.Add(token);
				RaiseStateChanged(IsAnythingBusy);
			}
			return token;
		}
	}
}

