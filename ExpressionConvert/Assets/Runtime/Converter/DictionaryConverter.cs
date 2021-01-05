using System.Collections.Generic;

namespace ExpressionConvert
{
	public static class DictionaryConverter
	{
		public static DictionaryConverter<T, U> Default<T, U> (IConverter<T> keyConverter, IConverter<U> valueConverterU)
        {
            return new DictionaryConverter<T, U>(keyConverter, valueConverterU);
        }
	}

	public class DictionaryConverter<T, U> : IConverter<Dictionary<T, U>>
	{
		private readonly IConverter<T> keyConverter;
		private readonly IConverter<U> valueConverter;

		public DictionaryConverter (IConverter<T> keyConverter, IConverter<U> valueConverter)
		{
			this.keyConverter = keyConverter;
			this.valueConverter = valueConverter;
		}

		public void GetByteCount (in Dictionary<T, U> src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				int length = src.Count;
				PrimitiveConverter.Default<int>().GetByteCount(default, stream);
				foreach (var x in src)
				{
					keyConverter.GetByteCount(x.Key, stream);
					valueConverter.GetByteCount(x.Value, stream);
				}
			}
		}
		public void GetBytes (in Dictionary<T, U> src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				int length = src.Count;
				PrimitiveConverter.Default<int>().GetBytes(length, stream);
				foreach (var x in src)
				{
					keyConverter.GetBytes(x.Key, stream);
					valueConverter.GetBytes(x.Value, stream);
				}
			}
		}
		public void FromBytes (ref Dictionary<T, U> src, ConvertStreamReader stream)
		{
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				int length = default;
				PrimitiveConverter.Default<int>().FromBytes(ref length, stream);
				src = new Dictionary<T, U>(length);
				for (int i = 0; i < length; ++ i)
				{
                    T x = default;
					keyConverter.FromBytes(ref x, stream);
                    U y = default;
					valueConverter.FromBytes(ref y, stream);
                    src.Add(x, y);
				}
			}
			else
			{
				src = null;
			}
		}
	}
}