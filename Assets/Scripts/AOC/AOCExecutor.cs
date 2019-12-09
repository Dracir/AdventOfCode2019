using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Collections.Concurrent;

public class AOCExecutor : MonoBehaviour
{
	public static readonly ConcurrentQueue<Action> ActionForMain = new ConcurrentQueue<Action>();

	Task<string> Part1Task;
	Task<string> Part2Task;
	System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch();

	enum Status { Idle, RunningPart1, RunningPart2, Done };
	private Status CurrentStatus;
	void Start()
	{
		var currentDay = AOCUI.Instance.CurrentDay;
		CreateTaskForDay(currentDay);
		Part1Task.Start();
		Timer.Start();
		CurrentStatus = Status.RunningPart1;
	}

	private void CreateTaskForDay(int currentDay)
	{
		var input = AOCInput.GetInput(currentDay);
		if (currentDay == 1)
		{
			CreateTask(() => Day1Main.Part1(input), () => Day1Main.Part2(input));
			Day1Main.StartPart1ComputeShader(input);

		}
		else if (currentDay == 2)
			CreateTask(() => Day2Main.Part1(input), () => Day2Main.Part2(input));
		else if (currentDay == 3)
			CreateTask(() => Day3Main.Part1(input) + "", () => Day3Main.Part2(input) + "");
		else if (currentDay == 4)
			CreateTask(() => Day4Main.Part1(input) + "", () => Day4Main.Part2(input) + "");
		else if (currentDay == 5)
			CreateTask(() => Day5Main.Part1(input) + "", () => Day5Main.Part2(input) + "");
		else if (currentDay == 6)
			CreateTask(() => Day6Main.Part1(input) + "", () => Day6Main.Part2(input) + "");
		else if (currentDay == 7)
			CreateTask(() => Day7Main.Part1(input) + "", () => Day7Main.Part2(input) + "");
		else if (currentDay == 8)
			CreateTask(() => Day8Main.Part1(input) + "", () => Day8Main.Part2(input) + "");
		else if (currentDay == 9)
			CreateTask(() => Day9Main.Part1(input) + "", () => Day9Main.Part2(input) + "");
	}

	private void CreateTask(Func<string> part1, Func<string> part2)
	{
		Part1Task = new Task<string>(part1);
		Part2Task = new Task<string>(part2);
	}

	void Update()
	{
		CheckThreadedExecution();
		Action action;
		while (ActionForMain.TryDequeue(out action))
		{
			action();
		}
	}

	private void CheckThreadedExecution()
	{
		switch (CurrentStatus)
		{
			case Status.RunningPart1:
				if (Part1Task.IsCompleted)
					MoveToTask2();
				else if (Part1Task.IsFaulted)
				{
					Debug.LogError(Part1Task.Exception);
					MoveToTask2();
				}
				break;

			case Status.RunningPart2:
				if (Part1Task.IsCompleted)
					Done();
				else if (Part1Task.IsFaulted)
				{
					Debug.LogError(Part1Task.Exception);
					Done();
				}
				break;
		}
	}

	private void Done()
	{
		CurrentStatus = Status.Done;
		Debug.Log($"Part 2 result : {Part2Task.Result} . Done in {Timer.Elapsed.ToString("mm\\m\\ ss\\s\\ ffff\\m\\s")}");
		Debug.Log("Done ! :D");
	}

	private void MoveToTask2()
	{
		Timer.Stop();
		Debug.Log($"Part 1 result : {Part1Task.Result} . Done in {Timer.Elapsed.ToString("mm\\m\\ ss\\s\\ ffff\\m\\s")}");
		Part1Task.Dispose();
		CurrentStatus = Status.RunningPart2;
		Timer.Restart();
		Part2Task.Start();
	}
}
