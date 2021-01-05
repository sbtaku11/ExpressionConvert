using System;

namespace ExpressionConvert.PrimitiveConvert
{
	public class PrimitiveConvertFormatter
	: IConverter<Boolean>
	, IConverter<Byte>
	, IConverter<Char>
	, IConverter<Int16>
	, IConverter<Int32>
	, IConverter<Int64>
	, IConverter<UInt16>
	, IConverter<UInt32>
	, IConverter<UInt64>
	, IConverter<Single>
	, IConverter<Double>
	{
		static PrimitiveConvertFormatter ()
		{
			var Default = new PrimitiveConvertFormatter();
			PrimitiveConverter<Boolean>.Default = Default;
			PrimitiveConverter<Byte>.Default = Default;
			PrimitiveConverter<Char>.Default = Default;
			PrimitiveConverter<Int16>.Default = Default;
			PrimitiveConverter<Int32>.Default = Default;
			PrimitiveConverter<Int64>.Default = Default;
			PrimitiveConverter<UInt16>.Default = Default;
			PrimitiveConverter<UInt32>.Default = Default;
			PrimitiveConverter<UInt64>.Default = Default;
			PrimitiveConverter<Single>.Default = Default;
			PrimitiveConverter<Double>.Default = Default;
		}

		public static void Initialize () { }

		// Boolean
		const byte True = 1;
		const byte False = 0;
		public void GetByteCount (in Boolean src, ConvertStreamAllocator stream) => stream.AddCount();
		public void GetBytes (in Boolean src, ConvertStreamWriter stream) => stream.Encode(src ? True : False);
		public void FromBytes (ref Boolean src, ConvertStreamReader stream) => src = stream.Decode() == True;

		// Byte
		public void GetByteCount (in Byte src, ConvertStreamAllocator stream) => stream.AddCount();
		public void GetBytes (in Byte src, ConvertStreamWriter stream) => stream.Encode(src);
		public void FromBytes (ref Byte src, ConvertStreamReader stream) => src = stream.Decode();

		// Char
		const int CharByteSize = 2;
		public void GetByteCount (in Char src, ConvertStreamAllocator stream)
		{
			stream.AddCount(CharByteSize);
		}
		public void GetBytes (in Char src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, CharByteSize);
		}
		public void FromBytes (ref Char src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, CharByteSize);
			src = BitConverter.ToChar(bytes, index);
		}
		
		// Int16
		const int Int16ByteSize = 2;
		public void GetByteCount (in Int16 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(Int16ByteSize);
		}
		public void GetBytes (in Int16 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, Int16ByteSize);
		}
		public void FromBytes (ref Int16 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, Int16ByteSize);
			src = BitConverter.ToInt16(bytes, index);
		}

		// Int32
		const int Int32ByteSize = 4;
		public void GetByteCount (in Int32 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(Int32ByteSize);
		}
		public void GetBytes (in Int32 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, Int32ByteSize);
		}
		public void FromBytes (ref Int32 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, Int32ByteSize);
			src = BitConverter.ToInt32(bytes, index);
		}

		// Int64
		const int Int64ByteSize = 8;
		public void GetByteCount (in Int64 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(Int64ByteSize);
		}
		public void GetBytes (in Int64 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, Int64ByteSize);
		}
		public void FromBytes (ref Int64 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, Int64ByteSize);
			src = BitConverter.ToInt64(bytes, index);
		}

		// UInt16
		const int UInt16ByteSize = 2;
		public void GetByteCount (in UInt16 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(UInt16ByteSize);
		}
		public void GetBytes (in UInt16 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, UInt16ByteSize);
		}
		public void FromBytes (ref UInt16 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, UInt16ByteSize);
			src = BitConverter.ToUInt16(bytes, index);
		}

		// UInt32
		const int UInt32ByteSize = 4;
		public void GetByteCount (in UInt32 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(UInt32ByteSize);
		}
		public void GetBytes (in UInt32 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, UInt32ByteSize);
		}
		public void FromBytes (ref UInt32 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, UInt32ByteSize);
			src = BitConverter.ToUInt32(bytes, index);
		}

		// UInt64
		const int UInt64ByteSize = 8;
		public void GetByteCount (in UInt64 src, ConvertStreamAllocator stream)
		{
			stream.AddCount(UInt64ByteSize);
		}
		public void GetBytes (in UInt64 src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, UInt64ByteSize);
		}
		public void FromBytes (ref UInt64 src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, UInt64ByteSize);
			src = BitConverter.ToUInt64(bytes, index);
		}

		// Single
		const int SingleByteSize = 4;
		public void GetByteCount (in Single src, ConvertStreamAllocator stream)
		{
			stream.AddCount(SingleByteSize);
		}
		public void GetBytes (in Single src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, SingleByteSize);
		}
		public void FromBytes (ref Single src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, SingleByteSize);
			src = BitConverter.ToSingle(bytes, index);
		}

		// Double
		const int DoubleByteSize = 8;
		public void GetByteCount (in Double src, ConvertStreamAllocator stream)
		{
			stream.AddCount(DoubleByteSize);
		}
		public void GetBytes (in Double src, ConvertStreamWriter stream)
		{
			var bytes = BitConverter.GetBytes(src);
			stream.Encode(bytes, DoubleByteSize);
		}
		public void FromBytes (ref Double src, ConvertStreamReader stream)
		{
			var index = stream.Decode(out var bytes, DoubleByteSize);
			src = BitConverter.ToDouble(bytes, index);
		}
	}
}