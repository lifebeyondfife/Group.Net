using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Group.Net
{
	public class Image<T> : IComparable<Image<T>>, IEnumerable<T> where T : IComparable<T>
	{
		private readonly IList<T> image;

		public int Count
		{
			get { return image.Count; }
		}

		public T this[int index]
		{
			get { return image[index]; }
			set { image[index] = value; }
		}

		public Image(IList<T> image)
		{
			this.image = image;
		}

		public Image(Image<T> other)
		{
			image = new List<T>(other.image);
		}

		public override int GetHashCode()
		{
			return image.Aggregate(13, (current, point) => (current * 7) + point.GetHashCode());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int CompareTo(Image<T> other)
		{
			return image.Zip(other.image, (f, s) => f.CompareTo(s)).SkipWhile(n => n == 0).FirstOrDefault();
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Image<T>) obj) == 0;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return image.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join(" ", image.Select(n => n.ToString()));
		}
	}
}
