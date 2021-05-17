namespace Practice
{
	using System.Collections.Generic;

	public static class KeyValuePairExtension
	{
		public static bool IsNotNull<T, R>(this KeyValuePair<T, R> keyValuePair)
		{
			bool result = true;

			if (keyValuePair.Key == null || keyValuePair.Value == null)
			{
				result = false;
			}

			return result;
		}
	}
}
