namespace ExpressionConvert
{
    public static class NullableConverter
	{
		public static NullableConverter<T> Default<T> (IConverter<T> converter) where T : struct
		{
			return new NullableConverter<T>(converter);
		}
	}

	public class NullableConverter<T> : IConverter<T?> where T : struct
	{
		private readonly IConverter<T> converter;

		public NullableConverter (IConverter<T> converter)
		{
			this.converter = converter;
		}

		public void GetByteCount (in T? src, ConvertStreamAllocator stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
            if (exist) converter.GetByteCount(src.Value, stream);
        }
		public void GetBytes (in T? src, ConvertStreamWriter stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
            if (exist) converter.GetBytes(src.Value, stream);
        }
		public void FromBytes (ref T? src, ConvertStreamReader stream)
        {
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				T x = default;
            	converter.FromBytes(ref x, stream);
            	src = x;
			}
			else
			{
				src = null;
			}
        }
	}
}