using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
		return Part2(inputText, 30, 30);
	}
	public static int Part2(string inputText, int vaporizorX, int vaporizorY)
	{
		var grid = InputParser.ParseBoolGrid(inputText.Replace("\r", ""), '\n', '#');
		grid[vaporizorY, vaporizorX] = false;
		var orderGrid = AsteroidToDestructionOrderGrid(grid, new Vector2Int(vaporizorX, vaporizorY));

		int max = 0;
		int locationX = 0;
		int locationY = 0;

		for (int y = 0; y < grid.GetLength(0); y++)
			for (int x = 0; x < grid.GetLength(1); x++)
			{
				if (max < orderGrid[y, x])
				{
					max = orderGrid[y, x];
					Debug.Log($"{max} at {x},{y}");
					locationX = x;
					locationY = y;
				}

			}

		return locationX * 100 + locationY;
	}

	public static int[,] AsteroidToDestructionOrderGrid(bool[,] grid, Vector2Int vaporiserPosition)
	{
		int width = grid.GetLength(1);
		int height = grid.GetLength(0);

		var stations = new int[height, width];
		var stationAngles = new SortedDictionary<float, SortedDictionary<float, Vector2Int>>();

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
					stations[nextDestroy.Value.y, nextDestroy.Value.x] = ++index;
					changeOccured = true;
				}
			}
		}


		return stations;
	}
}
