using System;

namespace ExpressionConvert
{
	public class ClassProcessor<T> where T : class
	{
		private readonly Func<T> constructor;
		private readonly GetByteCountDelegate<T> getByteCount;
		private readonly GetBytesDelegate<T> getBytes;
		private readonly FromBytesDelegate<T> fromBytes;

		public ClassProcessor (Func<T> constructor, GetByteCountDelegate<T> getByteCount, GetBytesDelegate<T> getBytes, FromBytesDelegate<T> fromBytes)
		{
			this.constructor = constructor;
			this.getByteCount = getByteCount;
			this.getBytes = getBytes;
			this.fromBytes = fromBytes;
		}

		public T Constructor () => constructor();
		public void GetByteCount (in T src, ConvertStreamAllocator stream) => getByteCount(src, stream);
		public void GetBytes (in T src, ConvertStreamWriter stream) => getBytes(src, stream);
		public void FromBytes (ref T src, ConvertStreamReader stream) => fromBytes(ref src, stream);
	}
}