using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnumerableSequence : IEnumerable
{
	private List<IEnumerable> _enumerables = new List<IEnumerable>();
	private IEnumerator _current;
	private int _currentIndex = -1;

	/// Whether the sequence repeat when it' s done a full cycle.
	private bool _repeating;

	private static int INDEX_DONE = -2;

#if UNITY_EDITOR
	[SerializeField, TextArea]
	private string CurrentEnumerator;
#endif

	public EnumerableSequence(bool repeating = false)
	{
		_repeating = repeating;
	}

	public void Add(IEnumerable enumerable)
	{
		_enumerables.Add(enumerable);
	}

	public void Add(params IEnumerable[] enumerables)
	{
		for (int i = 0; i < enumerables.Length; i++)
			_enumerables.Add(enumerables[i]);
	}



	public void Update()
	{

#if UNITY_EDITOR
		if (_enumerables.Count == 0)
		{
			Debug.LogError($"EnumerableSequence {ToString()} cant be empty!");
			return;
		}
#endif

		if (_currentIndex == INDEX_DONE) return;

		while (true)
		{
			if (_enumerables.Count != 0 && _current == null)
			{
				if (_repeating)
				{
					_currentIndex = (_currentIndex + 1) % _enumerables.Count;
					_current = _enumerables[_currentIndex].GetEnumerator();
				}
				else
				{
					_currentIndex++;
					if (_currentIndex >= _enumerables.Count)
						_currentIndex = INDEX_DONE;
					else
						_current = _enumerables[_currentIndex].GetEnumerator();
				}
			}

			if (_current != null && !_current.MoveNext())
				_current = null;
			else
			{
#if UNITY_EDITOR
				if (_current != null)
					CurrentEnumerator = EnumerableUtils.CleanIEnumeratorToString(_current.ToString());
#endif
				return;
			}


		}
	}

	public bool IsDone()
	{
		return _currentIndex == INDEX_DONE;
	}

	public void Clear()
	{
		_enumerables.Clear();
		_current = null;
		_currentIndex = 0;
	}

	public void Reset()
	{
		_current = null;
		_currentIndex = -1;
	}

	public IEnumerator GetEnumerator()
	{
		while (!IsDone())
		{
			Update();
			yield return null;
		}

	}
}
