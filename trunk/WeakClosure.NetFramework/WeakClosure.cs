using System;
using System.Diagnostics;

namespace WeakClosureProject
{
	/// <summary>
	/// The implementation of the Weak Closure Pattern.<para/>
	/// <para/>
	/// If you used to use post-actions with the closures and guaranteed don't want to get problems with memory leaks, then use this pattern.
	/// This pattern allows to simplify the object-recipient that now shouldn't control the lifetime of given post-action (by making the nulling of the strong reference of the Action at some point).
	/// But it makes little bit complicated the object-supplier of the post-action.
	/// </summary>
	/// <typeparam name="T">A type of a target object catched by the closure.</typeparam>
	/// <remarks>
	/// https://weakclosure.codeplex.com/
	/// Use the <see cref="Nullable{T}"/> if you want to work with the value type of <see cref="T"/>
	/// </remarks>
	/// <example>
	/// 
	/// </example>
	public class WeakClosure<T> : WeakReference<T>
		where T : class
	{
		public WeakClosure(T target)
			: base(target)
		{
		}

		public WeakClosure(T target, Boolean trackResurrection)
			: base(target, trackResurrection)
		{
		}

		/// <summary>
		/// Удобный метод, позволяющий сократить код за счет инкапсуляции в нем проверок на null целевого объекта.<para/>
		/// !!! Don't create any closure at the <see cref="action"/>.<para/>
		/// Use the <see cref="action"/> parameter only.<para/>
		/// <para/>
		/// Если вы сомневаетесь или не знаете, что такое замыкание, то не используйте этот метод!
		/// Или используйте обычную проверку на if(<see cref="WeakReference{T}.TargetTyped"/> != null).
		/// </summary>
		/// <param name="action"></param>
		[DebuggerStepThrough]
		public void ExecuteIfTargetNotNull(Action<T> action)
		{
			if (TargetTyped == null)
			{
				return;
			}

			action(TargetTyped);
		}
	}
}
