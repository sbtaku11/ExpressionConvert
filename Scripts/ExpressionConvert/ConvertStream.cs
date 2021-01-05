namespace ExpressionConvert
{
	// Allocator
	public class ConvertStreamAllocator
	{
		private int count;
		
		public ConvertStreamAllocator () { }

		public int GetCount () => count;

		public void AddCount () => count ++;
		public void AddCount (int value) => count += value;

		public ConvertStreamWriter ToWriter () => new ConvertStreamWriter(count);
	}

	// Writer
	public class ConvertStreamWriter
	{
		private readonly byte[] data;
		private int current;
		
		public ConvertStreamWriter (int count) => data = new byte[count];

		public int GetCount () => data.Length;

		public void Encode (byte bytes) => data[current ++] = bytes;

		public void Encode (byte[] bytes, int count)
		{
			for (int i = 0; i < count; ++ i) data[current + i] = bytes[i];
			current += count;
		}

		public byte[] GetBytes () => data;
	}

	// Reader
	public class ConvertStreamReader
	{
		private readonly byte[] data;
		private int current;
		
		public ConvertStreamReader (byte[] bytes) => data = bytes;

		public int GetCount () => data.Length;

		public byte Decode () => data[current ++];

		public int Decode (out byte[] bytes, int count)
		{
			int start = current;
			bytes = data;
			current += count;
			return start;
		}
	}
}