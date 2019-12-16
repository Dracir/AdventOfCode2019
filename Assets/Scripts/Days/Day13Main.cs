using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day13Main
{

	public static int Part1(string inputText)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');
		var compiler = new IntCodeCompiler(0);
		compiler.Compute(new IntCodeProgram(intcode, 0));

		var level = CreateLevel(compiler.OutputValues.ToArray());
		int nbBlock = 0;
		for (int y = 0; y < level.GetLength(0); y++)
			for (int x = 0; x < level.GetLength(1); x++)
				if (level[y, x] == TileType.Block) nbBlock++;

		return nbBlock;
	}

	public enum TileType { Empty, Wall, Block, Paddle, Ball };

	public static TileType[,] CreateLevel(long[] levelTiles)
	{
		var tiles = new List<(int x, int y, TileType tileType)>();
		for (int i = 0; i < levelTiles.Length; i += 3)
			tiles.Add(((int)levelTiles[i], (int)levelTiles[i + 1], IdToTileType((int)levelTiles[i + 2])));

		int maxX = tiles.Max(tile => tile.x) + 1;
		int maxY = tiles.Max(tile => tile.y) + 1;

		var level = new TileType[maxY, maxX];
		foreach (var tile in tiles)
			level[tile.y, tile.x] = tile.tileType;

		return level;
	}

	public static TileType IdToTileType(int id) => (TileType)((TileType[])Enum.GetValues(typeof(TileType)))[id];

	public static long Part2(string inputText)
	{
		var intcode = InputParser.ListOfLongs(inputText, ',');
		var compiler = new IntCodeCompiler(0, true);
		intcode[0] = 2;
		var program = new IntCodeProgram(intcode, 0);


		var level = new TileType[100, 100];
		int ballPosition = 0;
		int paddlePosition = 0;
		int blockRemaining = 261;
		int maxBreak = 25000;
		long score = 0;

		while (--maxBreak > 0 && !program.IsDone && blockRemaining > 0)
		{
			paddlePosition = FindXOfTileTyle(TileType.Paddle, level);
			ballPosition = FindXOfTileTyle(TileType.Ball, level);

			if (paddlePosition < ballPosition)
				compiler.SetInputs(new long[] { 1 });
			else if (paddlePosition > ballPosition)
				compiler.SetInputs(new long[] { -1 });
			else
				compiler.SetInputs(new long[] { 0 });

			compiler.Clear();
			program = compiler.Compute(program);
			program = compiler.Compute(program);
			program = compiler.Compute(program);

			if (compiler.OutputValues[0] == -1 && compiler.OutputValues[1] == 0)
			{
				blockRemaining--;
				score = compiler.OutputValues[2];
				Debug.Log($"{blockRemaining} block remainning. Score : {score}");
			}
			else
			{
				var x = compiler.OutputValues[0];
				var y = compiler.OutputValues[1];
				var tileType = (int)compiler.OutputValues[2];
				level[y, x] = IdToTileType(tileType);
			}
		}

		Debug.Log($"maxBreak:{maxBreak},  ");

		return score;
	}

	private static int FindXOfTileTyle(TileType type, TileType[,] level)
	{
		for (int y = 0; y < level.GetLength(0); y++)
			for (int x = 0; x < level.GetLength(1); x++)
				if (level[y, x] == type) return x;

		return -1;
	}
}
