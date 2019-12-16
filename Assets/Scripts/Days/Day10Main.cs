using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day10Main
{

	public static int Part1(string inputText)
	{
		var grid = InputParser.ParseBoolGrid(inputText.Replace("\r", ""), '\n', '#');
		var best = GetBestLocation(grid);
		Debug.Log($"Best is at {best.x},{best.y}");
		return GetBestLocation(grid).detection;
	}

	public static (int x, int y, int detection) GetBestLocation(bool[,] grid)
	{
		var distances = AsteroidToDistanceGrid(grid);
		int max = 0;
		int locationX = 0;
		int locationY = 0;

		for (int y = 0; y < grid.GetLength(0); y++)
			for (int x = 0; x < grid.GetLength(1); x++)
			{
				max = Mathf.Max(max, distances[y, x]);
				locationX = x;
				locationY = y;
			}
		return (locationX, locationY, max);
	}

	public static int[,] AsteroidToDistanceGrid(bool[,] grid)
	{
		int width = grid.GetLength(1);
		int height = grid.GetLength(0);

		var stations = new int[height, width];

		for (int y = 0; y < height; y++)
			for (int x = 0; x < width; x++)
				if (grid[y, x])
					stations[y, x] = GetNbDetection(grid, y, x);

		return stations;
	}

	private static int GetNbDetection(bool[,] grid, int y, int x)
	{
		var angles = new List<float>();
		for (int y2 = 0; y2 < grid.GetLength(0); y2++)
		{
			for (int x2 = 0; x2 < grid.GetLength(1); x2++)
			{
				if (x2 == x && y2 == y)
					continue;
				if (!grid[y2, x2])
					continue;

				var angle = AngleBetween(x, y, x2, y2);
				if (!angles.Any(a => a == angle))
					angles.Add(angle);
			}
		}
		//if (angles.Count != 0)
		//	Debug.Log($"At ({x},{y}) : " + string.Join(", ", angles));
		return angles.Count;
	}

	public static float AngleBetween(int x1, int y1, int x2, int y2)
	{
		var deplacement = new Vector2Int(x1, y1) - new Vector2Int(x2, y2);
		return (Mathf.Atan2(deplacement.y, deplacement.x) * Mathf.Rad2Deg + 270) % 360;
	}

	public static int Part2(string inputText)
	{
		return Part2(inputText, 28, 8);
	}
	public static int Part2(string inputText, int vaporizorX, int vaporizorY)
	{
		var grid = InputParser.ParseBoolGrid(inputText.Replace("\r", ""), '\n', '#');
		var orderGrid = AsteroidToDestructionOrderGrid(grid, new Vector2Int(vaporizorX, vaporizorY));

		MakeImageForGrid(orderGrid, vaporizorX, vaporizorY);

		for (int y = 0; y < grid.GetLength(0); y++)
			for (int x = 0; x < grid.GetLength(1); x++)
				if (201 == orderGrid[y, x])
					return x * 100 + y;
		return 0;
	}

	private static void MakeImageForGrid(int[,] orderGrid, int vaporizorX, int vaporizorY)
	{
		var w = orderGrid.GetLength(1);
		var h = orderGrid.GetLength(0);
		var image = new Color32[h, w];

		for (int x = 0; x < w; x++)
		{
			for (int y = 0; y < h; y++)
			{
				if (orderGrid[y, x] == 0)
					image[y, x] = Color.black;
				else
					image[y, x] = Color.Lerp(Color.white, Color.red, orderGrid[y, x] / 255f);
			}
		}
		image[vaporizorY, vaporizorX] = Color.green;

		AOCExecutor.ActionForMain.Enqueue(() => Day8Main.ImageToTexturee(image, w, h, AOCUI.Instance.Part1ComputeOutput));
	}

	public static int[,] AsteroidToDestructionOrderGrid(bool[,] grid, Vector2Int vaporiserPosition)
	{
		int width = grid.GetLength(1);
		int height = grid.GetLength(0);

		var stations = new int[height, width];
		var stationAngles = new SortedDictionary<float, SortedDictionary<float, Vector2Int>>();


		grid[vaporiserPosition.y, vaporiserPosition.x] = false;

		for (int y = 0; y < height; y++)
			for (int x = 0; x < width; x++)
				if (grid[y, x])
				{
					var pt = new Vector2Int(x, y);
					var angle = AngleBetween(vaporiserPosition.x, vaporiserPosition.y, x, y);
					var distance = Vector2.Distance(vaporiserPosition, pt);
					if (!stationAngles.ContainsKey(angle))
						stationAngles.Add(angle, new SortedDictionary<float, Vector2Int>());
					stationAngles[angle].Add(distance, pt);
				}

		int index = 0;
		bool changeOccured = true;
		while (changeOccured)
		{
			changeOccured = false;
			foreach (var stationAngle in stationAngles)
			{
				if (stationAngle.Value.Count != 0)
				{
					var nextDestroy = stationAngle.Value.First();
					stationAngle.Value.Remove(nextDestroy.Key);
					if (stations[nextDestroy.Value.y, nextDestroy.Value.x] != 0)
						Debug.LogError($"Deja passé par {nextDestroy.Value.x},{nextDestroy.Value.y}");
					else
						stations[nextDestroy.Value.y, nextDestroy.Value.x] = ++index;
					changeOccured = true;
				}
			}
		}


		return stations;
	}
}
