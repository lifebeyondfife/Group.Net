using System;
using System.Linq;

namespace Group.Net.Groups
{
	public class DihedralPermutationGroup : PermutationGroup
	{
		private DihedralPermutationGroup(params Element[] generators)
			: base(generators)
		{
		}

		public DihedralPermutationGroup(int n)
			: base(
				new Cycle(Enumerable.Range(0, n)),
				new Cycle(Enumerable.Range(1, n/2).Where(i => i < n - i).Select(i => new [] { i, n - i }))
			)
		{
			if (n < 2)
				throw new ApplicationException("Dihedral Permutation Group must act on at least two points.");
		}
	}
}
