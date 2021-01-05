namespace ExpressionConvert
{
	public delegate T Constructor<T> (ConvertStreamReader stream);
	public delegate void GetByteCountDelegate<T> (in T src, ConvertStreamAllocator stream);
	public delegate void GetBytesDelegate<T> (in T src, ConvertStreamWriter stream);
	public delegate void FromBytesDelegate<T> (ref T src, ConvertStreamReader stream);

	public delegate void StructSetter<T, U> (ref T o, U v);
}