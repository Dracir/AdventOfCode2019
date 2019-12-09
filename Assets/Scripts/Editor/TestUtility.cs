using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public static class TestUtility
{
	public static void AreEquals(Vector2[,] expected, Vector2[,] actual, float epsilon)
	{

		if (expected.GetLength(0) != actual.GetLength(0) || expected.GetLength(1) != actual.GetLength(1))
		{
			Assert.Fail($"Size differ\nExpected:\t[{expected.GetLength(0)},{expected.GetLength(1)}]\nBut was:\t[{actual.GetLength(0)},{actual.GetLength(1)}]");
			return;
		}


		for (int x = 0; x < expected.GetLength(0); x++)
			for (int y = 0; y < expected.GetLength(1); y++)
			{
				if (Mathf.Abs(expected[x, y].x - actual[x, y].x) >= epsilon || Mathf.Abs(expected[x, y].y - actual[x, y].y) >= epsilon)
				{
					Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expected.GetLength(0)},{expected.GetLength(1)}]>\n"
										+ $"Values differ at index [{x},{y}]\nExpected:\t{expected[x, y].x},{expected[x, y].y}\nBut was:\t{actual[x, y].x},{actual[x, y].y}");
				}

			}
	}

	public static void AreEqual(int[] expected, int[] actual)
	{

		if (expected.Length != actual.Length)
		{
			Assert.Fail($"Size differ\nExpected:\t[{expected.Length}]\nBut was:\t[{actual.Length}]");
			return;
		}

		for (int x = 0; x < expected.Length; x++)
			if (expected[x] != actual[x])
			{
				Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expected.Length}]>\n"
									+ $"Values differ at index [{x}]\nExpected:\t{string.Join(",", expected)}\nBut was:\t{string.Join(",", actual)}");
			}
	}

	public static void AreEqual(long[] expected, long[] actual)
	{

		if (expected.Length != actual.Length)
		{
			Assert.Fail($"Size differ\nExpected:\t[{expected.Length}]\nBut was:\t[{actual.Length}]");
			return;
		}

		for (int x = 0; x < expected.Length; x++)
			if (expected[x] != actual[x])
			{
				Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expected.Length}]>\n"
									+ $"Values differ at index [{x}]\nExpected:\t{string.Join(",", expected)}\nBut was:\t{string.Join(",", actual)}");
			}
	}
}
