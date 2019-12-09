using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day7Main
{

	public static long Part1(string inputText) => Run(inputText, RunSequence, new long[] { 0, 1, 2, 3, 4 });
	public static long Part2(string inputText) => Run(inputText, RunSequenceWithFeedbackLoop, new long[] { 5, 6, 7, 8, 9 });

	public static long Run(string inputText, Func<long[], long[], long> sequenceRunner, long[] phases)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');
		long max = 0;
		foreach (var phase in GetPermutations(phases, 5))
		{
			var value = sequenceRunner(intcode.ToArray(), phase.ToArray());
			max = Math.Max(value, max);
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


	public static long RunSequence(long[] intcode, long[] phases)
	{
		var compiler = new IntCodeCompiler();
		long input = 0;
		foreach (var phase in phases)
		{
			compiler.Clear();
			compiler.SetInputs(new long[] { phase, input });
			var program = new IntCodeProgram(intcode.ToArray(), 0);
			compiler.Compute(program);
			input = compiler.OutputValue;
		}

		return input;
	}



	public static long RunSequenceWithFeedbackLoop(long[] intcode, long[] phases)
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


		long input = 0;
		for (int i = 0; i < compilers.Length; i++)
		{
			var compiler = compilers[i];
			var program = programs[i];
			Debug.Log($"(First Pass)Running Amp {i} at pointer {program.Pointer} with input {input}");
			compiler.Clear();
			compiler.SetInputs(new long[] { phases[i], input });
			programs[i] = compiler.Compute(program);
			input = compiler.OutputValue;
		}

		int maxLoops = 20;
		while (!programs.Last().IsDone && maxLoops-- > 0)
		{
			for (int i = 0; i < compilers.Length; i++)
			{
				var compiler = compilers[i];
				var program = programs[i];
				Debug.Log($"Running Amp {i} at pointer {program.Pointer} with input {input}");
				compiler.Clear();
				compiler.SetInputs(new long[] { input });
				programs[i] = compiler.Compute(program);
				input = compiler.OutputValue;
			}
		}


		return input;
	}
}
