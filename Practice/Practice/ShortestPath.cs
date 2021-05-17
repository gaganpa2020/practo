namespace Practice
{
	using Practice.DTO;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public class Node
	{
		public int Timestamp { get; set; }
		public DataTransaction Data { get; set; }
		public List<string> AdjacentNodes { get; set; }
	}

	public class ShortestPathSolution
	{
		public void Solution()
		{
			Console.WriteLine(ShortestPath(GetTestData(), "FG", 9));
		}

		private int ShortestPath(IList<DataTransaction> data, string toAccount, int asOfTime)
		{
			// Transform datastructure to make a graph. 
			IDictionary<string, Node> graph = new Dictionary<string, Node>();
			foreach (var item in data)
			{
				if (!graph.ContainsKey(item.Account))
				{
					graph[item.Account] = new Node()
					{
						Timestamp = item.Timestamp,
						Data = item,
						AdjacentNodes = new List<string>()
					};
				}

				if (graph.ContainsKey(item.Bought_From))
				{
					graph[item.Bought_From].AdjacentNodes.Add(item.Account);
				}
			}


			// Check if account doesn't exist in data structure. 
			if (!graph.ContainsKey(toAccount))
			{
				Console.WriteLine("Target account not found!");
				throw new Exception("Target account not found!");
			}


			// Traverse and send results. 
			ArrayList result = ShortestPathInternal(graph, toAccount, asOfTime);

			if (result.Count > 0)
			{
				Console.WriteLine(string.Join("->", result.ToArray()));
			}
			else
			{
				Console.WriteLine("No path found at point in time!");
				throw new Exception("No path found at point in time!");
			}

			return result.Count;
		}

		// Traverse the graph
		private ArrayList ShortestPathInternal(IDictionary<string, Node> graph, string toAccount, int asOfTime)
		{
			KeyValuePair<string, Node> head = graph.First();
			ArrayList visited = new ArrayList();

			while (head.IsNotNull() && head.Key != toAccount && head.Value.Timestamp <= asOfTime)
			{
				visited.Add(head.Key);
				var adjacentNodes = graph.Where(x => head.Value.AdjacentNodes.Contains(x.Key));
				head = (from p in adjacentNodes
						where (p.Value.AdjacentNodes.Contains(toAccount) || p.Value.AdjacentNodes.Any())
						&& !visited.Contains(p.Key)
						orderby p.Value.Timestamp descending
						select p).FirstOrDefault();
			}

			return visited;
		}

		private IList<DataTransaction> GetTestData()
		{
			return new List<DataTransaction>()
			{
				new DataTransaction(){ Timestamp=1, Account="AB", Bought_From=string.Empty },
				new DataTransaction(){ Timestamp=2, Account="YZ", Bought_From="AB" },
				new DataTransaction(){ Timestamp=3, Account="MN", Bought_From="YZ" },
				new DataTransaction(){ Timestamp=3, Account="QR", Bought_From="YZ" },
				new DataTransaction(){ Timestamp=3, Account="HI", Bought_From="YZ" },
				new DataTransaction(){ Timestamp=2, Account="CD", Bought_From="MN" },
				new DataTransaction(){ Timestamp=2, Account="JK", Bought_From="MN" },
				new DataTransaction(){ Timestamp=2, Account="ST", Bought_From="HI" },
				new DataTransaction(){ Timestamp=2, Account="ST", Bought_From="JK" },
				new DataTransaction(){ Timestamp=2, Account="JK", Bought_From="ST" },
				new DataTransaction(){ Timestamp=2, Account="FG", Bought_From="JK" },
				new DataTransaction(){ Timestamp=2, Account="UV", Bought_From="ST" },
				new DataTransaction(){ Timestamp=2, Account="FG", Bought_From="UV" },
				new DataTransaction(){ Timestamp=2, Account="UV", Bought_From="YZ" }
			};
		}
	}
}
