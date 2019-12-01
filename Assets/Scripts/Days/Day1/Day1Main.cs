using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Day1Main
{

	public static string Part1(string inputText)
	{
		var values = InputParser.ListOfInts(inputText);
		return values.Select(x => FuelRequirement(x)).Sum() + "";
	}

	public static int FuelRequirement(int mass)
	{
		return (int)(mass / 3f) - 2;
	}

	public static string Part2(string inputText)
	{	
		var values = InputParser.ListOfInts(inputText);
		return values.Select(x => RecursiveFuelRequirement(x)).Sum() + "";
	}

	public static int RecursiveFuelRequirement(int mass)
	{
		var fuel = (int)(mass / 3f) - 2;
		if (fuel <= 0)
			return 0;
		else
			return fuel + RecursiveFuelRequirement(fuel);
	}
}
