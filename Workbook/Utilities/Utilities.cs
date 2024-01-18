namespace Utils
{
	public static class Utilities
	{
		//public static bool IsNullEmptyOrWhiteSpace(string value)
		//{
		//	return string.IsNullOrWhiteSpace(value);
		//}

		public static bool IsNullEmptyOrWhiteSpace(string value) => string.IsNullOrWhiteSpace(value);

		public static bool IsZeroOrNegative(int value) => value <= 0;
		
	}
}
