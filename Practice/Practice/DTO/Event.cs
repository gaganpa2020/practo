namespace Practice.DTO
{
	using System;

	class Event
	{
		public Event(Char Category, int Timestamp)
		{
			this.Category = Category;
			this.Timestamp = Timestamp;
		}

		public Char Category { get; set; }
		public int Timestamp { get; set; }
	}
}
