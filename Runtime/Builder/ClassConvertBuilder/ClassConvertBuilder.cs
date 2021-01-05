using System;

namespace ExpressionConvert
{
	public static class ClassConvertBuilder
	{
		public static ClassConvertBuilder<T> Create<T> (Func<T> constructor) where T : class
		{
			return new ClassConvertBuilder<T>(constructor);
		}

		public static ClassConvertBuilder<T> Create<T> () where T : class
		{
			return new ClassConvertBuilder<T>(null);
		}
	}

	public class ClassConvertBuilder<T> where T : class
	{
		private Func<T> constructor;
		private GetByteCountDelegate<T> getByteCount;
		private GetBytesDelegate<T> getBytes;
		private FromBytesDelegate<T> fromBytes;

		public ClassConvertBuilder (Func<T> constructor) : base ()
		{
			this.constructor = constructor;
			getByteCount = (in T src, ConvertStreamAllocator stream) => { };
			getBytes = (in T src, ConvertStreamWriter stream) => { };
			fromBytes = (ref T src, ConvertStreamReader stream) => { };
		}

		public ClassConvertBuilder<T> Add<U> (Func<T, U> getter, Action<T, U> setter, IConverter<U> converter)
		{
			getByteCount += (in T src, ConvertStreamAllocator stream) => converter.GetByteCount(getter(src), stream);
			getBytes += (in T src, ConvertStreamWriter stream) => converter.GetBytes(getter(src), stream);
			fromBytes += (ref T src, ConvertStreamReader stream) => 
			{
				U value = default;
				converter.FromBytes(ref value, stream);
				setter(src, value);
			};

			return this;
		}

		public ClassConvertBuilder<T> AddPrimitive<U> (Func<T, U> getter, Action<T, U> setter)
		{
			Add(getter, setter, PrimitiveConverter.Default<U>());
			return this;
		}

		public ClassConvertBuilder<T> AddString (Func<T, String> getter, Action<T, String> setter)
		{
			Add(getter, setter, StringConverter.Default);
			return this;
		}

		public ClassConvertBuilder<T> AddEnum<U> (Func<T, U> getter, Action<T, U> setter) where U : Enum
		{
			Add(getter, setter, EnumConverter.Default<U>());
			return this;
		}
		
		public ClassProcessor<T> ToProcessor ()
		{
			return new ClassProcessor<T>(constructor, getByteCount, getBytes, fromBytes);
		}

		public ClassConverter<T> ToConverter ()
		{
			return new ClassConverter<T>(constructor, getByteCount, getBytes, fromBytes);
		}
	}
}