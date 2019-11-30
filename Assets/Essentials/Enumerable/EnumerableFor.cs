using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnumerableFor<T> : IEnumerable
{

	private IEnumerable<T> _collection;
	private Func<T, IEnumerable> _action;

	public EnumerableFor(IEnumerable<T> collection, Func<T, IEnumerable> action)
	{
		_collection = collection;
		_action = action;
	}

	public IEnumerator GetEnumerator() => new EnumeratorFor<T>(_collection, _action);
}

public class EnumeratorFor<T> : IEnumerator<T>
{
	private IEnumerator<T> _collection;
	private int _currentIndex;
	private int _count;

	private Func<T, IEnumerable> _action;
	private IEnumerator _currentAction;
	private T _currentElement;

	public T Current { get { return _currentElement; } }


	public EnumeratorFor(IEnumerable<T> collection, Func<T, IEnumerable> action)
	{
		_collection = collection.GetEnumerator();
		_count = collection.Count();
		_action = action;
	}


	object IEnumerator.Current => throw new NotImplementedException();

	public void Dispose()
	{
		_collection = null;
		_action = null;
	}

	public bool MoveNext()
	{
		while (true)
		{
			if (_currentAction == null)
			{
				if (_collection.MoveNext())
				{
					_currentElement = _collection.Current;
					_currentAction = _action(_currentElement).GetEnumerator();
					_currentIndex++;
				}
				else
				{
					return false;
				}

			}

			if (_currentAction != null && !_currentAction.MoveNext())
				_currentAction = null;
			else
			{
				return true;
			}
		}
	}

	public void Reset()
	{
	}

	public override string ToString()
	{
		var action = EnumerableUtils.CleanIEnumeratorToString(_action(Current).ToString());
		return $"For each at {_currentIndex} of {_count} total.\n" +
		$"{action}({Current})";
	}
}
