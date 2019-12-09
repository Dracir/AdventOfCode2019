using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Day5Main
{

	public static string Part1(string inputText)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');
		var compiler = new IntCodeCompiler(1);
		var program = new IntCodeProgram(intcode, 0);

		compiler.Compute(program);
		IntCodeToBasic.Convert(program, "D5Part1HumainCode.txt");

		return compiler.OutputConcat;
	}

	public static string Part2(string inputText)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');

		IntCodeToBasic.ConvertWhileRunning(new IntCodeCompiler(5), new IntCodeProgram(intcode.ToArray(), 0), "D5Part2HumainCode.txt", true);

		var compiler = new IntCodeCompiler(5);
		compiler.Compute(new IntCodeProgram(intcode, 0));

		return compiler.OutputConcat;
	}
}
