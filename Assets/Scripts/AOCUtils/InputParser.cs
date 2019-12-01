using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class InputParser
{
	public static int[] ListOfInts(string input) => input.Split('\n').Select(x => int.Parse(x)).ToArray();

}
