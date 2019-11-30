using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enumerables
{
	public static IEnumerable DoForTime(IEnumerator action, float duration)
	{
		float t = 0;
		while (t < duration)
		{
			action.MoveNext();
			t += Time.deltaTime;
			yield return null;
		}
	}

	public static IEnumerable Wait(float seconds)
	{
		float t = 0;
		while (t < seconds)
		{
			t += Time.deltaTime;
			yield return null;
		}
	}
	
	public static IEnumerable WaitForAnyInput(params KeyCode[] keyCodes)
	{
		while (!AnyDown(keyCodes))
			yield return null;
	}

	public static bool AnyDown(KeyCode[] keyCodes)
	{
		for (int i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
				return true;
		}

		return false;
	}
}
