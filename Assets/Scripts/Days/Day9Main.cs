using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Day9Main
{

	public static long Part1(string inputText)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');

		IntCodeToBasic.ConvertWhileRunning(new IntCodeCompiler(1), new IntCodeProgram(intcode.ToArray(), 0), "D9Part1_HumainCode.txt",true);

		var compiler = new IntCodeCompiler(1);
		compiler.Compute(new IntCodeProgram(intcode, 0));

		return compiler.OutputValue;
	}

	public static int Part2(string inputText)
	{
		return 2;
	}
}
