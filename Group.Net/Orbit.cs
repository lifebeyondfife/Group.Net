using System;
using System.Collections.Generic;
using System.Linq;

using Group.Net.Groups;

namespace Group.Net
{
	public class Orbit<T> where T : IComparable<T>
	{
		private readonly PermutationGroup<T> group;

		internal Orbit(PermutationGroup<T> group)
		{
			this.group = group;
		}

		internal HashSet<Points<T>> CalculateOrbit(IEnumerable<Points<T>> pointsBlock)
		{
			var orbitPoints = new HashSet<Points<T>>(pointsBlock);
			var queue = new Queue<Points<T>>(orbitPoints);

			while (queue.Any())
			{
				var headPoints = queue.Dequeue();

				for (var i = 0; i < group.GeneratorCount; ++i)
				{
					var permutedPoints = group.Apply(i, headPoints);
					if (orbitPoints.Contains(permutedPoints))
						continue;

					orbitPoints.Add(permutedPoints);
					queue.Enqueue(permutedPoints);
				}
			}

			return orbitPoints;
		}
	}
}
