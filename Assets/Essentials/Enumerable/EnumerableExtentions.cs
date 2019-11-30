using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EnumerableExtentions
{

	public static IEnumerable LambdaToEnumerable(Action doAction)
	{
		doAction();
		yield return null;
	}

	public static IEnumerable Squence(this IEnumerable enumerable, params IEnumerable[] otherEnumerables)
	{
		int index = 0;
		IEnumerator current = enumerable.GetEnumerator();

		while (index < otherEnumerables.Length || current != null)
		{
			if (index < otherEnumerables.Length && current == null)
			{
				current = otherEnumerables[index].GetEnumerator();
				index++;
			}

			if (current != null)
			{
				if (!current.MoveNext())
					current = null;
				yield return null;
			}
		}
	}

	public static IEnumerable Squence(params IEnumerable[] enumerables)
	{
		int index = 0;
		IEnumerator current = null;

		while (index < enumerables.Length || current != null)
		{
			if (index < enumerables.Length && current == null)
			{
				current = enumerables[index].GetEnumerator();
				index++;
			}

			if (current != null)
			{
				if (!current.MoveNext())
					current = null;
				yield return null;
			}
		}
	}

}
