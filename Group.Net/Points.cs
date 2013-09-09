using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Group.Net
{
	public class Points<T> : IComparable<Points<T>>, IEnumerable<T> where T : IComparable<T>
	{
		private readonly IList<T> points;

		public int Count
		{
			get { return points.Count; }
		}

		public T this[int index]
		{
			get { return points[index]; }
			set { points[index] = value; }
		}

		public Points(IList<T> points)
		{
			this.points = points;
		}

		public Points(Points<T> other)
		{
			points = new List<T>(other.points);
		}

		public override int GetHashCode()
		{
			return points.Aggregate(13, (current, point) => (current * 7) + point.GetHashCode());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int CompareTo(Points<T> other)
		{
			return points.Zip(other.points, (f, s) => f.CompareTo(s)).SkipWhile(n => n == 0).FirstOrDefault();
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Points<T>) obj) == 0;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return points.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join(" ", points.Select(n => n.ToString()));
		}
	}
}
