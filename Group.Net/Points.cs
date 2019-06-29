using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Group.Net
{
	public class Points : IComparable<Points>, IEnumerable<int>
	{
		private readonly IList<int> points;

		public int Count
		{
			get { return points.Count; }
		}

		public int this[int idx]
		{
			get { return points[idx]; }
			set { points[idx] = value; }
		}

		public Points(IEnumerable<int> points)
		{
			this.points = points.ToList();
		}

		public Points(params int[] points)
		{
			this.points = points.ToList();
		}

		public Points(Points other)
			: this(other.points)
		{
		}

		public static implicit operator Points(int point)
		{
			return new Points(new[] { point });
		}

		public IEnumerator<int> GetEnumerator()
		{
			return points.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override int GetHashCode()
		{
			return points.Aggregate(13, (current, point) => (current * 7) + point.GetHashCode());
		}

		public int CompareTo(Points other)
		{
			return points.Zip(other.points, (f, s) => f.CompareTo(s)).SkipWhile(n => n == 0).FirstOrDefault();
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Points) obj) == 0;
		}

		public override string ToString()
		{
			return string.Join(" ", points.Select(point => point.ToString(CultureInfo.CurrentCulture)));
		}
	}
}
