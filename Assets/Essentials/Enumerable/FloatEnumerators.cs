using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatEnumerators
{

	public static IEnumerator Oscillate(Action<float> applyTo, float frequency, float amplitude, float offset)
	{
		while (true)
		{
			var o = amplitude * Mathf.Sin(frequency * Time.time) + offset;
			applyTo(o);
			yield return null;

		}
	}
}


