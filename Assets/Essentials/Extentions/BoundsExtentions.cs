using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class BoundsExtentions
{
	public static float Left(this Bounds bounds)
	{
		return bounds.min.x;
	}
	public static float Right(this Bounds bounds)
	{
		return bounds.max.x;
	}
	public static float Top(this Bounds bounds)
	{
		return bounds.max.y;
	}
	public static float Bottom(this Bounds bounds)
	{
		return bounds.min.y;
	}
}
