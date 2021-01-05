namespace ExpressionConvert
{
	public class StructConverter<T> : IConverter<T> where T : struct
	{
		private readonly GetByteCountDelegate<T> getByteCount;
		private readonly GetBytesDelegate<T> getBytes;
		private readonly FromBytesDelegate<T> fromBytes;

		public StructConverter (GetByteCountDelegate<T> getByteCount, GetBytesDelegate<T> getBytes, FromBytesDelegate<T> fromBytes)
		{
			this.getByteCount = getByteCount;
			this.getBytes = getBytes;
			this.fromBytes = fromBytes;
		}

		public void GetByteCount (in T src, ConvertStreamAllocator stream) => getByteCount(src, stream);
		public void GetBytes (in T src, ConvertStreamWriter stream) => getBytes(src, stream);
		public void FromBytes (ref T src, ConvertStreamReader stream) => fromBytes(ref src, stream);
	}
}