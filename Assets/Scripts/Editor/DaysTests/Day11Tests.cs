﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;

namespace Tests
{
	public class Day11Tests
	{
		public static string Day11Input = "3,8,1005,8,311,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,28,1,1104,0,10,1006,0,71,2,1002,5,10,2,1008,5,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,0,10,4,10,102,1,8,66,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,102,1,8,87,1006,0,97,2,1002,6,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,116,1006,0,95,1,1009,10,10,3,8,102,-1,8,10,101,1,10,10,4,10,108,1,8,10,4,10,102,1,8,145,1,1002,19,10,2,1109,7,10,1006,0,18,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,1,10,4,10,1001,8,0,179,3,8,102,-1,8,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,200,1,1105,14,10,1,1109,14,10,2,1109,11,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,102,1,8,235,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,257,2,101,9,10,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,101,0,8,282,2,1109,19,10,1,105,0,10,101,1,9,9,1007,9,1033,10,1005,10,15,99,109,633,104,0,104,1,21102,937268368140,1,1,21102,328,1,0,1106,0,432,21102,1,932700599052,1,21101,0,339,0,1105,1,432,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21101,0,209421601831,1,21102,1,386,0,1106,0,432,21102,235173604443,1,1,21102,1,397,0,1106,0,432,3,10,104,0,104,0,3,10,104,0,104,0,21101,825439855372,0,1,21102,1,420,0,1106,0,432,21101,0,988220907880,1,21102,431,1,0,1106,0,432,99,109,2,22101,0,-1,1,21101,40,0,2,21102,1,463,3,21102,453,1,0,1106,0,496,109,-2,2105,1,0,0,1,0,0,1,109,2,3,10,204,-1,1001,458,459,474,4,0,1001,458,1,458,108,4,458,10,1006,10,490,1102,1,0,458,109,-2,2106,0,0,0,109,4,2102,1,-1,495,1207,-3,0,10,1006,10,513,21102,0,1,-3,22102,1,-3,1,21202,-2,1,2,21102,1,1,3,21101,532,0,0,1105,1,537,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,560,2207,-4,-2,10,1006,10,560,21201,-4,0,-4,1106,0,628,22102,1,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,1,579,0,1106,0,537,21202,1,1,-4,21102,1,1,-1,2207,-4,-2,10,1006,10,598,21101,0,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,620,21201,-1,0,1,21102,1,620,0,105,1,495,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2105,1,0";

		[Test]
		public void Part1_Exemple1_Step1()
		{
			var program = ProgramThatOutput(1, 0);
			var robot = new EHPR(program);
			robot.Step();

			Assert.AreEqual(1, robot.Painting[new Vector2Int(0, 0)].First());
			Assert.AreEqual(EHPR.Direction.LEFT, robot.CurrentDirection);
			Assert.AreEqual(new Vector2Int(-1, 0), robot.CurrentPosition);
		}

		[Test]
		public void Part1_Exemple1_Step2()
		{
			var program = ProgramThatOutput(1, 0, 0, 0);
			var robot = new EHPR(program);
			robot.Run();


			PrintPainting(robot.Painting);
			var position = new Vector2Int(-1, -1);
			var lastPosition = new Vector2Int(-1, 0);
			Assert.AreEqual(EHPR.Direction.DOWN, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(0, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_Exemple1_Step3()
		{
			var program = ProgramThatOutput(1, 0, 0, 0, 1, 0);
			var robot = new EHPR(program);
			robot.Run();

			PrintPainting(robot.Painting);
			var position = new Vector2Int(0, -1);
			var lastPosition = new Vector2Int(-1, -1);
			Assert.AreEqual(EHPR.Direction.RIGHT, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_Exemple1_Step4()
		{
			var program = ProgramThatOutput(1, 0, 0, 0, 1, 0, 1, 0);
			var robot = new EHPR(program);
			robot.Run();

			PrintPainting(robot.Painting);
			var position = new Vector2Int(0, 0);
			var lastPosition = new Vector2Int(0, -1);
			Assert.AreEqual(EHPR.Direction.UP, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_Exemple1_Step5()
		{
			var program = ProgramThatOutput(1, 0, 0, 0, 1, 0, 1, 0, 0, 1);
			var robot = new EHPR(program);
			robot.Run();

			PrintPainting(robot.Painting);
			var position = new Vector2Int(1, 0);
			var lastPosition = new Vector2Int(0, 0);
			Assert.AreEqual(EHPR.Direction.RIGHT, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(0, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_Exemple1_Step6()
		{
			var program = ProgramThatOutput(1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0);
			var robot = new EHPR(program);
			robot.Run();

			PrintPainting(robot.Painting);
			var position = new Vector2Int(1, 1);
			var lastPosition = new Vector2Int(1, 0);
			Assert.AreEqual(EHPR.Direction.UP, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_Exemple1_Step7()
		{
			var program = ProgramThatOutput(1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0);
			var robot = new EHPR(program);
			robot.Run();

			PrintPainting(robot.Painting);
			var position = new Vector2Int(0, 1);
			var lastPosition = new Vector2Int(1, 1);
			Assert.AreEqual(EHPR.Direction.LEFT, robot.CurrentDirection);
			Assert.AreEqual(position, robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[lastPosition].Last());
		}

		[Test]
		public void Part1_GoingRight_Step1()
		{
			var program = ProgramThatOutput(1, 1);
			var robot = new EHPR(program);
			robot.Step();

			Assert.AreEqual(EHPR.Direction.RIGHT, robot.CurrentDirection);
			Assert.AreEqual(new Vector2Int(1, 0), robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[new Vector2Int(0, 0)].Last());
		}

		[Test]
		public void Part1_GoingRight_Step2()
		{
			var program = ProgramThatOutput(1, 1, 0, 1);
			var robot = new EHPR(program);
			robot.Run();

			Assert.AreEqual(EHPR.Direction.DOWN, robot.CurrentDirection);
			Assert.AreEqual(new Vector2Int(1, -1), robot.CurrentPosition);
			Assert.AreEqual(0, robot.Painting[new Vector2Int(1, 0)].Last());
		}

		[Test]
		public void Part1_GoingRight_Step3()
		{
			var program = ProgramThatOutput(1, 1, 0, 1, 1, 1);
			var robot = new EHPR(program);
			robot.Run();

			Assert.AreEqual(EHPR.Direction.LEFT, robot.CurrentDirection);
			Assert.AreEqual(new Vector2Int(0, -1), robot.CurrentPosition);
			Assert.AreEqual(1, robot.Painting[new Vector2Int(1, -1)].Last());
		}

		[Test]
		public void Part1_GoingRight_Step4()
		{
			var program = ProgramThatOutput(1, 1, 0, 1, 1, 1, 0, 1);
			var robot = new EHPR(program);
			robot.Run();

			Assert.AreEqual(EHPR.Direction.UP, robot.CurrentDirection);
			Assert.AreEqual(new Vector2Int(0, 0), robot.CurrentPosition);
			Assert.AreEqual(0, robot.Painting[new Vector2Int(0, -1)].Last());
		}

		[Test]
		public void Part1()
		{
			var program = new IntCodeProgram(InputParser.ListOfLongs(Day11Input, ','), 0, 0);
			var robot = new EHPR(program);
			robot.Run();

			WriteToFilePainting(robot.Painting, "D11_Part1.txt");

			Assert.AreEqual(1, robot.Painting.Count);
		}

		[Test]
		public void Part2()
		{
			var program = new IntCodeProgram(InputParser.ListOfLongs(Day11Input, ','), 0, 0);
			var robot = new EHPR(program);
			robot.Painting.Add(new Vector2Int(0, 0), new List<long>(new long[] { 1 }));
			robot.Run();

			WriteToFilePainting(robot.Painting, "D11_Part2.txt");

			Assert.AreEqual(1, robot.Painting.Count);
		}


		private void WriteToFilePainting(Dictionary<Vector2Int, List<long>> painting, string fileName)
		{
			var output = $"{painting.Keys.Count} panels painted in {painting.Sum(x => x.Value.Count)} Steps.\n";
			foreach (var item in painting)
				output += $"{item.Key}: {string.Join(", ", item.Value)}\n";
			AOCInput.WriteToFile(fileName, output);
		}

		private void PrintPainting(Dictionary<Vector2Int, List<long>> painting)
		{
			var output = $"{painting.Keys.Count} panels painted in {painting.Sum(x => x.Value.Count)} Steps.\n";
			foreach (var item in painting)
				output += $"{item.Key}: {string.Join(", ", item.Value)}\n";
			Debug.Log(output);
		}

		private IntCodeProgram ProgramThatOutput(params long[] outout)
		{
			var program = new List<long>();

			foreach (var item in outout)
			{
				program.Add(104);
				program.Add(item);
			}
			program.Add(99);
			return new IntCodeProgram(program, 0);
		}
	}
}
