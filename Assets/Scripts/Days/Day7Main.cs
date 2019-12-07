using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day7Main
{

	public static int Part1(string inputText) => Run(inputText, RunSequenceWithFeedbackLoop, new int[] { 0, 1, 2, 3, 4 });
	public static int Part2(string inputText) => Run(inputText, RunSequenceWithFeedbackLoop, new int[] { 5, 6, 7, 8, 9 });

	public static int Run(string inputText, Func<int[], int[], int> sequenceRunner, int[] phases)
	{
		var intcode = InputParser.ListOfInts(inputText, ',');
		int max = 0;
		foreach (var phase in GetPermutations(phases, 5))
		{
			var value = sequenceRunner(intcode.ToArray(), phase.ToArray());
			max = Mathf.Max(value, max);
		}
		return max;
	}

	public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
	{
		if (length == 1) return list.Select(t => new T[] { t });
		return GetPermutations(list, length - 1)
			.SelectMany(t => list.Where(o => !t.Contains(o)),
				(t1, t2) => t1.Concat(new T[] { t2 }));
	}


	public static int RunSequence(int[] intcode, int[] phases)
	{
		var compiler = new IntCodeCompiler();
		int input = 0;
		foreach (var phase in phases)
		{
			compiler.Clear();
			compiler.SetInputs(new int[] { phase, input });
			var program = new IntCodeProgram(intcode.ToArray(), 0);
			compiler.Compute(program);
			input = int.Parse(compiler.OutputValue);
		}

		return input;
	}



	public static int RunSequenceWithFeedbackLoop(int[] intcode, int[] phases)
	{
		var compilers = new IntCodeCompiler[]{
			new IntCodeCompiler(phases[0], true),
			new IntCodeCompiler(phases[1], true),
			new IntCodeCompiler(phases[2], true),
			new IntCodeCompiler(phases[3], true),
			new IntCodeCompiler(phases[4], true)
		};

		var programs = new IntCodeProgram[]{
			new IntCodeProgram(intcode.ToArray(),0),
			new IntCodeProgram(intcode.ToArray(),0),
			new IntCodeProgram(intcode.ToArray(),0),
			new IntCodeProgram(intcode.ToArray(),0),
			new IntCodeProgram(intcode.ToArray(),0)
		};


		int input = 0;
		for (int i = 0; i < compilers.Length; i++)
		{
			var compiler = compilers[i];
			var program = programs[i];
			compiler.Clear();
			compiler.SetInputs(new int[] { phases[i], input });
			compiler.Compute(program);
			input = int.Parse(compiler.OutputValue);
		}

		int maxLoops = 9999999;
		while (!programs.Last().IsDone && maxLoops-- > 0)
		{
			for (int i = 0; i < compilers.Length; i++)
			{
				//Debug.Log("Next Amp " + i);
				var compiler = compilers[i];
				var program = programs[i];
				compiler.Clear();
				compiler.SetInputs(new int[] { input, input });
				compiler.Compute(program);
				input = int.Parse(compiler.OutputValue);
			}
		}


		return input;
	}
}
