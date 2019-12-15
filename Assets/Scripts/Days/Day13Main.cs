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

	public static string Part2(string inputText)
	{
		return "2";
	}
}
