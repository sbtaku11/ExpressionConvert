using System.Net;

namespace ExpressionConvert
{
	public class IPEndPointConverter : IConverter<IPEndPoint>
	{
		public readonly static IPEndPointConverter Default = new IPEndPointConverter();

		public IPEndPointConverter () { }

		public void GetByteCount (in IPEndPoint src, ConvertStreamAllocator stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetByteCount(default, stream);
			if (exist)
			{
				IPAddressConverter.Default.GetByteCount(src.Address, stream);
				PrimitiveConverter.Default<int>().GetByteCount(default, stream);
			}
		}
		public void GetBytes (in IPEndPoint src, ConvertStreamWriter stream)
		{
			bool exist = !(src is null);
			PrimitiveConverter.Default<bool>().GetBytes(exist, stream);
			if (exist)
			{
				IPAddressConverter.Default.GetBytes(src.Address, stream);
				PrimitiveConverter.Default<int>().GetBytes(default, stream);
			}
		}
		public void FromBytes (ref IPEndPoint src, ConvertStreamReader stream)
		{
			bool exist = default;
			PrimitiveConverter.Default<bool>().FromBytes(ref exist, stream);
			if (exist)
			{
				IPAddress ip = default;
				int port = default;
				IPAddressConverter.Default.FromBytes(ref ip, stream);
				PrimitiveConverter.Default<int>().FromBytes(ref port, stream);
				src = new IPEndPoint(ip, port);
			}
			else
			{
				src = null;
			}
		}
	}
}