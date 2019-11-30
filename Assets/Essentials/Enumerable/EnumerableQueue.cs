using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumerableQueue : IEnumerable
{
	private Queue<IEnumerable> Queue = new Queue<IEnumerable>();
	private IEnumerator Current;


	public void Enqueue(IEnumerable enumerable)
	{
		Queue.Enqueue(enumerable);
	}

	public void Enqueue(params IEnumerable[] enumerables)
	{
		for (int i = 0; i < enumerables.Length; i++)
			Queue.Enqueue(enumerables[i]);
	}

	public void Update()
	{
		if (Queue.Count != 0 && Current == null)
		{
			Current = Queue.Dequeue().GetEnumerator();
		}

		if (Current != null && !Current.MoveNext())
			Current = null;
	}

	public bool IsDone() => Queue.Count == 0 && Current == null;

	public void Clear()
	{
		Queue.Clear();
		Current = null;
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
