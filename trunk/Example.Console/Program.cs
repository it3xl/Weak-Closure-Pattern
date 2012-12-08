using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WeakClosureProject;

namespace Example.Console
{
	class Program
	{


		static void Main(string[] args)
		{
			var someState = new Object();

			//private static
			ObjectSupplier objectSupplierOfStrongReference = new ObjectSupplier();

			WeakClosure<ObjectSupplier> weakClosure = new WeakClosure<ObjectSupplier>(objectSupplierOfStrongReference);

			var someClosure_Dont_Do_So = new Object();


			var useFirstSample = trueValue;


			if (useFirstSample)
			{
				// The standart approach.
				ObjectRecipient.DoExternalWork(
					someState,
					() =>
						{
							// !!! It's important! Don't create any other closures here!
							// You can use only the weakObjectSupplier!
							//var forbidden_approach = someClosure_Dont_Do_So;


							Thread.Sleep(TimeSpan.FromSeconds(3));
							RunLoanMemory();
							Thread.Sleep(TimeSpan.FromSeconds(3));
							RunLoanMemory();
							Thread.Sleep(TimeSpan.FromSeconds(3));


							ObjectSupplier objectSupplier = weakClosure
								// You should understand what is the custom WeakReference{T} class at this project.
								.TargetTyped;

							if (objectSupplier == null)
							{
								return;
							}

							var someData = new Object();

							objectSupplier.DoSomeThing(someData);
						});


				// The infinite loop that you can break from a debugger. For test use.
				//var loop = true;
				//while (loop)
				//{
				//    Thread.Sleep(TimeSpan.FromSeconds(2));
				//}
			}
			else
			{
				// The little bit sophisticated approach.
				// It requires more deep knowledge of the WeakClosure from a developer.
				// But you shouldn't know what is the custom WeakReference{T} class at this project.
				ObjectRecipient.DoExternalWork(
					someState,
					() => weakClosure
						.ExecuteIfTargetNotNull(objectSupplier =>
					{
						// !!! It's important! Don't create any other closures here!
						// You can use only the ExecuteIfTargetNotNull method action parameter - objectSupplier at this sample!
						//var forbidden_approach = someClosure_Dont_Do_So;

						var someData = new Object();

						objectSupplier.DoSomeThing(someData);

					}));


				// The infinite loop that you can break from a debugger. For test use.
				//var loop = true;
				//while (loop)
				//{
				//    Thread.Sleep(TimeSpan.FromSeconds(2));
				//}
			}

		}

		public static void RunLoanMemory()
		{
			var load = new List<Int32[,]>();
			for (var i = 0; i < 10000; i++)
			{
				load.Add(new Int32[100, 100]);
			}
		}


		/// <summary>
		/// Cheating the Resharper field with the true value. For good code reading by the Resharper users :).
		/// If you delete it, then Resharper makes the second example not readable.
		/// </summary>
		private static Boolean trueValue = true;


	}

	/// <summary>
	/// The object that hold the Post-Action 
	/// </summary>
	public class ObjectSupplier
	{

		public void DoSomeThing(Object someData)
		{
			// Do some things here...
		}

	}

	public class ObjectRecipient
	{
		
		public static void DoExternalWork(Object someState, Action postAction)
		{
			Program.RunLoanMemory();

			Thread.Sleep(TimeSpan.FromSeconds(5));

			var th = new Thread(() =>
			    {
					Program.RunLoanMemory();

					Thread.Sleep(TimeSpan.FromSeconds(5));

					// Do some things here...

					// Execute the post-action when all are done.
					if (postAction != null)
					{
						postAction();
					}
			    });

			th.Start();
		}

	}





}
