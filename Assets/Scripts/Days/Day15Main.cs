using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Day15Main
{

	public static int Part1(string inputText)
	{
		var program = new IntCodeProgram(InputParser.ListOfLongs(inputText, ','), 0, 0);
		var robot = new RepairDroid(program);
		robot.FindOxygenSystem();
		return 1;
	}

	public static int Part2(string inputText)
	{
		return 2;
	}
}
