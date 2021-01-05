using System;

namespace ExpressionConvert
{
	public static class StructConvertBuilder
	{
		public static StructConvertBuilder<T> Create<T> () where T : struct
		{
			return new StructConvertBuilder<T>();
		}
	}

	public class StructConvertBuilder<T> where T : struct
	{
		private GetByteCountDelegate<T> getByteCount;
		private GetBytesDelegate<T> getBytes;
		private FromBytesDelegate<T> fromBytes;

		public StructConvertBuilder ()
		{
			getByteCount = (in T src, ConvertStreamAllocator stream) => { };
			getBytes = (in T src, ConvertStreamWriter stream) => { };
			fromBytes = (ref T src, ConvertStreamReader stream) => { };
		}

        public StructConvertBuilder<T> Add<U> (Func<T, U> getter, StructSetter<T, U> setter, IConverter<U> converter)
		{
			getByteCount += (in T src, ConvertStreamAllocator stream) => converter.GetByteCount(getter(src), stream);
			getBytes += (in T src, ConvertStreamWriter stream) => converter.GetBytes(getter(src), stream);
			fromBytes += (ref T src, ConvertStreamReader stream) => 
			{
				U value = default;
				converter.FromBytes(ref value, stream);
				setter(ref src, value);
			};

			return this;
		}

		public StructConvertBuilder<T> AddPrimitive<U> (Func<T, U> getter, StructSetter<T, U> setter)
		{
			Add(getter, setter, PrimitiveConverter.Default<U>());
			return this;
		}

		public StructConvertBuilder<T> AddEnum<U> (Func<T, U> getter, StructSetter<T, U> setter) where U : Enum
		{
			Add(getter, setter, EnumConverter.Default<U>());
			return this;
		}

		public StructConvertBuilder<T> AddString (Func<T, String> getter, StructSetter<T, String> setter)
		{
			Add(getter, setter, StringConverter.Default);
			return this;
		}

		public StructConverter<T> ToConverter ()
		{
			return new StructConverter<T>(getByteCount, getBytes, fromBytes);
		}
	}
}