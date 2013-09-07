using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Net.Groups
{
	public class SymmetricPermutationGroup<T> : PermutationGroup<T> where T : IComparable<T>
	{
		private SymmetricPermutationGroup(IList<IList<int>> generators)
			: base(generators)
		{
		}

		public SymmetricPermutationGroup(int n)
			: base(new[] { new List<int>(new[] { 0, 1 }), Enumerable.Range(0, n).ToList() })
		{
		}
	}
}
