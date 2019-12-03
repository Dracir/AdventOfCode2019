using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day3Tests
	{
		[Test]
		public void Part1_Exemple1()
		{
			var input = "R8,U5,L5,D3\nU7,R6,D4,L4";
			var output = Day3Main.Part1(input);
			Assert.AreEqual(6, output);
		}

		[Test]
		public void Part1_Exemple2()
		{
			var input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83";
			var output = Day3Main.Part1(input);
			Assert.AreEqual(159, output);
		}
		[Test]
		public void Part1_Exemple3()
		{
			var input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
			var output = Day3Main.Part1(input);
			Assert.AreEqual(135, output);
		}

		[Test]
		public void Part2_Exemple1()
		{
			var input = "R8,U5,L5,D3\nU7,R6,D4,L4";
			var output = Day3Main.Part2(input);
			Assert.AreEqual(30, output);
		}

		[Test]
		public void Part2_Exemple2()
		{
			var input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83";
			var output = Day3Main.Part2(input);
			Assert.AreEqual(610, output);
		}
		[Test]
		public void Part2_Exemple3()
		{
			var input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
			var output = Day3Main.Part2(input);
			Assert.AreEqual(410, output);
		}
	}
}
