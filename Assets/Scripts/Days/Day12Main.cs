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

	public static string Part2(string inputText)
	{
		return "2";
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
}
