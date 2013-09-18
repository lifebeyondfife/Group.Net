using System;
using System.Linq;

namespace Group.Net.Groups
{
	public class SymmetricPermutationGroup : PermutationGroup
	{
		private SymmetricPermutationGroup(params Element[] generators)
			: base(generators)
		{
		}

		public SymmetricPermutationGroup(int n)
			: base(new Cycle(new[] { 0, 1 }), new Cycle(Enumerable.Range(0, n)))
		{
			if (n < 2)
				throw new ApplicationException("Symmetric Permutation Group must act on at least two points.");
		}
	}
}
