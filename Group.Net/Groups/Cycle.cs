using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Group.Net.Groups
{
	public class Cycle : IEnumerable<IList<int>>
	{
		private readonly IList<IList<int>> cycles;

		public Cycle(IEnumerable<IEnumerable<int>> cycles)
		{
			//if (cycles.Select(c1 => cycles.Where(c => c != c1).Select(c1.Intersect)).Any())
			//	throw new ApplicationException();

			this.cycles = cycles.Select(list => list.ToList()).Cast<IList<int>>().ToList();
		}

		public Cycle(params IEnumerable<int>[] cycles)
		{
			//if (cycles.Select(c1 => cycles.Where(c => c != c1).Select(c1.Intersect)).Any())
			//	throw new ApplicationException();

			this.cycles = cycles.Select(list => list.ToList()).Cast<IList<int>>().ToList();
		}

		public void Apply(IList<int> image)
		{
			foreach (var cycle in cycles)
			{
				var temp = image[cycle[cycle.Count - 1]];

				for (var i = cycle.Count - 1; i > 0; --i)
					image[cycle[i]] = image[cycle[i - 1]];

				image[cycle[0]] = temp;
			}			
		}

		public IEnumerator<IList<int>> GetEnumerator()
		{
			return cycles.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
