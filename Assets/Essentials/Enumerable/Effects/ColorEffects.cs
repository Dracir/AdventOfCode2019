using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ColorEffects
{

	public static IEnumerable FadeOut(Image image, float duration, Func<float, float> ease) => FadeOut((c) => image.color = c, () => image.color, duration, ease);
	public static IEnumerable FadeOut(SpriteRenderer sr, float duration, Func<float, float> ease) => FadeOut((c) => sr.color = c, () => sr.color, duration, ease);
	public static IEnumerable FadeOut(Text text, float duration, Func<float, float> ease) => FadeOut((c) => text.color = c, () => text.color, duration, ease);
	public static IEnumerable FadeOut(Action<Color> set, Func<Color> getStartColor, float duration, Func<float, float> ease)
	{
		float t = 0;
		var startColor = getStartColor();
		var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

		while (t < duration)
		{
			t += Time.deltaTime;
			set(Color.LerpUnclamped(startColor, endColor, ease(t / duration)));
			yield return null;
		}
		set(endColor);
	}





	public static IEnumerable FadeIn(Image image, float duration, Func<float, float> ease) => FadeIn((c) => image.color = c, () => image.color, duration, ease);
	public static IEnumerable FadeIn(SpriteRenderer sr, float duration, Func<float, float> ease) => FadeIn((c) => sr.color = c, () => sr.color, duration, ease);
	public static IEnumerable FadeIn(Text text, float duration, Func<float, float> ease) => FadeIn((c) => text.color = c, () => text.color, duration, ease);
	public static IEnumerable FadeIn(Action<Color> set, Func<Color> getStartColor, float duration, Func<float, float> ease)
	{
		float t = 0;
		var startColor = getStartColor();
		var endColor = new Color(startColor.r, startColor.g, startColor.b, 1);

		while (t < duration)
		{
			t += Time.deltaTime;
			set(Color.LerpUnclamped(startColor, endColor, ease(t / duration)));
			yield return null;
		}

		set(endColor);
	}




	public static IEnumerable Lerp(Image image, Func<Color> getStartColor, Func<Color> getEndColor, float duration, Func<float, float> ease) => Lerp((c) => image.color = c, getStartColor, getEndColor, duration, ease);
	public static IEnumerable Lerp(SpriteRenderer sr, Func<Color> getStartColor, Func<Color> getEndColor, float duration, Func<float, float> ease) => Lerp((c) => sr.color = c, getStartColor, getEndColor, duration, ease);
	public static IEnumerable Lerp(Text text, Func<Color> getStartColor, Func<Color> getEndColor, float duration, Func<float, float> ease) => Lerp((c) => text.color = c, getStartColor, getEndColor, duration, ease);
	public static IEnumerable Lerp(Action<Color> set, Func<Color> getStartColor, Func<Color> getEndColor, float duration, Func<float, float> ease)
	{
		
		float t = 0;
		var startColor = getStartColor();
		var endColor = getEndColor();

		while (t < duration)
		{
			t += Time.deltaTime;
			set(Color.LerpUnclamped(startColor, endColor, ease(t / duration)));
			yield return null;
		}

		set(endColor);
	}




}
