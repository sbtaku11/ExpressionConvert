using System;
using System.Collections.Generic;

namespace ExpressionConvert
{
	public class PolymorphGroup<T> where T : class
	{
		private readonly Dictionary<Type, PolymorphContent<T>> polymorphs;

		public PolymorphGroup (Dictionary<Type, PolymorphContent<T>> polymorphs)
		{
			this.polymorphs = polymorphs;
		}

		public Dictionary<Type, PolymorphContent<T>> GetPolymorphs ()
		{
			return polymorphs;
		}
	}
}