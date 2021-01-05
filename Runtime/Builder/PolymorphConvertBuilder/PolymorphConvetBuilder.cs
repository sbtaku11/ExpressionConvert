using System;
using System.Collections.Generic;

namespace ExpressionConvert
{
	public static class PolymorphConvetBuilder
	{
		public static PolymorphConvetBuilder<T> Create<T> () where T : class
		{
			return new PolymorphConvetBuilder<T>();
		}

		public static PolymorphConvetBuilder<T> Create<T> (ClassProcessor<T> processor) where T : class
		{
			return new PolymorphConvetBuilder<T>(processor);
		}
	}

	public class PolymorphConvetBuilder<T> where T : class
	{
		private readonly bool inhabitance;
		private readonly Dictionary<Type, PolymorphContent<T>> contents;
		private int currentOrder;

		public PolymorphConvetBuilder ()
		{
			contents = new Dictionary<Type, PolymorphContent<T>>();
		}

		public PolymorphConvetBuilder (ClassProcessor<T> processor)
		{
			inhabitance = true;

			contents = new Dictionary<Type, PolymorphContent<T>>();
			var content = new PolymorphContent<T>(processor);
			contents.Add(typeof(T), content);
		}

		public PolymorphConvetBuilder<T> Add<U> (ClassProcessor<U> processor) where U : class, T
		{
			if (processor is null) throw new ArgumentNullException("Processor is null");

			var content = new PolymorphContent<T>(DownCast(processor));

			if (inhabitance) content.inhabitanceOrders.Add(typeof(T));

			contents.Add(typeof(U), content);

			return this;
		}

		public PolymorphConvetBuilder<T> Add<U> (PolymorphGroup<U> group) where U : class, T
		{
			if (group is null) throw new ArgumentNullException("Group is null");

			var groupPolymorphs = group.GetPolymorphs();
			foreach (var groupPolymorph in groupPolymorphs)
			{
				var processor = groupPolymorph.Value.processor;
				var content = new PolymorphContent<T>(DownCast(processor));

				var inhabitanceOrders = groupPolymorph.Value.inhabitanceOrders;
				foreach (var i in inhabitanceOrders) content.inhabitanceOrders.Add(i);

				if (inhabitance) content.inhabitanceOrders.Add(typeof(T));
				
				contents.Add(groupPolymorph.Key, content);
			}

			return this;
		}

		private ClassProcessor<T> DownCast<U> (ClassProcessor<U> processor) where U : class, T
		{
			Func<T> constructor = processor.Constructor;
			GetByteCountDelegate<T> getByteCount = (in T src, ConvertStreamAllocator stream) => processor.GetByteCount((U)src, stream);
			GetBytesDelegate<T> getBytes = (in T src, ConvertStreamWriter stream) => processor.GetBytes((U)src, stream);
			FromBytesDelegate<T> fromBytes = (ref T src, ConvertStreamReader stream) => 
			{
				U x = (U)src;
				processor.FromBytes(ref x, stream);
				src = x;
			};

			return new ClassProcessor<T>(constructor, getByteCount, getBytes, fromBytes);
		}

		public PolymorphGroup<T> ToGroup ()
		{
			return new PolymorphGroup<T>(contents);
		}

		public IConverter<T> ToConverter ()
		{
			return new PolymorphConverter<T>(contents);
		}
	}
}