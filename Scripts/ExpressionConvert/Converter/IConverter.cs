namespace ExpressionConvert
{
	public interface IConverter<T>
	{
		void GetByteCount (in T src, ConvertStreamAllocator stream);
		void GetBytes (in T src, ConvertStreamWriter stream);
		void FromBytes (ref T src, ConvertStreamReader stream);
	}
}