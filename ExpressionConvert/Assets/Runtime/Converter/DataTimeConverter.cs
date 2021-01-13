using System;

namespace ExpressionConvert
{
	public class DateTimeConverter : IConverter<DateTime>
	{
		public readonly static DateTimeConverter Default = new DateTimeConverter();

		public DateTimeConverter () { }

		public void GetByteCount (in DateTime src, ConvertStreamAllocator stream)
		{
			PrimitiveConverter.Default<long>().GetByteCount(default, stream);
		}
		public void GetBytes (in DateTime src, ConvertStreamWriter stream)
		{
			PrimitiveConverter.Default<long>().GetBytes(src.Ticks, stream);
		}
		public void FromBytes (ref DateTime src, ConvertStreamReader stream)
		{
			long ticks = default;
			PrimitiveConverter.Default<long>().FromBytes(ref ticks, stream);
			src = new DateTime(ticks);
		}
	}
}