
using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace JobApplicationTracker.Practice
{
	public class Delegates_LambdaExp
	{
		// =========================================
		// 1. Custom Delegates
		// =========================================

		public delegate void MyMessageDeleg(string msg);

		public delegate bool FilterNumbersDeleg(int num);

		public delegate void MultiCastDeleg(string notific);


		// =========================================
		// 2. Named Methods
		// =========================================

		// Custom Delegate Method
		private static void MyMessageMethod(string msg) => Console.WriteLine($"Somaya's Delegate says: {msg}"); //Exprision-Bodied Method

		// Filtering Methods
		private static bool IsEven(int num) => num % 2 == 0;

		private static bool IsOdd(int num) => num % 2 != 0;

		private static bool IsPositive(int num) => num > 0;

		private static bool IsNegative(int num) => num < 0;

		// Notification Methods
		private static void SMSNotification(string notific)
		{
			Console.WriteLine($"SMS: {notific}");
		}

		private static void EmailNotification(string notific)
		{
			Console.WriteLine($"Email: {notific}");
		}

		private static void WhatsAppNotification(string notific)
		{
			Console.WriteLine($"WhatsApp: {notific}");
		}


		// =========================================
		// 3. Delegate as a Parameter
		// =========================================

		private static List<int> FilterNumbers(List<int> numbers, FilterNumbersDeleg filter)
		{
			List<int> result = new();

			foreach (int num in numbers)
			{
				if (filter(num))
				{
					result.Add(num);
				}
			}

			return result;
		}


		// =========================================
		// 4. Helper Method to Print Lists
		// =========================================

		private static void PrintNumbers(string title, List<int> numbers)
		{
			Console.WriteLine($"\n{title}");

			foreach (int num in numbers)
			{
				Console.Write($"{num} ");
			}

			Console.WriteLine();
		}


		// =========================================
		// 5. D1 - Func<T, TResult>
		// =========================================

		private static void FuncPractice()
		{
			Console.WriteLine("\n========== Func Practice ==========");

			// 1. Square a number
			               //name = paramter => expression
			Func<int, int> square = number => number * number;

			Console.WriteLine($"Square of 5: {square(5)}");


			// 2. Add two numbers
			Func<int, int, int> add = (first, second) => first + second;

			Console.WriteLine($"Sum: {add(10, 20)}");


			// 3. Calculate annual salary after 14% tax
			Func<decimal, decimal> calculateAnnualSalary =
				monthlySalary =>
				{
					decimal annualSalary = monthlySalary * 12;

					decimal tax = annualSalary * 0.14m;

					return annualSalary - tax;
				};

			Console.WriteLine(
				$"Net Annual Salary: {calculateAnnualSalary(10000)}"
			);


			// 4. Func without parameters
			Func<string> getWelcomeMessage =
				() => "Welcome to Job Application Tracker!";

			Console.WriteLine(getWelcomeMessage());
		}


		// =========================================
		// 6. D2 - Action<T>
		// =========================================

		private static void ActionPractice()
		{
			Console.WriteLine("\n========== Action Practice ==========");

			// 1. Print candidate name
			Action<string> printCandidate = candidateName => Console.WriteLine($"Welcome {candidateName}");
			printCandidate("Somaya");


			// 2. Display job title and salary
			Action<string, decimal> displayJob =
				(title, salary) =>
				{
					Console.WriteLine($"Job Title: {title}");
					Console.WriteLine($"Salary: {salary}");
				};

			displayJob("Backend .NET Developer", 15000);


			// 3. Action without parameters
			Action welcome =
				() => Console.WriteLine("Welcome!");

			welcome();


			// 4. Formatted notification
			Action<string> printNotification = message =>
			{
				Console.WriteLine("------------------------------");
				Console.WriteLine($"Notification: {message}");
				Console.WriteLine("------------------------------");
			};

			printNotification("Application submitted successfully");
		}


		// =========================================
		// 7. D3 - Predicate<T>
		// =========================================

		private static void PredicatePractice()
		{
			Console.WriteLine("\n========== Predicate Practice ==========");

			// 1. Check if number is even
			Predicate<int> isEven =
				number => number % 2 == 0;

			Console.WriteLine($"Is 10 Even? {isEven(10)}");


			// 2. Check if age is 18 or above
			Predicate<int> isAdult =
				age => age >= 18;

			Console.WriteLine($"Is 20 Adult? {isAdult(20)}");


			// 3. Check if job is open
			Predicate<bool> isJobOpen =
				isOpen => isOpen;

			Console.WriteLine($"Is Job Open? {isJobOpen(true)}");


			// 4. Check candidate experience
			Predicate<int> hasExperience =
				years => years > 0;

			Console.WriteLine(
				$"Has Experience? {hasExperience(2)}"
			);


			// 5. Filter a list using Predicate
			List<int> numbers = new() { 1, 2, 5, 7, -11, 33, -7, 12, 6 };

			List<int> positiveNumbers = new();

			foreach (int number in numbers)
			{
				if (IsPositive(number))
				{
					positiveNumbers.Add(number);
				}
			}

			PrintNumbers("Positive Numbers:", positiveNumbers);
		}


		// =========================================
		// 8. D4 - Comparison<T>
		// =========================================

		private static void ComparisonPractice()
		{
			Console.WriteLine("\n========== Comparison Practice ==========");

			List<int> numbers = new()
			{
				50, 10, 40, 20, 30
			};


			// 1. Compare two numbers
			Comparison<int> compareNumbers =
				(first, second) => first.CompareTo(second);

			Console.WriteLine(
				$"Compare 10 and 20: {compareNumbers(10, 20)}"
			);


			// 2. Ascending order
			Comparison<int> ascending =
				(first, second) => first.CompareTo(second);

			List<int> ascendingNumbers = new(numbers);

			ascendingNumbers.Sort(ascending);

			PrintNumbers("Ascending Order:", ascendingNumbers);


			// 3. Descending order
			Comparison<int> descending =
				(first, second) => second.CompareTo(first);

			List<int> descendingNumbers = new(numbers);

			descendingNumbers.Sort(descending);

			PrintNumbers("Descending Order:", descendingNumbers);


			// 4. Sort job titles alphabetically
			List<string> jobTitles = new()
			{
				"Software Engineer",
				"Backend Developer",
				"Frontend Developer",
				"Data Analyst"
			};

			Comparison<string> compareTitles =
				(first, second) =>
					string.Compare(
						first,
						second,
						StringComparison.OrdinalIgnoreCase
					);

			List<string> sortedTitles = new(jobTitles);

			sortedTitles.Sort(compareTitles);

			Console.WriteLine("\nJobs Alphabetically:");

			foreach (string title in sortedTitles)
			{
				Console.WriteLine(title);
			}


			// Verify original list was not modified
			PrintNumbers("Original Numbers:", numbers);
		}


		// =========================================
		// 9. Lambda Expressions Practice
		// =========================================

		private static void LambdaPractice()
		{
			Console.WriteLine("\n========== Lambda Practice ==========");


			// 1. Expression Lambda
			Func<int, int> square =
				number => number * number;

			Console.WriteLine($"Expression Lambda: {square(4)}");


			// 2. Statement Lambda
			Func<int, int> calculate = number =>
			{
				int doubled = number * 2;

				int result = doubled + 10;

				return result;
			};

			Console.WriteLine($"Statement Lambda: {calculate(5)}");


			// 3. Multiple Parameters
			Func<int, int, int> add =
				(first, second) => first + second;

			Console.WriteLine($"Multiple Parameters: {add(10, 20)}");


			// 4. No Parameters
			Func<string> getMessage =
				() => "Learning Lambda Expressions";

			Console.WriteLine(getMessage());


			// 5. Type Inference
			Func<int, bool> isPositive =
				number => number > 0;

			Console.WriteLine($"Type Inference: {isPositive(15)}");


			// 6. Explicit Parameter Type
			Func<int, int> doubleNumber =
				(int number) => number * 2;

			Console.WriteLine($"Explicit Type: {doubleNumber(10)}");


			// 7. Named Method vs Lambda
			Func<int, int, int> namedMethod = Multiply;

			Func<int, int, int> anonymousMethod =
				(first, second) => first * second;

			Console.WriteLine($"Named Method: {namedMethod(5, 3)}");

			Console.WriteLine($"Lambda Method: {anonymousMethod(5, 3)}");


			// 8. Capturing an Outer Variable
			int taxPercentage = 14;

			Func<decimal, decimal> calculateTax =
				salary => salary * taxPercentage / 100m;

			Console.WriteLine($"Tax: {calculateTax(10000)}");
		}


		// Named Method used in LambdaPractice
		private static int Multiply(int first, int second)
		{
			return first * second;
		}


		// =========================================
		// 10. Run All Practices
		// =========================================

		public static void Run()
		{
			Console.WriteLine(
				"===== Delegates & Lambda Expressions Practice ====="
			);


			// Custom Delegate
			Console.WriteLine("\n========== Custom Delegate ==========");

			MyMessageDeleg myDelegate = MyMessageMethod;

			myDelegate("Welcome from Delegate!");

			myDelegate.Invoke("Welcome using Invoke!");


			// Delegate as Parameter
			Console.WriteLine("\n========== Delegate as Parameter ==========");

			List<int> numbers = new() { 1, 2, 5, 7, -11, 33, -7, 12, 6 };

			FilterNumbersDeleg filterEven = IsEven;

			FilterNumbersDeleg filterOdd = IsOdd;

			FilterNumbersDeleg filterPositive = IsPositive;

			FilterNumbersDeleg filterNegative = IsNegative;


			PrintNumbers("Even Numbers:", FilterNumbers(numbers, filterEven));

			PrintNumbers("Odd Numbers:", FilterNumbers(numbers, filterOdd));

			PrintNumbers("Positive Numbers:", FilterNumbers(numbers, filterPositive));

			PrintNumbers("Negative Numbers:", FilterNumbers(numbers, filterNegative));


			// Multicast Delegate
			Console.WriteLine("\n========== Multicast Delegate ==========");

			MultiCastDeleg multi = SMSNotification;

			multi += EmailNotification;

			multi += WhatsAppNotification;

			multi("New message from Somaya :)");


			// Built-in Delegates
			FuncPractice();

			ActionPractice();

			PredicatePractice();

			ComparisonPractice();

			LambdaPractice();


			Console.WriteLine(
				"\n===== All Practices Completed ====="
			);
		}
	}
}
