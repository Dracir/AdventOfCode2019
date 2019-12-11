using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day11Main
{
	public static int Part1(string inputText)
	{
		var program = new IntCodeProgram(InputParser.ListOfLongs(inputText, ','), 0, 0);
		var robot = new EHPR(program);
		robot.Run();

		return robot.Painting.Count;
	}

	public static int Part2(string inputText)
	{
		var program = new IntCodeProgram(InputParser.ListOfLongs(inputText, ','), 0, 0);
		var robot = new EHPR(program);
		robot.Painting.Add(new Vector2Int(0, 0), new List<long>(new long[] { 1 }));
		robot.Run();

		AOCExecutor.ActionForMain.Enqueue(() => WriteToFilePainting(robot.Painting, "Day11_Part2.txt"));
		var bounds = MathUtils.FindBound(robot.Painting.Keys.ToList());
		int xOff = -bounds.xMin;
		int yOff = -bounds.yMin;
		Debug.Log(bounds);
		var image = new int[bounds.size.y + 1, bounds.size.x + 1];
		foreach (var pt in robot.Painting)
		{
			if (pt.Key.x < bounds.min.x || pt.Key.x > bounds.max.x || pt.Key.y < bounds.min.y || pt.Key.y > bounds.max.y)
				Debug.Log($"Contains pas {pt.Key}");
			if (pt.Value.Count == 0)
				continue;
			var value = pt.Value.Last();
			image[bounds.size.y - (pt.Key.y + yOff), pt.Key.x + xOff] = (int)value;
		}

		AOCExecutor.ActionForMain.Enqueue(() => Day8Main.ImageToTexturee(image, bounds.size.x + 1, bounds.size.y + 1, AOCUI.Instance.Part1ComputeOutput));

		return 2;
	}

	private static void WriteToFilePainting(Dictionary<Vector2Int, List<long>> painting, string fileName)
	{
		var output = $"{painting.Keys.Count} panels painted in {painting.Sum(x => x.Value.Count)} Steps.\n";
		foreach (var item in painting)
			output += $"{item.Key}: {string.Join(", ", item.Value)}\n";
		AOCInput.WriteToFile(fileName, output);
	}
}
