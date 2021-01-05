namespace ExpressionConvert
{
	public static class ArrayConverter
	{
		public static ArrayConverter<T> Default<T> (IConverter<T> converter)
		{
			return new ArrayConverter<T>(converter);
		}
	}

	public class ArrayConverter<T> : IConverter<T[]>
	{
		private readonly IConverter<T> converter;

		public ArrayConverter (IConverter<T> converter)
		{
			this.converter = converter;
		}

		public void GetByteCount (in T[] src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				int length = src.Length;
				PrimitiveConverter.Default<int>().GetByteCount(default, stream);
				for (int i = 0; i < length; ++ i)
				{
					converter.GetByteCount(src[i], stream);
				}
			}
		}
		public void GetBytes (in T[] src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				int length = src.Length;
				PrimitiveConverter.Default<int>().GetBytes(length, stream);
				for (int i = 0; i < length; ++ i)
				{
					converter.GetBytes(src[i], stream);
				}
			}
		}
		public void FromBytes (ref T[] src, ConvertStreamReader stream)
		{
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				int length = default;
				PrimitiveConverter.Default<int>().FromBytes(ref length, stream);
				src = new T[length];
				for (int i = 0; i < length; ++ i)
				{
					converter.FromBytes(ref src[i], stream);
				}
			}
			else
			{
				src = null;
			}
		}
	}
}