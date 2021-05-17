namespace Practice
{
	using Practice.DTO;
	using System;
	using System.Collections.Generic;

	class RecencyCalculation
	{
		public void Solution()
		{
			// Arrange test data.
			IList<Event> eventSet = GetTestData();
			int[] ageGroups = new int[3] { 1, 5, 8 };
			int asOfTime = 10;

			// Calculate frequency 
			int[] result = CalculateFrequency(eventSet, ageGroups, asOfTime);

			// Print the results.
			Console.WriteLine(string.Join(',', result));
		}

		/*
		 * Assumptions - 1: Timestamp would have consistant data in terms of time entry, 
		 * Timestamp will always grow on the scale of seconds. All the traction would have definit timestamp in seconds. 
		 * For example: On a x axis, we will have time(s) and will keep ploting events on it. A event lets say A would have timestamp as 2 or 3. 
		 * Timestamp 2.5 is not considered. 
		 * 
		 * Assumption -2: asOfTime would be non zero. 
		 */
		public int[] CalculateFrequency(IList<Event> sampleData, int[] ageGroups, int asOfTime)
		{
			//Transform the sample data into dictionary based on assumption that time is increase in a linear way. 
			IDictionary<int, IList<char>> collection = new Dictionary<int, IList<char>>();
			foreach (var item in sampleData)
			{
				if (collection.ContainsKey(item.Timestamp))
				{
					collection[item.Timestamp].Add(item.Category);
				}
				else
				{
					collection[item.Timestamp] = new List<char>() { item.Category };
				}
			}

			// Calculate event based on the ageGroups and as of time
			int[] result = new int[ageGroups.Length];
			foreach (var age in ageGroups)
			{
				int sum = 0;
				int counter = asOfTime;
				int traverseTill = asOfTime - age;
				while (counter >= traverseTill)
				{
					if (collection.ContainsKey(counter))
					{
						sum += collection[counter].Count;
					}

					counter--;
				}

				result[Array.IndexOf(ageGroups, age)] = sum;
			}

			return result;
		}

		private IList<Event> GetTestData()
		{
			IList<Event> eventSet = new List<Event>();
			//Define the test data. 
			eventSet = new List<Event>();
			eventSet.Add(new Event('A', 2));
			eventSet.Add(new Event('A', 4));
			eventSet.Add(new Event('A', 8));
			eventSet.Add(new Event('A', 10));
			eventSet.Add(new Event('A', 12));
			eventSet.Add(new Event('B', 1));
			eventSet.Add(new Event('B', 3));
			eventSet.Add(new Event('B', 6));
			eventSet.Add(new Event('B', 13));
			eventSet.Add(new Event('C', 2));
			eventSet.Add(new Event('C', 5));
			eventSet.Add(new Event('C', 7));
			eventSet.Add(new Event('C', 9));
			eventSet.Add(new Event('C', 15));
			return eventSet;
		}
	}
}
