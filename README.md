# Busyness Kit
Busyness Kit is intended to solve the curtain problem when dealing with loading indicators in an interface.

Busyness Kit provides a thread-safe class responsible for knowing if any of its clients are currently busy doing something.

Suppose we have more than one async method where some sort of long running operation occurs, of the following sort:

	async Task DoStuff() {
		derp = await Stuff();
	}

We want to bind the busy indicators in our view graph to some particular thing which is always correct. We'll make a busy token source, like so:

	readonly BusyTokenSource busyTokenSource = new BusyTokenSource();

Then we'll modify our methods like so:

	async Task DoStuff() {
		using(busyTokenSource.GetToken()) {
			derp = await Stuff();
		}
	}

We can then bind the busy indicators in our user interface to the busy token source.

If we're where we're binding the UI by polling, we can pull the current state at any time like so:

	busyIndicator.IsVisible = busyTokenSource.IsAnythingBusy;

We can also _subscribe_ to events like so:

	busyTokenSource.StateChanged += (source, busynessEventArgs) { … }
