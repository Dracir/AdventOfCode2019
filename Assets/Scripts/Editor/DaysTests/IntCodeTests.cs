using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class IntCodeTests
	{
		public class Day2
		{

			[Test]
			public void Exemple11()
			{
				var input = new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple12()
			{
				var input = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input,4));
			}

			[Test]
			public void Exemple13()
			{
				var input = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input,8));
			}

			[Test]
			public void Exemple2()
			{
				var input = new int[] { 1, 0, 0, 0, 99 };
				var expected = new int[] { 2, 0, 0, 0, 99 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple3()
			{
				var input = new int[] { 2, 3, 0, 3, 99 };
				var expected = new int[] { 2, 3, 0, 6, 99 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple4()
			{
				var input = new int[] { 2, 4, 4, 5, 99, 0 };
				var expected = new int[] { 2, 4, 4, 5, 99, 9801 };
				TestUtility.AreEquals(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple5()
			{
				var input = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
				var expected = new int[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
				TestUtility.AreEquals(expected, IntCodeCompiler.Compute(input));
			}
		}
	}
}
