using System;
using System.Collections.Generic;

namespace Group.Net.Groups
{
	public class PermutationGroup<T> where T : IComparable<T>
	{
		protected readonly IList<IList<int>> Generators;

		public int GeneratorCount
		{
			get { return Generators.Count; }
		}

		public PermutationGroup(IList<IList<int>> generators)
		{
			Generators = generators;
		}

		public Points<T> Apply(int index, Points<T> points)
		{
			var permutedPoints = new Points<T>(points);
			
			if (permutedPoints.Count == 1)
				return permutedPoints;

			for (var i = 0; i < Generators[index].Count - 1; ++i)
				permutedPoints[Generators[index][i + 1]] = points[Generators[index][i]];

			permutedPoints[Generators[index][0]] = points[Generators[index][Generators[index].Count - 1]];

			return permutedPoints;
		}
	}
}
