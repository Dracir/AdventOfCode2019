using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

public class Day2Main
{

	public static string Part1(string inputText)
	{
		var intcode = InputParser.ListOfInts(inputText, ',');
		intcode[1] = 12;
		intcode[2] = 2;

		return Compute(intcode, 0)[0] + "";
	}
	
	public static int[] Compute(int[] intcode, int pointer)
	{
		while (intcode[pointer] != 99)
		{
			intcode = ComputeStep(intcode, pointer);
			pointer += 4;
		}
		return intcode;
	}

	public static int[] ComputeStep(int[] intcode, int pointer)
	{
		int instruction = intcode[pointer];
		if (instruction == 99)
			return intcode;
		else if (instruction == 1 || instruction == 2)
		{
			var registerA = intcode[intcode[pointer + 1]];
			var registerB = intcode[intcode[pointer + 2]];
			var result = (instruction == 1) ? registerA + registerB : registerA * registerB;
			intcode[intcode[pointer + 3]] = result;
		}

		return intcode;
	}


	public static string Part2(string inputText)
	{
		var intcode = InputParser.ListOfInts(inputText, ',');
		var tasks = new List<Task<Tuple<int, int, int>>>();

		for (int noun = 0; noun < 99; noun++)
			for (int verb = 0; verb < 99; verb++)
			{
				var newTask = MakeTask(intcode, noun, verb);
				tasks.Add(newTask);
				newTask.Start();
			}

		while (tasks.Count != 0)
		{
			var doneTasks = tasks.Where(x => x.IsCompleted).ToArray();
			foreach (var doneTask in doneTasks)
			{
				if (doneTask.Result.Item3 == 19690720)
					return (100 * doneTask.Result.Item1 + doneTask.Result.Item2) + "";
				else
					tasks.Remove(doneTask);
			}
			Thread.Sleep(100);
		}


		return "2";
	}

	private static Task<Tuple<int, int, int>> MakeTask(int[] intcode, int noun, int verb)
	{
		var newIntcode = intcode.ToArray();
		newIntcode[1] = noun;
		newIntcode[2] = verb;
		return new Task<Tuple<int, int, int>>(() => new Tuple<int, int, int>(noun, verb, Compute(newIntcode, 0)[0]));
	}
}
