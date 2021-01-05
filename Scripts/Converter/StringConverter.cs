using System;
using System.Text;

namespace ExpressionConvert
{
	public class StringConverter : IConverter<String>
	{
        public readonly static StringConverter Default = new StringConverter();

		public StringConverter () { }

		public void GetByteCount (in String src, ConvertStreamAllocator stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
            if (exist)
            {
                PrimitiveConverter.Default<int>().GetByteCount(default, stream);
			    int count = Encoding.UTF8.GetByteCount(src);
			    stream.AddCount(count);
            }
        }
		public void GetBytes (in String src, ConvertStreamWriter stream)
        {
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
            if (exist)
            {
                int count = Encoding.UTF8.GetByteCount(src);
				PrimitiveConverter.Default<int>().GetBytes(count, stream);
				var bytes = Encoding.UTF8.GetBytes(src);
				stream.Encode(bytes, bytes.Length);
            }
        }
		public void FromBytes (ref String src, ConvertStreamReader stream)
        {
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				int count = default;
				PrimitiveConverter.Default<int>().FromBytes(ref count, stream);
				var index = stream.Decode(out var bytes, count);
				src = Encoding.UTF8.GetString(bytes, index, count);
			}
			else
			{
				src = null;
			}
        }
	}
}