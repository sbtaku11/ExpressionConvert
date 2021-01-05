using System.Collections.Generic;

namespace ExpressionConvert
{
	public static class ListConverter
	{
		public static ListConverter<T> Default<T> (IConverter<T> converter)
		{
			return new ListConverter<T>(converter);
		}
	}

	public class ListConverter<T> : IConverter<List<T>>
	{
		private readonly IConverter<T> converter;

		public ListConverter (IConverter<T> converter)
		{
			this.converter = converter;
		}

		public void GetByteCount (in List<T> src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				int length = src.Count;
				PrimitiveConverter.Default<int>().GetByteCount(default, stream);
				foreach (var x in src)
				{
					converter.GetByteCount(x, stream);
				}
			}
		}
		public void GetBytes (in List<T> src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				int length = src.Count;
				PrimitiveConverter.Default<int>().GetBytes(length, stream);
				foreach (var x in src)
				{
					converter.GetBytes(x, stream);
				}
			}
		}
		public void FromBytes (ref List<T> src, ConvertStreamReader stream)
		{
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				int length = default;
				PrimitiveConverter.Default<int>().FromBytes(ref length, stream);
				src = new List<T>(length);
				for (int i = 0; i < length; ++ i)
				{
					T x = default;
					converter.FromBytes(ref x, stream);
					src.Add(x);
				}
			}
			else
			{
				src = null;
			}
		}
	}
}