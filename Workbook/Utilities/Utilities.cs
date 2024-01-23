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

		/// <summary>
		/// Tests for a DateOnly in the future from now
		/// </summary>
		/// <param name="value">The date to check</param>
		/// <returns>True if the date to check is in the future, false otherwise</returns>
		public static bool IsInTheFuture(DateOnly value) => value > DateOnly.FromDateTime(DateTime.Now);
		
	}
}
