using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;
using System;

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


	public static void AreEqual(int[,] expected, int[,] actual) => AreEqual<int>(expected, actual, (a, b) => a == b, (a) => a.ToString());

	public static void AreEqual<T>(T[,] expected, T[,] actual, Func<T, T, bool> equalityFonction, Func<T, string> t2String)
	{

		if (expected.GetLength(0) != actual.GetLength(0) || expected.GetLength(1) != actual.GetLength(1))
		{
			Assert.Fail($"Size differ\nExpected:\t[{expected.GetLength(0)},{expected.GetLength(1)}]\nBut was:\t[{actual.GetLength(0)},{actual.GetLength(1)}]");
			return;
		}


		for (int x = 0; x < expected.GetLength(0); x++)
			for (int y = 0; y < expected.GetLength(1); y++)
			{
				if (!equalityFonction(expected[x, y], actual[x, y]))
				{
					if (actual.GetLength(0) < 10 && expected.GetLength(1) < 20)
					{
						var expectedGrid = GridToPrintableString(expected);
						var actualGrid = GridToPrintableString(actual);
						Assert.Fail($"Expected\n{expectedGrid}\nActual\n{actualGrid}");
					}
					else
						Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expected.GetLength(0)},{expected.GetLength(1)}]>\n"
											+ $"Values differ at index [{x},{y}]\nExpected:\t{t2String(expected[x, y])}\nBut was:\t{t2String(actual[x, y])}");
				}

			}
	}

	public static string GridToPrintableString<T>(T[,] grid)
	{
		var print = "";
		for (int y = 0; y < grid.GetLength(0); y++)
		{
			for (int x = 0; x < grid.GetLength(1); x++)
				print += grid[y, x].ToString() + "\t";
			print += "\n";
		}

		return print;
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
