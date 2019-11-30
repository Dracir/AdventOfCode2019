using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Easing
{
	private const float HALFPI = (float)(Math.PI / 2.0f);
	private const float PI = (float)(Math.PI);

	public enum EasingFunctions
	{
		SmoothStart2, SmoothStart3, SmoothStart4, SmoothStart5,
		SmoothStop2, SmoothStop3, SmoothStop4, SmoothStop5,
		SmoothStep, SmootherStep,
		CircularStart, CircularStop, CicleStartStop,
		ElasticStart, ElasticStop, ElasticStartStop,
		BackStart, BackStop, BackStartStop,
		BounceStart, BounceStop, BounceStartStop
	}

#if UNITY_EDITOR
	public static void DrawGizmos(Func<float, float> ease, Vector3 position, String name = "")
	{
		float y = position.y;
		float xOfff = position.x;

		for (float x = 0; x <= 1; x += 0.01f)
		{
			float v = ease.Invoke(x);
			Gizmos.DrawSphere(new Vector3(xOfff + x, y + v, 0), 0.01f);
		}

		Handles.Label(new Vector3(xOfff, y, 0), name);
		Gizmos.DrawLine(new Vector3(xOfff + 0, y, 0), new Vector3(xOfff + 1, y, 0));
		Gizmos.DrawLine(new Vector3(xOfff + 0, y, 0), new Vector3(xOfff + 0, y + 1, 0));

	}
#endif

	public static Func<float, float> GetFunction(EasingFunctions easing)
	{
		switch (easing)
		{
			case EasingFunctions.SmoothStart2: return SmoothStart2;
			case EasingFunctions.SmoothStart3: return SmoothStart3;
			case EasingFunctions.SmoothStart4: return SmoothStart4;
			case EasingFunctions.SmoothStart5: return SmoothStart5;

			case EasingFunctions.SmoothStop2: return SmoothStop2;
			case EasingFunctions.SmoothStop3: return SmoothStop3;
			case EasingFunctions.SmoothStop4: return SmoothStop4;
			case EasingFunctions.SmoothStop5: return SmoothStop5;

			case EasingFunctions.SmoothStep: return SmoothStep;
			case EasingFunctions.SmootherStep: return SmootherStep;

			case EasingFunctions.CicleStartStop: return CicleStartCicleStop;
			case EasingFunctions.CircularStart: return CircularStart;
			case EasingFunctions.CircularStop: return CircularStop;

			case EasingFunctions.ElasticStart: return ElasticStart;
			case EasingFunctions.ElasticStop: return ElasticStop;
			case EasingFunctions.ElasticStartStop: return ElasticStartStop;

			case EasingFunctions.BackStart: return BackStart;
			case EasingFunctions.BackStop: return BackStop;
			case EasingFunctions.BackStartStop: return BackStartStop;

			case EasingFunctions.BounceStart: return BounceStart;
			case EasingFunctions.BounceStop: return BounceStop;
			case EasingFunctions.BounceStartStop: return BounceStartStop;

			default:
				return (f) => f;
		}
	}

	public static float Evaluate(EasingFunctions easing, float t){
		return GetFunction(easing)(t);
	}

	public static Func<float, float> GetFunc(this EasingFunctions easing)
	{
		return GetFunction(easing);
	}



	public static float SmoothStart2(float t) => t * t;
	public static float SmoothStart3(float t) => t * t * t;
	public static float SmoothStart4(float t) => t * t * t * t;
	public static float SmoothStart5(float t) => t * t * t * t * t;

	public static float SmoothStop2(float t) => 1 - (1 - t) * (1 - t);
	public static float SmoothStop3(float t) => 1 - (1 - t) * (1 - t) * (1 - t);
	public static float SmoothStop4(float t) => 1 - Mathf.Pow((1 - t), 4);
	public static float SmoothStop5(float t) => 1 - Mathf.Pow((1 - t), 5);


	public static float SmoothStep(float t) => t * t * (3f - 2f * t);
	public static float SmootherStep(float t) => t * t * t * (t * (6f * t - 15f) + 10f);


	#region Circular
	public static float CircularStart(float t) => (float)(1 - Math.Sqrt(1 - (t * t)));
	public static float CircularStop(float t) => (float)(Math.Sqrt((2 - t) * t));
	public static float CicleStartCicleStop(float t)
	{
		if (t < 0.5f)
		{
			return (float)(0.5f * (1 - Math.Sqrt(1 - 4 * (t * t))));
		}
		else
		{
			return (float)(0.5f * (Math.Sqrt(-((2 * t) - 3) * ((2 * t) - 1)) + 1));
		}
	}
	#endregion


	#region Elastic
	/// <summary>
	/// Modeled after the damped sine wave y = sin(13pi/2*x)*Math.Pow(2, 10 * (x - 1))
	/// </summary>
	static public float ElasticStart(float t) => (float)(Math.Sin(13 * HALFPI * t) * Math.Pow(2, 10 * (t - 1)));

	/// <summary>
	/// Modeled after the damped sine wave y = sin(-13pi/2*(x + 1))*Math.Pow(2, -10x) + 1
	/// </summary>
	static public float ElasticStop(float t) => (float)(Math.Sin(-13 * HALFPI * (t + 1)) * Math.Pow(2, -10 * t) + 1);

	/// <summary>
	/// Modeled after the piecewise exponentially-damped sine wave:
	/// y = (1/2)*sin(13pi/2*(2*x))*Math.Pow(2, 10 * ((2*x) - 1))      ; [0,0.5)
	/// y = (1/2)*(sin(-13pi/2*((2x-1)+1))*Math.Pow(2,-10(2*x-1)) + 2) ; [0.5, 1]
	/// </summary>
	static public float ElasticStartStop(float t)
	{
		if (t < 0.5f)
		{
			return (float)(0.5f * Math.Sin(13 * HALFPI * (2 * t)) * Math.Pow(2, 10 * ((2 * t) - 1)));
		}
		else
		{
			return (float)(0.5f * (Math.Sin(-13 * HALFPI * ((2 * t - 1) + 1)) * Math.Pow(2, -10 * (2 * t - 1)) + 2));
		}
	}
	#endregion


	#region Back
	/// <summary>
	/// Modeled after the overshooting cubic y = x^3-x*sin(x*pi)
	/// </summary>
	static public float BackStart(float t)
	{
		return (float)(t * t - t * Math.Sin(t * PI));
	}

	/// <summary>
	/// Modeled after overshooting cubic y = 1-((1-x)^3-(1-x)*sin((1-x)*pi))
	/// </summary>	
	static public float BackStop(float t)
	{
		float f = (1 - t);
		return (float)(1 - (f * f - f * Math.Sin(f * PI)));
	}

	/// <summary>
	/// Modeled after the piecewise overshooting cubic function:
	/// y = (1/2)*((2x)^3-(2x)*sin(2*x*pi))           ; [0, 0.5[
	/// y = (1/2)*(1-((1-x)^3-(1-x)*sin((1-x)*pi))+1) ; [0.5, 1]
	/// </summary>
	static public float BackStartStop(float t)
	{
		if (t < 0.5f)
		{
			float f = 2 * t;
			return (float)(0.5f * (f * f * f - f * Math.Sin(f * PI)));
		}
		else
		{
			float f = (1 - (2 * t - 1));
			return (float)(0.5f * (1 - (f * f * f - f * Math.Sin(f * PI))) + 0.5f);
		}
	}
	#endregion


	#region Bounce
	/// <summary>
	/// Makes 4 bounces from small to big bounces.
	/// </summary>
	static public float BounceStart(float t) => 1 - BounceStop(1 - t);


	/// <summary>
	/// Makes 4 bounces from big to small bounces.
	/// </summary>
	static public float BounceStop(float t)
	{
		if (t < 4 / 11.0f)
		{
			return (121 * t * t) / 16.0f;
		}
		else if (t < 8 / 11.0f)
		{
			return (363 / 40.0f * t * t) - (99 / 10.0f * t) + 17 / 5.0f;
		}
		else if (t < 9 / 10.0f)
		{
			return (4356 / 361.0f * t * t) - (35442 / 1805.0f * t) + 16061 / 1805.0f;
		}
		else
		{
			return (54 / 5.0f * t * t) - (513 / 25.0f * t) + 268 / 25.0f;
		}
	}

	/// <summary>
	/// Makes 4 bounces from small to big bounces. [0, 0.5[
	/// Makes 4 bounces from big to small bounces. [0.5, 1]
	/// </summary>
	static public float BounceStartStop(float t)
	{
		if (t < 0.5f)
		{
			return 0.5f * BounceStart(t * 2);
		}
		else
		{
			return 0.5f * BounceStop(t * 2 - 1) + 0.5f;
		}
	}

	#endregion



	public static float Mix(Func<float, float> a, Func<float, float> b, float bWeight, float t) => (1 - bWeight) * a(t) + (bWeight) * b(t);

	public static float CrossFade(Func<float, float> a, Func<float, float> b, float t) => (1 - t) * a(t) + (t) * b(t);
}
