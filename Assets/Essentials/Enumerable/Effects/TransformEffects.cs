using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TransformEffects
{
	public static IEnumerable SetLocalPosition(Transform transform, Func<Vector3> getStartValue, Func<Vector3> getEndValue, float duration, Func<float, float> ease)
	=> VectorEffects.Lerp((v) => transform.localPosition = v, getStartValue, getEndValue, duration, ease);
	
	public static IEnumerable SetPosition(Transform transform, Func<Vector3> getStartValue, Func<Vector3> getEndValue, float duration, Func<float, float> ease)
	=> VectorEffects.Lerp((v) => transform.position = v, getStartValue, getEndValue, duration, ease);
}
