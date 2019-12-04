using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class InputParser
{
	public static int[] ListOfInts(string input) => input.Split('\n').Select(x => int.Parse(x)).ToArray();
	public static int[] ListOfInts(string input, char separator) => input.Split(separator).Select(x => int.Parse(x)).ToArray();

	/* a range in the format of "number-number" for exemple 130254-678275*/
	public static Vector2Int ParseRange(string input)
	{
		var split = input.Split('-');
		return new Vector2Int(Int32.Parse(split[0]), Int32.Parse(split[1]));
	}
}