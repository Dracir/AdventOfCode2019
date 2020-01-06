using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class RepairDroid
{
	public enum TileType { Unknown, Empty, Wall, OxygenSystem }
	public int MiddleX = 50;
	public int MiddleY = 50;
	public TileType[,] Map = new TileType[100, 100];
	public bool[,] ExploredMap = new bool[100, 100];

	public Dictionary<Vector2Int, List<long>> Painting = new Dictionary<Vector2Int, List<long>>();
	public Vector2Int CurrentPosition = new Vector2Int(50, 50);
	public Direction CurrentDirection = Direction.UP;

	IntCodeProgram Program;
	IntCodeCompiler Compiler = new IntCodeCompiler(0, true);

	public RepairDroid(IntCodeProgram program)
	{
		Program = program;
	}

	public enum Direction { UP = 1, RIGHT = 4, LEFT = 3, DOWN = 2 };

	public Vector2Int LeftOfMyPosition => new Vector2Int(CurrentPosition.x - 1, CurrentPosition.y);
	public Vector2Int RightOfMyPosition => new Vector2Int(CurrentPosition.x + 1, CurrentPosition.y);
	public Vector2Int DownOfMyPosition => new Vector2Int(CurrentPosition.x, CurrentPosition.y - 1);
	public Vector2Int UpOfMyPosition => new Vector2Int(CurrentPosition.x, CurrentPosition.y + 1);

	public static Direction LeftOf(Direction direction)
	{
		switch (direction)
		{
			case Direction.UP: return Direction.LEFT;
			case Direction.RIGHT: return Direction.UP;
			case Direction.DOWN: return Direction.RIGHT;
			case Direction.LEFT: return Direction.DOWN;
			default: return Direction.LEFT;
		}
	}
	public static Direction RightOf(Direction direction)
	{
		switch (direction)
		{
			case Direction.UP: return Direction.RIGHT;
			case Direction.RIGHT: return Direction.DOWN;
			case Direction.DOWN: return Direction.LEFT;
			case Direction.LEFT: return Direction.UP;
			default: return Direction.RIGHT;
		}
	}

	private static Vector2Int GetForward(Vector2Int position, Direction direction)
	{
		switch (direction)
		{
			case Direction.UP: return new Vector2Int(position.x, position.y + 1);
			case Direction.RIGHT: return new Vector2Int(position.x + 1, position.y);
			case Direction.DOWN: return new Vector2Int(position.x, position.y - 1);
			case Direction.LEFT: return new Vector2Int(position.x - 1, position.y);
			default: return position;
		}
	}

	public bool HasExplored(Vector2Int position) => ExploredMap[position.y, position.x];

	public void FindOxygenSystem()
	{
		if (!HasExplored(LeftOfMyPosition))
		{
			Move(Direction.LEFT);
			FindOxygenSystem();
			Move(Direction.RIGHT);
		}
		if (!HasExplored(UpOfMyPosition))
		{
			Move(Direction.UP);
			FindOxygenSystem();
			Move(Direction.DOWN);
		}
		if (!HasExplored(RightOfMyPosition))
		{
			Move(Direction.LEFT);
			FindOxygenSystem();
			Move(Direction.RIGHT);
		}
		if (!HasExplored(DownOfMyPosition))
		{
			Move(Direction.DOWN);
			FindOxygenSystem();
			Move(Direction.UP);
		}
	}

	private void Move(Direction direction)
	{
		Compiler.Clear();
		Compiler.SetInputs((long)direction);
		Program = Compiler.Compute(Program);
		var output = Compiler.OutputValue;
		var forwardTile = GetForward(CurrentPosition, direction);
		ExploredMap[forwardTile.y, forwardTile.x] = true;
		if (output == 0)
			Map[forwardTile.y, forwardTile.x] = TileType.Wall;
		else if (output == 1)
		{
			Map[forwardTile.y, forwardTile.x] = TileType.Empty;
			CurrentPosition = forwardTile;
		}
		else
		{
			Map[forwardTile.y, forwardTile.x] = TileType.OxygenSystem;
			CurrentPosition = forwardTile;
		}

		var map = (TileType[,])Map.Clone();
		AOCExecutor.ActionForMainOnPerUpdate.Enqueue(() =>RepairDroid.MapToTexturee(map, AOCUI.Instance.Part1ComputeOutput));
	}


	public static void MapToTexturee(TileType[,] map, RawImage target)
	{
		int w = map.GetLength(1);
		int h = map.GetLength(0);
		var texture = new Texture2D(w, h);

		var colors = new Color32[w * h];

		int i = 0;
		for (int y = h - 1; y >= 0; y--)
		{
			for (int x = 0; x < w; x++)
			{
				var value = map[y, x];
				if (value == TileType.Unknown)
					colors[i++] = Color.black;
				else if (value == TileType.Empty)
					colors[i++] = Color.gray;
				else if (value == TileType.Wall)
					colors[i++] = Color.green;
				else if (value == TileType.OxygenSystem)
					colors[i++] = Color.blue;
				else
					colors[i++] = Color.yellow;
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.SetPixels32(colors);
		texture.Apply();
		target.texture = texture;
	}

	/*public void Run()
	{
		int infiniProtection = 100000;
		while (!Program.IsDone && infiniProtection-- != 0)
			Step();
		if (infiniProtection <= 0)
			Debug.LogError("Used infiniProtection");
	}

	public void Step()
	{
		var currentColor = 0L;

		compiler.Clear();
		if (Painting.ContainsKey(CurrentPosition))
			currentColor = Painting[CurrentPosition].Last();
		else
		{
			Painting.Add(CurrentPosition, new List<long>());
			currentColor = 0;
		}

		compiler.SetInputs(new long[] { currentColor });

		Program = compiler.Compute(Program);
		Program = compiler.Compute(Program);

		if (Program.IsDone)
			return;

		//	Debug.Log(compiler.OutputAasListIntStr);

		Painting[CurrentPosition].Add(compiler.OutputValues[0]);

		if (compiler.OutputValues[1] == 0)
			CurrentDirection = LeftOf(CurrentDirection);
		else
			CurrentDirection = RightOf(CurrentDirection);
		CurrentPosition = MoveForward(CurrentPosition, CurrentDirection);
	}*/
}
