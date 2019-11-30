using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class EnumerableUtils
{

	public static IEnumerable WhenAny(params IEnumerable[] enumerables)
	{
		var itterators = enumerables.Select(x => x.GetEnumerator()).ToArray();
		bool anyDone = false;
		while (!anyDone)
		{
			for (int i = 0; i < enumerables.Length; i++)
			{
				if (!itterators[i].MoveNext())
					anyDone = true;
			}

			yield return null;
		}
	}


	public static IEnumerable WhenAll(params IEnumerable[] enumerables)
	{
		var itterators = enumerables.Select(x => x.GetEnumerator()).ToArray();
		bool anyRunning = true;
		while (anyRunning)
		{
			anyRunning = false;
			for (int i = 0; i < enumerables.Length; i++)
			{
				if (itterators[i].MoveNext())
					anyRunning = true;
			}

			yield return null;
		}
	}

	public static string CleanIEnumeratorToString(string value)
	{
		if (value.Contains("+<"))
			return value.Substring(0, value.LastIndexOf('>')).Replace("+<", ".");
		else
			return value;
	}
}