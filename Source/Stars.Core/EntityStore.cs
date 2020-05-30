using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public interface IEntity
	{
		int Id { get; set; }
	}

	public class EntityStore<T> : IEnumerable<T>, IList<T>
		where T : IEntity
	{
		private readonly List<T> items = new List<T>();

		public int Count => ((ICollection<T>)items).Count;

		public bool IsReadOnly => ((ICollection<T>)items).IsReadOnly;

		public T this[int index] { get => ((IList<T>)items)[index]; set => ((IList<T>)items)[index] = value; }

		public EntityStore()
		{
		}

		public EntityStore(IEnumerable<T> collection)
		{
			AddRange(collection);
		}

		public void Add(T item)
		{
			item.Id = 0;
			items.Add(item);
			item.Id = items.Max(i => i.Id) + 1;
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				Add(item);
			}
		}

		public bool Remove(T item)
		{
			// TODO: Search for entity id?
			return items.Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)items).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)items).GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return ((IList<T>)items).IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			((IList<T>)items).Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			((IList<T>)items).RemoveAt(index);
		}

		public void Clear()
		{
			((ICollection<T>)items).Clear();
		}

		public bool Contains(T item)
		{
			return ((ICollection<T>)items).Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			((ICollection<T>)items).CopyTo(array, arrayIndex);
		}
	}
}
