using System;

namespace ExpressionConvert
{
	public class TimeSpanConverter : IConverter<TimeSpan>
	{
		public readonly static TimeSpanConverter Default = new TimeSpanConverter();

		public TimeSpanConverter () { }

		public void GetByteCount (in TimeSpan src, ConvertStreamAllocator stream)
		{
			PrimitiveConverter.Default<long>().GetByteCount(default, stream);
		}
		public void GetBytes (in TimeSpan src, ConvertStreamWriter stream)
		{
			PrimitiveConverter.Default<long>().GetBytes(src.Ticks, stream);
		}
		public void FromBytes (ref TimeSpan src, ConvertStreamReader stream)
		{
			long ticks = default;
			PrimitiveConverter.Default<long>().FromBytes(ref ticks, stream);
			src = new TimeSpan(ticks);
		}
	}
}