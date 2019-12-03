using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static InputParser;

public class Day3Main
{

	public static int Part1(string inputText)
	{
		var wires = ParsePathList(inputText);
		return DistanceClosestToCentralPort(wires[0], wires[1]);
	}


	public static int DistanceClosestToCentralPort(DirectionPath wireA, DirectionPath wireB)
	{
		var bounds = GetBounds(wireA, wireB);
		var grid = new GridValue[bounds.size.x, bounds.size.y];
		var centralPortPosition = new Vector2Int(Mathf.Abs(bounds.min.x), Mathf.Abs(bounds.min.y));
		grid[centralPortPosition.x, centralPortPosition.y] = GridValue.CentralPort;

		grid = FillGrid(grid, wireA, GridValue.A, GridValue.B, centralPortPosition);
		grid = FillGrid(grid, wireB, GridValue.B, GridValue.A, centralPortPosition);

		//var str = "";
		var minDistance = Int32.MaxValue;
		for (int y = grid.GetLength(1) - 1; y >= 0; y--)
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				//str += GridValueToChar(grid[x, y]);
				if (grid[x, y] == GridValue.AB)
					minDistance = Mathf.Min(minDistance, Mathf.Abs(x - centralPortPosition.x) + Mathf.Abs(y - centralPortPosition.y));
			}
			//str += "\n";
		}

		//Debug.Log(str);

		return minDistance;
	}

	private static char GridValueToChar(GridValue value)
	{
		switch (value)
		{
			case GridValue.A: return 'a';
			case GridValue.B: return 'b';
			case GridValue.AB: return 'X';
			case GridValue.CentralPort: return 'O';
			default: return '.';
		}
	}

	private static GridValue[,] FillGrid(GridValue[,] grid, DirectionPath path, GridValue a, GridValue b, Vector2Int portPosition, int[,] gridDistance = null)
	{
		int x = portPosition.x;
		int y = portPosition.y;
		int distance = 0;
		foreach (var wire in path.Nodes)
		{
			var displacement = NodeToDeplacement(wire.Direction, wire.Distance);
			if (IsHorizontal(wire.Direction))
				for (int i = 0; i < wire.Distance; i++)
				{
					grid[x, y] = ValueToPutOnGrid(grid[x, y], x, y, a, b);
					if (gridDistance != null && gridDistance[x, y] == 0)
						gridDistance[x, y] = distance;
					x += displacement;
					distance++;
				}
			else
				for (int i = 0; i < wire.Distance; i++)
				{
					grid[x, y] = ValueToPutOnGrid(grid[x, y], x, y, a, b);
					if (gridDistance != null && gridDistance[x, y] == 0)
						gridDistance[x, y] = distance;
					y += displacement;
					distance++;
				}
		}

		return grid;
	}

	private static GridValue ValueToPutOnGrid(GridValue valueOnGrid, int x, int y, GridValue a, GridValue b)
	{
		if (valueOnGrid == GridValue.Empty)
			return a;
		else if (valueOnGrid == b)
			return GridValue.AB;
		else
			return valueOnGrid;
	}

	public static bool IsHorizontal(char direction) => direction == 'L' || direction == 'R';

	private enum GridValue { Empty, A, B, AB, CentralPort }

	private static BoundsInt GetBounds(DirectionPath wireA, DirectionPath wireB)
	{
		var minsA = FindMins(wireA, (min, x) => Mathf.Min(min, x));
		var minsB = FindMins(wireB, (min, x) => Mathf.Min(min, x));
		var maxsA = FindMins(wireA, (min, x) => Mathf.Max(min, x));
		var maxsB = FindMins(wireB, (min, x) => Mathf.Max(min, x));
		var minX = Mathf.Min(minsA.selectedX, minsB.selectedX);
		var minY = Mathf.Min(minsA.selectedY, minsB.selectedY);
		var maxX = Mathf.Max(maxsA.selectedX, maxsB.selectedX);
		var maxY = Mathf.Max(maxsA.selectedY, maxsB.selectedY);

		return new BoundsInt(minX, minY, 0, maxX - minX + 1, maxY - minY + 1, 0);
	}

	private static (int selectedX, int selectedY) FindMins(DirectionPath wire, Func<int, int, int> selectionFunction)
	{
		int selectedX = 0;
		int selectedY = 0;
		int x = 0, y = 0;
		foreach (var node in wire.Nodes)
		{
			if (node.Direction == 'L')
				x -= node.Distance;
			else if (node.Direction == 'R')
				x += node.Distance;
			else if (node.Direction == 'D')
				y -= node.Distance;
			else if (node.Direction == 'U')
				y += node.Distance;

			selectedX = selectionFunction(selectedX, x);
			selectedY = selectionFunction(selectedY, y);
		}
		return (selectedX, selectedY);
	}

	private static int NodeToDeplacement(char direction, int distance)
	{
		return (direction == 'L' || direction == 'D') ? -1 : 1;
	}

	public static int Part2(string inputText)
	{
		var wires = ParsePathList(inputText);
		return DistanceClosestToCentralPortByLength(wires[0], wires[1]);
	}

	public static int DistanceClosestToCentralPortByLength(DirectionPath wireA, DirectionPath wireB)
	{
		var bounds = GetBounds(wireA, wireB);
		var grid = new GridValue[bounds.size.x, bounds.size.y];
		var gridDistanceA = new int[bounds.size.x, bounds.size.y];
		var gridDistanceB = new int[bounds.size.x, bounds.size.y];
		var centralPortPosition = new Vector2Int(Mathf.Abs(bounds.min.x), Mathf.Abs(bounds.min.y));
		grid[centralPortPosition.x, centralPortPosition.y] = GridValue.CentralPort;

		grid = FillGrid(grid, wireA, GridValue.A, GridValue.B, centralPortPosition, gridDistanceA);
		grid = FillGrid(grid, wireB, GridValue.B, GridValue.A, centralPortPosition, gridDistanceB);

		var minDistance = Int32.MaxValue;
		for (int y = grid.GetLength(1) - 1; y >= 0; y--)
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				if (grid[x, y] == GridValue.AB)
					minDistance = Mathf.Min(minDistance, gridDistanceA[x, y] + gridDistanceB[x, y]);
			}
		}


		return minDistance;
	}


	/*
	A List of paths
	R75,D30,R83,U83,L12,D49,R71,U7,L72
	U62,R66,U55,R34,D71,R55,D58,R83
	*/
	public static DirectionPath[] ParsePathList(string inputText)
	{
		return inputText.Split('\n').Select(line => ParsePath(line)).ToArray();
	}

	private static DirectionPath ParsePath(string line)
	{
		return new DirectionPath(line.Split(',').Select(node => ParsePathNode(node)).ToArray());
	}

	private static DirectionPathNode ParsePathNode(string node)
	{
		var direction = node[0];
		var distance = Int32.Parse(node.Substring(1));
		return new DirectionPathNode(node[0], distance);
	}

	public struct DirectionPath
	{
		public DirectionPathNode[] Nodes;

		public DirectionPath(DirectionPathNode[] nodes)
		{
			Nodes = nodes;
		}
	}

	public struct DirectionPathNode
	{
		public char Direction;
		public int Distance;

		public DirectionPathNode(char direction, int distance)
		{
			Direction = direction;
			Distance = distance;
		}
	}
}
