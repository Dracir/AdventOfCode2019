using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EHPR
{

	public Dictionary<Vector2Int, List<long>> Painting = new Dictionary<Vector2Int, List<long>>();
	public Vector2Int CurrentPosition = new Vector2Int(0, 0);
	public Direction CurrentDirection = Direction.UP;

	IntCodeProgram Program;
	IntCodeCompiler compiler = new IntCodeCompiler(0, true);

	public EHPR(IntCodeProgram program)
	{
		Program = program;
	}

	public enum Direction { UP, RIGHT, LEFT, DOWN };
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

	private static Vector2Int MoveForward(Vector2Int currentPosition, Direction direction)
	{
		switch (direction)
		{
			case Direction.UP: return new Vector2Int(currentPosition.x, currentPosition.y + 1);
			case Direction.RIGHT: return new Vector2Int(currentPosition.x + 1, currentPosition.y);
			case Direction.DOWN: return new Vector2Int(currentPosition.x, currentPosition.y - 1);
			case Direction.LEFT: return new Vector2Int(currentPosition.x - 1, currentPosition.y);
			default: return currentPosition;
		}
	}

	public void Run()
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
	}
}
