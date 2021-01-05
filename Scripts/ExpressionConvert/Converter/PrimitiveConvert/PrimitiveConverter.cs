using ExpressionConvert.PrimitiveConvert;

namespace ExpressionConvert
{
	public static class PrimitiveConverter
	{
		public static IConverter<T> Default<T> () => PrimitiveConverter<T>.Default;
	}
}

namespace ExpressionConvert.PrimitiveConvert
{
	public static class PrimitiveConverter<T>
	{
		public static IConverter<T> Default;

		static PrimitiveConverter () => PrimitiveConvertFormatter.Initialize();
	}
}