using System.Net;

namespace ExpressionConvert
{
	public class IPAddressConverter : IConverter<IPAddress>
	{
		public readonly static IPAddressConverter Default = new IPAddressConverter();

		public IPAddressConverter () { }

		public void GetByteCount (in IPAddress src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				StringConverter.Default.GetByteCount(src.ToString(), stream);
			}
		}
		public void GetBytes (in IPAddress src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				var str = src.ToString();
				StringConverter.Default.GetBytes(str, stream);
			}
		}
		public void FromBytes (ref IPAddress src, ConvertStreamReader stream)
		{
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				string str = default;
				StringConverter.Default.FromBytes(ref str, stream);
				src = IPAddress.Parse(str);
			}
			else
			{
				src = null;
			}
		}
	}
}