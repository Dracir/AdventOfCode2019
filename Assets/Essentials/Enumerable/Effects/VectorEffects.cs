using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class VectorEffects
{

	public static IEnumerable Lerp(Action<Vector3> set, Func<Vector3> getStartValue, Func<Vector3> getEndValue, float duration, Func<float, float> ease)
	{

		float t = 0;
		var startValue = getStartValue();
		var endValue = getEndValue();

		while (t < duration)
		{
			t += Time.deltaTime;
			set(Vector3.LerpUnclamped(startValue, endValue, ease(t / duration)));
			yield return null;
		}

		set(endValue);
	}

}