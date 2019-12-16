using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day12Main
{

	public static int Part1(string inputText)
	{
		var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-13, 14, -7)   ,Vector3Int.zero),
				(new Vector3Int(-18, 9, 0) ,Vector3Int.zero),
				(new Vector3Int(0, -3, -3)   ,Vector3Int.zero),
				(new Vector3Int(-15, 3, -13)   ,Vector3Int.zero)
			};

		for (int i = 0; i < 1000; i++)
			input = Day12Main.GravityStep(input);

		var pot = Day12Main.TotalEnergy(input);
		return pot;
	}

	private static bool AreEqual(Vector3Int a, Vector3Int b) => a.x == b.x && a.y == b.y && a.z == b.z;

	private static bool AreEqual((Vector3Int, Vector3Int)[] input, (Vector3Int, Vector3Int)[] currentState)
	{
		bool equal = true;
		for (int i = 0; i < currentState.Length; i++)
		{
			var a = input[i].Item1;
			var b = currentState[i].Item1;
			if (!a.Equals(b))
				equal = false;
		}
		return equal;
	}

	public static (Vector3Int, Vector3Int)[] GravityStep((Vector3Int, Vector3Int)[] input)
	{
		var output = new (Vector3Int, Vector3Int)[input.Length];
		Array.Copy(input, output, input.Length);

		for (int i = 0; i < output.Length; i++)
			for (int j = i + 1; j < output.Length; j++)
			{
				if (i == j) continue;
				output[i].Item2 += Gravity(output[i].Item1, output[j].Item1);
				output[j].Item2 += Gravity(output[j].Item1, output[i].Item1);
			}

		for (int i = 0; i < output.Length; i++)
			output[i].Item1 += output[i].Item2;

		//Print(output);

		return output;
	}

	public static int TotalEnergy((Vector3Int, Vector3Int)[] moons)
	{
		return moons.Sum(
			moon =>
			{
				var pot = Mathf.Abs(moon.Item1.x) + Mathf.Abs(moon.Item1.y) + Mathf.Abs(moon.Item1.z);
				var kin = Mathf.Abs(moon.Item2.x) + Mathf.Abs(moon.Item2.y) + Mathf.Abs(moon.Item2.z);
				return pot * kin;
			}
		);
	}

	public static void Print((Vector3Int, Vector3Int)[] moons)
	{
		var str = "";
		foreach (var moon in moons)
		{
			str += $"pos= {moon.Item1}, vel= {moon.Item2}\n";
		}
		Debug.Log(str);
	}

	public static Vector3Int Gravity(Vector3Int moon1, Vector3Int moon2) => new Vector3Int(Gravity(moon1.x, moon2.x), Gravity(moon1.y, moon2.y), Gravity(moon1.z, moon2.z));
	public static int Gravity(int p1, int p2) => (p1 == p2) ? 0 : ((p1 < p2) ? 1 : -1);


	

	public static int Part2(string inputText)
	{
		var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-13, 14, -7)   ,Vector3Int.zero),
				(new Vector3Int(-18, 9, 0) ,Vector3Int.zero),
				(new Vector3Int(0, -3, -3)   ,Vector3Int.zero),
				(new Vector3Int(-15, 3, -13)   ,Vector3Int.zero)
			};

		return Part2(input);
	}

	public static int Part2((Vector3Int, Vector3Int)[] input)
	{
		int step = 1;
		var currentState = GravityStep(input);
		var nbSteps = new int[4];
		while (!AreEqual(input, currentState) && step++ < int.MaxValue && step < 1000000)
		{
			currentState = GravityStep(currentState);
			for (int moonIndex = 0; moonIndex < 4; moonIndex++)
				if (nbSteps[moonIndex] == 0 && AreEqual(currentState[moonIndex].Item1, input[moonIndex].Item1))
					nbSteps[moonIndex] = step + 1;
		}
		Debug.Log($"{nbSteps[0]} * {nbSteps[1]} * {nbSteps[2]} * {nbSteps[3]}");

		return LCM(nbSteps[0], LCM(nbSteps[1], LCM(nbSteps[2], nbSteps[3])));
	}

	public static int[] GravityStep(int[] moons)
	{
		/*var output = new (Vector3Int, Vector3Int)[input.Length];
		Array.Copy(input, output, input.Length);

		for (int i = 0; i < output.Length; i++)
			for (int j = i + 1; j < output.Length; j++)
			{
				if (i == j) continue;
				output[i].Item2 += Gravity(output[i].Item1, output[j].Item1);
				output[j].Item2 += Gravity(output[j].Item1, output[i].Item1);
			}

		for (int i = 0; i < output.Length; i++)
			output[i].Item1 += output[i].Item2;

		//Print(output);

		return output;*/
		return moons;
	}

	static int GCD(int a, int b)
	{
		if (a % b == 0) return b;
		return GCD(b, a % b);
	}

	static int LCM(int a, int b)
	{
		return a * b / GCD(a, b);
	}
}
