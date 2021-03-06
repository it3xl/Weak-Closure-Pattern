﻿using System;

namespace WeakClosureProject
{
	/// <summary>
	/// it3xl.com: maximally simplified generic implementation of the <see cref="WeakReference"/>.<para/>
	/// The Silverlight supported.<para/>
	/// Kick it out if you use the .NET Framework 4.5 or later.
	/// <para/>
	/// Also other implementations has no Silverlight support (.NET Framework 4.5 too has no).
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class WeakReference<T>
		where T : class
	{
		private WeakReference WeakReferenceHost { get; set; }

		/// <summary>
		/// See <see cref="WeakReference"/> appropriate constructor.
		/// </summary>
		/// <param name="target"></param>
		public WeakReference(T target)
		{
			WeakReferenceHost = new WeakReference(target);
		}

		/// <summary>
		/// See <see cref="WeakReference"/> appropriate constructor.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="trackResurrection"></param>
		public WeakReference(T target, Boolean trackResurrection)
		{
			WeakReferenceHost = new WeakReference(target, trackResurrection);
		}

		/// <summary>
		/// The typed analog of the <see cref="System.WeakReference.Target"/> property.
		/// </summary>
		public T TargetTyped
		{
			get
			{
				var target = WeakReferenceHost.Target;

				if (WeakReferenceHost.IsAlive == false)
				{
					return null;
				}

				return target as T;
			}
			set
			{
				WeakReferenceHost.Target = value;
			}
		}

	}
}
