namespace ExpressionConvert
{
	public static class Converter
	{
		public static byte[] GetBytes<T> (in T src, IConverter<T> converter)
		{
			var allocator = new ConvertStreamAllocator();
			converter.GetByteCount(src, allocator);
			var writer = allocator.ToWriter();
			converter.GetBytes(src, writer);
			return writer.GetBytes();
		}

		public static T FromBytes<T> (byte[] bytes, IConverter<T> converter)
		{
			var reader = new ConvertStreamReader(bytes);
			T src = default;
			converter.FromBytes(ref src, reader);
			return src;
		}

		public static int GetByteSize<T> (in T src, IConverter<T> converter)
		{
			var allocator = new ConvertStreamAllocator();
			converter.GetByteCount(src, allocator);
			return allocator.GetCount();
		}
	}
}