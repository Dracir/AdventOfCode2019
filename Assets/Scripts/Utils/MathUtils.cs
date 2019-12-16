using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
	public static int[] GetIntArray(int num)
	{
		List<int> listOfInts = new List<int>();
		while (num > 0)
		{
			listOfInts.Add(num % 10);
			num = num / 10;
		}
		listOfInts.Reverse();
		return listOfInts.ToArray();
	}

	public static BoundsInt FindBound(List<Vector2Int> points)
	{
		int minX = int.MaxValue;
		int minY = int.MaxValue;
		int maxX = int.MinValue;
		int maxY = int.MinValue;

		foreach (var pt in points)
		{
			minX = Mathf.Min(minX, pt.x);
			maxX = Mathf.Max(maxX, pt.x);
			minY = Mathf.Min(minY, pt.y);
			maxY = Mathf.Max(maxY, pt.y);
		}

		return new BoundsInt(minX, minY, -1, maxX - minX, maxY - minY, 2);
	}
}
