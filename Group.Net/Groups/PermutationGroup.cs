using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Net.Groups
{
	public class PermutationGroup
	{
		private readonly IList<Element> Generators;
		private readonly IList<Element> InverseGenerators;
		
		internal readonly Element Identity;

		public Element this[int index]
		{
			get { return Generators[index]; }
		}

		public int GeneratorCount
		{
			get { return Generators.Count; }
		}

		internal PermutationGroup()
		{
			Generators = new List<Element>();
			Identity = new Element();
			InverseGenerators = Generators.Select(gen => gen.Inverse()).ToList();
		}

		public PermutationGroup(IEnumerable<Element> generators)
		{
			Generators = generators.ToList();
			var pointMaximum = Generators.Max(e => e.Count);
			Identity = new Element(Enumerable.Range(0, pointMaximum));

			foreach (var element in Generators)
				element.ExtendToPoints(pointMaximum);

			InverseGenerators = Generators.Select(gen => gen.Inverse()).ToList();
		}

		public PermutationGroup(params Element[] generators)
		{
			Generators = generators.ToList();
			var pointMaximum = Generators.Max(e => e.Count);
			Identity = new Element(Enumerable.Range(0, pointMaximum));

			foreach (var element in Generators)
				element.ExtendToPoints(pointMaximum);

			InverseGenerators = Generators.Select(gen => gen.Inverse()).ToList();
		}

		internal void AddGenerator(Element generator)
		{
			Generators.Add(generator);
			InverseGenerators.Add(generator.Inverse());
		}

		public Image<T> Apply<T>(int generatorIndex, Image<T> points) where T : IComparable<T>
		{
			return new Image<T>(points.Select((point, index) => points[Generators[generatorIndex][index]]).ToList());
		}

		public Points Apply(int generatorIndex, Points points)
		{
			return new Points(points.Select(point => InverseGenerators[generatorIndex][point]));
		}

		public HashSet<Points> Orbit(Points partialPoints)
		{
			var orbitPoints = new HashSet<Points>(new[] { partialPoints });
			var queue = new Queue<Points>(orbitPoints);

			while (queue.Any())
			{
				var headPoints = queue.Dequeue();

				for (var i = 0; i < Generators.Count; ++i)
				{
					var permutedPoints = Apply(i, headPoints);
					if (orbitPoints.Contains(permutedPoints))
						continue;

					orbitPoints.Add(permutedPoints);
					queue.Enqueue(permutedPoints);
				}
			}

			return orbitPoints;
		}

		public HashSet<Image<T>> Orbit<T>(IEnumerable<Image<T>> pointsBlock) where T : IComparable<T>
		{
			var orbitPoints = new HashSet<Image<T>>(pointsBlock);
			var queue = new Queue<Image<T>>(orbitPoints);

			while (queue.Any())
			{
				var headPoints = queue.Dequeue();

				for (var i = 0; i < Generators.Count; ++i)
				{
					var permutedPoints = Apply(i, headPoints);
					if (orbitPoints.Contains(permutedPoints))
						continue;

					orbitPoints.Add(permutedPoints);
					queue.Enqueue(permutedPoints);
				}
			}

			return orbitPoints;
		}

		public HashSet<Points> OrbitStabiliser(Points partialPoints, out PermutationGroup stabiliser)
		{
			stabiliser = new PermutationGroup();
			var elementLookup = new Dictionary<Points, Element>();

			var orbitPoints = new HashSet<Points>(new[] { partialPoints });
			var queue = new Queue<Points>(orbitPoints);
			elementLookup[partialPoints] = Identity;

			while (queue.Any())
			{
				var headPoints = queue.Dequeue();

				for (var i = 0; i < Generators.Count; ++i)
				{
					var permutedPoints = Apply(i, headPoints);
					if (orbitPoints.Contains(permutedPoints))
					{
						stabiliser.AddGenerator((elementLookup[headPoints].Apply(Generators[i])).Apply(elementLookup[permutedPoints].Inverse()));
						continue;
					}

					orbitPoints.Add(permutedPoints);
					queue.Enqueue(permutedPoints);
					elementLookup[permutedPoints] = elementLookup[headPoints].Apply(Generators[i]);
				}
			}

			return orbitPoints;
		}

		public int Size()
		{
			//	Make more efficient by constructing coset stabilising chain
			//	For now though, just calculate full orbit.
			var image = new Image<int>(Enumerable.Range(0, Generators[0].Count).ToList());

			return Orbit(new [] { image }).Count;
		}

		public void Optimise()
		{
			var groupSize = Size();

			var optimisedGroup = new PermutationGroup(Generators[0]);

			var optimisedGroupSize = optimisedGroup.Size();
			var generatorIndex = 0;
			do
			{
				var copiedOptimised = new PermutationGroup(optimisedGroup.Generators);

				copiedOptimised.AddGenerator(Generators[generatorIndex]);

				if (copiedOptimised.Size() > optimisedGroupSize)
				{
					optimisedGroup.AddGenerator(Generators[generatorIndex]);
					optimisedGroupSize = optimisedGroup.Size();
				}

				generatorIndex = (generatorIndex + 1) % Generators.Count;

			} while (optimisedGroupSize < groupSize);

			Generators.Clear();
			InverseGenerators.Clear();

			foreach (var generator in optimisedGroup.Generators)
				AddGenerator(generator);
		}
	}
}
