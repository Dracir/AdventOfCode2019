using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day10Tests
	{
		[Test]
		public void Part1_AngleTest_DownIs180() => Assert.AreEqual(180, Day10Main.AngleBetween(0, 1, 0, 4), 0.001f);
		[Test]
		public void Part1_AngleTest_RightIs90() => Assert.AreEqual(90, Day10Main.AngleBetween(0, 1, 2, 1), 0.001f);
		[Test]
		public void Part1_AngleTest_UpIs0() => Assert.AreEqual(0, Day10Main.AngleBetween(0, 2, 0, 1), 0.001f);
		[Test]
		public void Part1_AngleTest_LeftIs270() => Assert.AreEqual(270, Day10Main.AngleBetween(2, 1, 0, 1), 0.001f);


		private static string Exemple1Grid = ".#..#\n.....\n#####\n....#\n...##";
		[Test]
		public void Part1_Exemple1_Has8()
		{
			Assert.AreEqual(8, Day10Main.Part1(Exemple1Grid));
		}
		[Test]
		public void Part1_Exemple1_DistancesGrid()
		{
			var grid = InputParser.ParseBoolGrid(Exemple1Grid, '\n', '#');
			var expected = new int[,] { { 0, 7, 0, 0, 7 }, { 0, 0, 0, 0, 0 }, { 6, 7, 7, 7, 5 }, { 0, 0, 0, 0, 7 }, { 0, 0, 0, 8, 7 } };
			Assert.AreEqual(expected, Day10Main.AsteroidToDistanceGrid(grid));
		}

		private static string Exemple2Grid = "......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####";
		[Test]
		public void Part1_Exemple2_Has33()
		{
			Assert.AreEqual(33, Day10Main.Part1(Exemple2Grid));
		}

		private static string Exemple3Grid = "#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###.";
		[Test]
		public void Part1_Exemple3_Has35()
		{
			Assert.AreEqual(35, Day10Main.Part1(Exemple3Grid));
		}

		private static string Exemple4Grid = ".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#..";
		[Test]
		public void Part1_Exemple4_Has41()
		{
			Assert.AreEqual(41, Day10Main.Part1(Exemple4Grid));
		}


		private static string Part2Exemple1 = ".#....#####...#..\n##...##.#####..##\n##...#...#.#####.\n..#.....#...###..\n..#.#.....#....##";
		[Test]
		public void Part2_Exemple1Grid()
		{
			var grid = InputParser.ParseBoolGrid(Part2Exemple1.Replace("\r", ""), '\n', '#');
			var exected = new int[,] {
				{ 0,26,0,0,0,0,29,30,31,2,4,0,0,0,33,0,0},
				{23,24,0,0,0,27,28,0,1,3,32,6,7,0,0,9,34},
				{21,22,0,0,0,25,0,0,0,5,0,8,10,11,12,13,0},
				{0,0,20,0,0,0,0,0,0,0,0,0,14,35,36,0,0},
				{0,0,19,0,18,0,0,0,0,0,17,0,0,0,0,16,15}
			};
			var actual = Day10Main.AsteroidToDestructionOrderGrid(grid, new Vector2Int(8, 3));
			Debug.Log(TestUtility.GridToPrintableString<int>(actual));
			TestUtility.AreEqual(exected, actual);
		}

		[Test]
		public void Part2_Exemple1Answer()
		{
			Assert.AreEqual(1403, Day10Main.Part2(Part2Exemple1, 8, 3));
		}

		private static string BigExemple = ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##";
		[Test]
		public void Part2_Exemple2()
		{
			Assert.AreEqual(802, Day10Main.Part2(BigExemple, 11, 13));
		}

	}
}
