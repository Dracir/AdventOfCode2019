using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class AOCExecutor : MonoBehaviour
{

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
		Part1Task = new Task<string>(() => Day1Main.Part1(input));
		Part2Task = new Task<string>(() => Day1Main.Part2(input));
	}

	void Update()
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
		Debug.Log(Part2Task.Result + " in " + Timer.Elapsed.ToString("mm\\:ss"));
		Debug.Log("Done ! :D");
	}

	private void MoveToTask2()
	{
		Timer.Stop();
		Debug.Log(Part1Task.Result + " in " + Timer.Elapsed.ToString("mm\\:ss"));
		Part1Task.Dispose();
		CurrentStatus = Status.RunningPart2;
		Part2Task.Start();
		Timer.Restart();
	}
}
