using System;

namespace ExpressionConvert
{
    public static class EnumConverter
    {
        public static EnumConverter<T> Default<T> () where T : Enum
        {
            return EnumConverter<T>.Default;
        }
    }

	public class EnumConverter<T> : IConverter<T> where T : Enum
	{
        public readonly static EnumConverter<T> Default = new EnumConverter<T>();

		public EnumConverter () { }

		public void GetByteCount (in T src, ConvertStreamAllocator stream)
        {
            PrimitiveConverter.Default<int>().GetByteCount(default, stream);
        }
		public void GetBytes (in T src, ConvertStreamWriter stream)
        {
            var x = ExpressionConvert.ExpressionConvert<T, int>.Cast(src);
            PrimitiveConverter.Default<int>().GetBytes(x, stream);
        }
		public void FromBytes (ref T src, ConvertStreamReader stream)
        {
            int x = default;
            PrimitiveConverter.Default<int>().FromBytes(ref x, stream);
            src = ExpressionConvert.ExpressionConvert<int, T>.Cast(x);
        }
	}
}