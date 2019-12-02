using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day2Tests
	{
		public class Part1
		{

			[Test]
			public void Exemple11()
			{
				var input = new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				Assert.AreEqual(expected, Day2Main.ComputeStep(input, 0));
			}

			[Test]
			public void Exemple12()
			{
				var input = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				Assert.AreEqual(expected, Day2Main.ComputeStep(input, 4));
			}

			[Test]
			public void Exemple13()
			{
				var input = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				Assert.AreEqual(input, Day2Main.ComputeStep(input, 8));
			}

			[Test]
			public void Exemple2()
			{
				var input = new int[] { 1, 0, 0, 0, 99 };
				var expected = new int[] { 2, 0, 0, 0, 99 };
				Assert.AreEqual(expected, Day2Main.ComputeStep(input, 0));
			}

			[Test]
			public void Exemple3()
			{
				var input = new int[] { 2, 3, 0, 3, 99 };
				var expected = new int[] { 2, 3, 0, 6, 99 };
				Assert.AreEqual(expected, Day2Main.ComputeStep(input, 0));
			}

			[Test]
			public void Exemple4()
			{
				var input = new int[] { 2, 4, 4, 5, 99, 0 };
				var expected = new int[] { 2, 4, 4, 5, 99, 9801 };
				Assert.AreEqual(expected, Day2Main.ComputeStep(input, 0));
			}

			[Test]
			public void Exemple5()
			{
				var input = new int[] { 1,1,1,4,99,5,6,0,99 };
				var expected = new int[] { 30,1,1,4,2,5,6,0,99 };
				Assert.AreEqual(expected, Day2Main.Compute(input, 0));
			}
		}

	}
}