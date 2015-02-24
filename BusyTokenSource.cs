using System;
using System.Collections.Generic;

namespace NobleMuffins.Busy
{
	public class BusyTokenSource
	{
		private readonly LinkedList<BusyToken> liveTokens = new LinkedList<BusyToken>();

		public bool IsBusy {
			get {
				lock (liveTokens) {
					return liveTokens.Count > 0;
				}
			}
		}

		public string Description {
			get {
				string description = null;
				if (IsBusy) {
					lock (liveTokens) {
						description = liveTokens.Last.Value.description;
					}
				}
				return description;
			}
		}

		private void HandleTokenDisposal(BusyToken token)
		{
			lock (liveTokens) {
				liveTokens.Remove (token);
			}
		}

		public IDisposable GetToken(string description = null)
		{
			var token = new BusyToken (HandleTokenDisposal, description);
			lock (liveTokens) {
				liveTokens.AddLast (token);
			}
			return token;
		}
	}
}

