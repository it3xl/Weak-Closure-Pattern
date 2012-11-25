using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeakClosureProject;

namespace Example.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var someState = new Object();


			var objectSupplierStrongReference = new ObjectSupplier();

			var weakObjectSupplier = new WeakClosure<ObjectSupplier>(objectSupplierStrongReference);

			var someClosure_Dont_Do_So = new Object();

			// The standart approach.
			ObjectRecipient.DoExternalWork(
				someState,
				() =>
					{
						// !!! It's important! Don't use any other closures!
						// You can use only the weakObjectSupplier!
						//var forbidden_approach = someClosure_Dont_Do_So;

						var objectSupplier = weakObjectSupplier
							// You should understand what is the custom WeakReference{T} class at this project.
							.TargetTyped;

						if (objectSupplier == null)
						{
							return;
						}

						var someData = new Object();

						objectSupplier.DoSomeThing(someData);
					});

			// The little bit sophisticated approach.
			// It requires more deep knowledge of the WeakClosure from a developer.
			// But you shouldn't know what is the custom WeakReference{T} class at this project.
			ObjectRecipient.DoExternalWork(
				someState,
				() => weakObjectSupplier
					.ExecuteIfTargetNotNull(objectSupplier =>
				{
					// !!! It's important! Don't use any other closures!
					// You can use only the ExecuteIfTargetNotNull method action parameter - objectSupplier!
					//var forbidden_approach = someClosure_Dont_Do_So;

					var someData = new Object();

					objectSupplier.DoSomeThing(someData);

				}));

		}
	}

	/// <summary>
	/// The object that hole the Post-Action 
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
			// Do some things here...

			// Execute the post-action when all are done.
			if (postAction != null)
			{
				postAction();
			}
		}

	}





}
