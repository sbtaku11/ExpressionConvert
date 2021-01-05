using System;
using System.Collections.Generic;
using ExpressionConvert.PrimitiveConvert;

namespace ExpressionConvert
{
	public class PolymorphConverter<T> : IConverter<T> where T : class
	{
		private readonly Dictionary<Type, int> orders;
		private readonly ClassProcessor<T>[] processors;
		private readonly int[][] inhabitanceOrders;

		public PolymorphConverter (Dictionary<Type, PolymorphContent<T>> polymorphs)
		{
			orders = new Dictionary<Type, int>();

			var contents = new PolymorphContent<T>[polymorphs.Count];

			int current = 0;
			foreach (var polymorph in polymorphs)
			{
				orders[polymorph.Key] = current;
				contents[current] = polymorph.Value;
				current ++;
			}

			processors = new ClassProcessor<T>[current];
			inhabitanceOrders = new int[current][];

			for (int i = 0; i < current; ++ i)
			{
				var content = contents[i];
				processors[i] = content.processor;

				var array = content.inhabitanceOrders.ToArray();
				int length = array.Length;
				inhabitanceOrders[i] = new int[length];
				for (int j = 0; j < length; ++ j) inhabitanceOrders[i][j] = orders[array[j]];
			}
		}
		
		public void GetByteCount (in T src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				int order = orders[src.GetType()];
				PrimitiveConverter<int>.Default.GetByteCount(default, stream);
				processors[order].GetByteCount(src, stream);
				foreach (var i in inhabitanceOrders[order]) processors[i].GetByteCount(src, stream);
			}
		}
		public void GetBytes (in T src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				var order = orders[src.GetType()];
				PrimitiveConverter<int>.Default.GetBytes(order, stream);
				processors[order].GetBytes(src, stream);
				foreach (var i in inhabitanceOrders[order]) processors[i].GetBytes(src, stream);
			}
		}
		public void FromBytes (ref T src, ConvertStreamReader stream)
		{	
			bool exist = default;
			PrimitiveConverter<bool>.Default.FromBytes(ref exist, stream);
			if (exist)
			{
				int order = default;
				PrimitiveConverter<int>.Default.FromBytes(ref order, stream);
				T x = processors[order].Constructor();
				processors[order].FromBytes(ref x, stream);
				foreach (var i in inhabitanceOrders[order]) processors[i].FromBytes(ref x, stream);
				src = x;
			}
			else
			{
				src = null;
			}
		}
	}
}