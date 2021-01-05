using System;

namespace ExpressionConvert
{
	public class ClassConverter<T> : IConverter<T> where T : class
	{
		private readonly Func<T> constructor;
		private readonly GetByteCountDelegate<T> getByteCount;
		private readonly GetBytesDelegate<T> getBytes;
		private readonly FromBytesDelegate<T> fromBytes;

		public ClassConverter (Func<T> constructor, GetByteCountDelegate<T> getByteCount, GetBytesDelegate<T> getBytes, FromBytesDelegate<T> fromBytes)
		{
			this.constructor = constructor;
			this.getByteCount = getByteCount;
			this.getBytes = getBytes;
			this.fromBytes = fromBytes;
		}

		public void GetByteCount (in T src, ConvertStreamAllocator stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
            if (exist) getByteCount(src, stream);
        }
		public void GetBytes (in T src, ConvertStreamWriter stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
            if (exist) getBytes(src, stream);
        }
		public void FromBytes (ref T src, ConvertStreamReader stream)
        {
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				T x = constructor();
            	fromBytes(ref x, stream);
            	src = x;
			}
			else
			{
				src = null;
			}
        }
	}
}