using System.Linq;
using Group.Net.Groups;
using NUnit.Framework;

namespace Group.Net.Test
{
    public class Test
    {
		[Test]
		public void CycleTest()
		{
			var group = new PermutationGroup(new Cycle( new [] { 0, 1 }, new [] { 2, 3 }, new [] { 4, 5 }, new [] { 6, 7 }, new [] { 8, 9 } ));

			CollectionAssert.AreEqual(new [] { 1, 0, 3, 2, 5, 4, 7, 6, 9, 8 }, group.Apply(0, new Points(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })));
		}

		[Test]
		public void PointsTest()
		{
			var group = new PermutationGroup(new Cycle(new [] { 0, 1 }, new [] { 2, 3 }, new [] { 4, 5 }, new [] { 6, 7 }, new [] { 8, 9 }));

			CollectionAssert.AreEqual(new Points(new [] { 2, 4, 3 }), group.Apply(0, new Points(new [] { 3, 5, 2 })));
		}

		[Test]
		public void ImageTest()
		{
			var group = new PermutationGroup(new Cycle(new [] { 0, 1 }, new [] { 2, 3 }, new [] { 4, 5 }, new [] { 6, 7 }, new [] { 8, 9 }));

			CollectionAssert.AreEqual(new Image<string>(new [] { "B", "A", "D", "C", "F", "E", "H", "G", "J", "I" }),
				group.Apply(0, new Image<string>(new [] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" })));
		}

		[Test]
		public void OrbitTest()
		{
			//	This group is D4, the symmetries of a square
			var group = new DihedralPermutationGroup(4);

			var corners = new Points(new [] { 0, 2 });
			var edges = new Points(new [] { 0, 1 });

			var image = new Image<int>(new [] { 0, 1, 3, 2 });

			var imageOrbit = group.Orbit(new [] { image });

			Assert.AreEqual(8, imageOrbit.Count);

			var cornersOrbit = group.Orbit(corners);
			var edgesOrbit = group.Orbit(edges);

			CollectionAssert.AreEquivalent(new [] { new [] { 0, 2 }, new [] { 3, 1 }, new [] { 1, 3 }, new [] { 2, 0 } }, cornersOrbit);
			CollectionAssert.AreEquivalent(new [] { new [] { 0, 1 }, new [] { 3, 0 }, new [] { 1, 0 }, new [] { 2, 3 },
													new [] { 2, 1 }, new [] { 0, 3 }, new [] { 1, 2 }, new [] { 3, 2 } }, edgesOrbit);
		}

		[Test]
		public void OrbitStabiliser()
		{
			//	This group is a 3x3 grid with symmetric rows and columns
			var group = new PermutationGroup(
				new Cycle(new [] { 0, 1 }, new [] { 3, 4 }, new [] { 6, 7 }),
				new Cycle(new [] { 0, 1, 2 }, new [] { 3, 4, 5 }, new [] { 6, 7, 8 }),
				new Cycle(new [] { 0, 3 }, new [] { 1, 4 }, new [] { 2, 5 }),
				new Cycle(new [] { 0, 3, 6 }, new [] { 1, 4, 7 }, new [] { 2, 5, 8 })			
			);

			PermutationGroup stabilisedPointZero;
			var orbitZero = group.OrbitStabiliser(new Points(0), out stabilisedPointZero);

			CollectionAssert.AreEquivalent(Enumerable.Range(0, 9).Select(i => new [] { i }), orbitZero);

			var orbitOfZeroOnStabiliserZero = stabilisedPointZero.Orbit(0);
			var orbitOfOneOnStabiliserZero = stabilisedPointZero.Orbit(1);
			var orbitOfFourOnStabiliserZero = stabilisedPointZero.Orbit(4);

			CollectionAssert.AreEquivalent(new [] { new [] { 0 } }, orbitOfZeroOnStabiliserZero);
			CollectionAssert.AreEquivalent(new [] { new [] { 1 }, new [] { 2 } }, orbitOfOneOnStabiliserZero);
			CollectionAssert.AreEquivalent(new [] { new [] { 4 }, new [] { 5 }, new [] { 7 }, new [] { 8 } }, orbitOfFourOnStabiliserZero);
		}

		[Test]
		public void OptimisedStabiliser()
		{
			//	This group is a 3x3 grid with symmetric rows and columns
			var group = new PermutationGroup(
				new Cycle(new [] { 0, 1 }, new [] { 3, 4 }, new [] { 6, 7 }),
				new Cycle(new [] { 0, 1, 2 }, new [] { 3, 4, 5 }, new [] { 6, 7, 8 }),
				new Cycle(new [] { 0, 3 }, new [] { 1, 4 }, new [] { 2, 5 }),
				new Cycle(new [] { 0, 3, 6 }, new [] { 1, 4, 7 }, new [] { 2, 5, 8 })
			);

			PermutationGroup stabilisedPointZero;
			group.OrbitStabiliser(new Points(0), out stabilisedPointZero);

			stabilisedPointZero.Optimise();
			
			Assert.AreEqual(2, stabilisedPointZero.GeneratorCount);

			var orbitOfZeroOnStabiliserZero = stabilisedPointZero.Orbit(0);
			var orbitOfOneOnStabiliserZero = stabilisedPointZero.Orbit(1);
			var orbitOfFourOnStabiliserZero = stabilisedPointZero.Orbit(4);

			CollectionAssert.AreEquivalent(new [] { new [] { 0 } }, orbitOfZeroOnStabiliserZero);
			CollectionAssert.AreEquivalent(new [] { new [] { 1 }, new [] { 2 } }, orbitOfOneOnStabiliserZero);
			CollectionAssert.AreEquivalent(new [] { new [] { 4 }, new [] { 5 }, new [] { 7 }, new [] { 8 } }, orbitOfFourOnStabiliserZero);
		}
	}
}
