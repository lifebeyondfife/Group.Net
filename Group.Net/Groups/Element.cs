using System.Collections.Generic;
using System.Linq;

namespace Group.Net.Groups
{
	public class Element
	{
		private readonly IList<int> element;

		public int Count
		{
			get { return element.Count; }
		}

		public int this[int index]
		{
			get { return element[index]; }
			set { element[index] = value; }
		}

		internal Element()
		{
			element = new List<int>();
		}

		public Element(IEnumerable<int> element)
		{
			this.element = element.ToList();
		}

		public Element Apply(Element other)
		{
			return new Element(element.Select((point, index) => element[other[index]]).ToList());
		}

		public Element Inverse()
		{
			var inverse = new Element(Enumerable.Range(0, element.Count));
			element.Select((point, index) => inverse[point] = index).ToList();

			return inverse;
		}

		public static implicit operator Element(Cycle cycle)
		{
			var element = Enumerable.Range(0, cycle.Select(c => c.Max()).Max() + 1).ToList();

			cycle.Apply(element);

			return new Element(element);
		}

		internal void ExtendToPoints(int pointMaximum)
		{
			foreach (var point in Enumerable.Range(element.Count, pointMaximum - element.Count))
				element.Add(point);
		}
	}
}
