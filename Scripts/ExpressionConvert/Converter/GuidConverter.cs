using System;

namespace ExpressionConvert
{
	public class GuidConverter : IConverter<Guid>
	{
		public readonly static GuidConverter Default = new GuidConverter();

		public GuidConverter () { }

		const int GuidSizeCount = 16;
		public void GetByteCount (in Guid src, ConvertStreamAllocator stream)
		{
			stream.AddCount(GuidSizeCount);
		}
		public void GetBytes (in Guid src, ConvertStreamWriter stream)
		{
			var bytes = src.ToByteArray();
			stream.Encode(bytes, GuidSizeCount);
		}
		public void FromBytes (ref Guid src, ConvertStreamReader stream)
		{
			int current = stream.Decode(out var bytes, GuidSizeCount);
			var span = new byte[GuidSizeCount];
			for (int i = 0; i < GuidSizeCount; ++ i) span[i] = bytes[current + i];
			src = new Guid(span);
		}
	}
}