
using System;

namespace JobApplicationTracker.Practice
{
	public class Events
	{
		//my Noooootes_study for delegates :))

		// ==========================================
		// 1. Custom Delegate
		// ==========================================

		//// Defines the signature of subscriber methods => deleg
		//public delegate void StockPriceHandler(Stock stock, decimal oldPrice);


		// ==========================================
		// 2. Custom EventArgs
		// ==========================================

		// Stores data related to the event
		public class StockPriceChangedEventArgs : EventArgs
		{
			public decimal OldPrice { get; }
			public decimal NewPrice { get; }

			public StockPriceChangedEventArgs(
				decimal oldPrice,
				decimal newPrice)
			{
				OldPrice = oldPrice;
				NewPrice = newPrice;
			}
		}


		// ==========================================
		// 3. Publisher Class (Stock)
		// ==========================================

		public class Stock
		{
			private string name;
			private decimal price;

			//// Event using Custom Delegate
			//public event StockPriceHandler OnStockPriceChangedEvent;

			// Event using EventHandler => take (sender, event args) and this will use at invoke event and at signature for method that will subscribe
			// Event using EventHandler<TEventArgs>:
			// It follows the (sender, e) pattern.
			// These parameters are used when raising the event
			// and in the signature of the handler method that subscribes to it.
			public event EventHandler<StockPriceChangedEventArgs> OnStockPriceChangedEvent;

			public string Name => name;

			public decimal Price {get => price; set => price = value; }

			public Stock(string name)
			{
				this.name = name;
			}


			// ==========================================
			// 4. Event Occurrence + Raising Event
			// ==========================================

			public void ChangePriceBy(decimal percent)
			{
				decimal oldPrice = price;

				price += Math.Round(price * percent, 2);

				// Check if there are subscribers
					//OnStockPriceChangedEvent?.Invoke(this, oldPrice); // Raise Event using Custom Delegate

				// Alternative: EventHandler + EventArgs
				decimal newPrice = price;

                var args = new StockPriceChangedEventArgs(oldPrice, newPrice);

                OnStockPriceChangedEvent?.Invoke(this, args);
			}
		}


		// ==========================================
		// 5. Run: Create Object + Subscribe
		// ==========================================

		public static void Run()
		{
			Console.WriteLine("===== Events Practice =====");

			Stock stocky = new Stock("Apple");

			stocky.Price = 11000;

			//// Subscribe using Custom Delegate
			//stocky.OnStockPriceChangedEvent += Stocky_OnStockPriceChangedEvent;

			 //Alternative: Subscribe using EventHandler
			 stocky.OnStockPriceChangedEvent +=  Stocky_OnStockPriceChangedEvent1;

			// Change price (Event will be raised)
			stocky.ChangePriceBy(0.07m);
			stocky.ChangePriceBy(0.00m);
			stocky.ChangePriceBy(-0.03m);
		}


		// ==========================================
		// 6. Subscriber Method (Custom Delegate)
		// ==========================================

		private static void Stocky_OnStockPriceChangedEvent(Stock stock, decimal oldPrice)
		{
			if (stock.Price > oldPrice)
			{
				Console.ForegroundColor = ConsoleColor.Green;

				Console.WriteLine(
					$"Price of {stock.Name} increased " +
					$"from {oldPrice} to {stock.Price}"
				);
			}
			else if (stock.Price < oldPrice)
			{
				Console.ForegroundColor = ConsoleColor.Red;

				Console.WriteLine(
					$"Price of {stock.Name} decreased " +
					$"from {oldPrice} to {stock.Price}"
				);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Gray;

				Console.WriteLine(
					$"Price of {stock.Name} Not changed: {stock.Price}"
				);
			}

			Console.ResetColor();
		}


		// ==========================================
		// 7. Subscriber Method (EventHandler)
		// ==========================================

        private static void Stocky_OnStockPriceChangedEvent1(
            object? sender,
            StockPriceChangedEventArgs args)
        {
            if (sender is not Stock stock) //	Type Checking + Pattern Matching
				return;

			//Explicit Casting =>  Stock stock = (Stock)sender;

			if (args.NewPrice > args.OldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Price of {stock.Name} changed from {args.OldPrice} to {args.NewPrice}");

			}
			else if (args.NewPrice < args.OldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Price of {stock.Name} changed from {args.OldPrice} to {args.NewPrice}");

			}
			else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Price of {stock.Name} : {stock.Price} Not changed :)");

			}

		}

	}
}

