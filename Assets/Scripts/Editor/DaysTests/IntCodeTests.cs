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
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple12()
			{
				var input = new int[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input, 4));
			}

			[Test]
			public void Exemple13()
			{
				var input = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input, 8));
			}

			[Test]
			public void Exemple2()
			{
				var input = new int[] { 1, 0, 0, 0, 99 };
				var expected = new int[] { 2, 0, 0, 0, 99 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple3()
			{
				var input = new int[] { 2, 3, 0, 3, 99 };
				var expected = new int[] { 2, 3, 0, 6, 99 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple4()
			{
				var input = new int[] { 2, 4, 4, 5, 99, 0 };
				var expected = new int[] { 2, 4, 4, 5, 99, 9801 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple5()
			{
				var input = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
				var expected = new int[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
				TestUtility.AreEqual(expected, IntCodeCompiler.Compute(input));
			}
		}

		public class Day5
		{
			[Test]
			public void ParameterAInPositionMode() => Assert.AreEqual(true, IntCodeProgram.InstructionParameterAInPositionMode(001));
			[Test]
			public void ParameterAInImmediateMode() => Assert.AreEqual(false, IntCodeProgram.InstructionParameterAInPositionMode(101));
			[Test]
			public void ParameterAInPositionMode2_From1001() => Assert.AreEqual(true, IntCodeProgram.InstructionParameterAInPositionMode(1001));
			[Test]
			public void ParameterAInImmediateMode_From1101() => Assert.AreEqual(false, IntCodeProgram.InstructionParameterAInPositionMode(1101));
			[Test]
			public void ParameterBInPositionMode() => Assert.AreEqual(true, IntCodeProgram.InstructionParameterBInPositionMode(0001));
			[Test]
			public void ParameterBInImmediateMode() => Assert.AreEqual(false, IntCodeProgram.InstructionParameterBInPositionMode(1001));
			[Test]
			public void ParameterBInPositionMode_From0101() => Assert.AreEqual(true, IntCodeProgram.InstructionParameterBInPositionMode(0101));
			[Test]
			public void ParameterBInImmediateMode_From1101() => Assert.AreEqual(false, IntCodeProgram.InstructionParameterBInPositionMode(1101));


			[Test]
			public void AddInImmediate() => TestUtility.AreEqual(new int[] { 3, 0, 3, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1101, 0, 3, 0 }));
			[Test]
			public void AddInImmediate2() => TestUtility.AreEqual(new int[] { 3, 3, 0, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1101, 3, 0, 0 }));
			[Test]
			public void AddInImmediate3() => TestUtility.AreEqual(new int[] { -1, -1, 0, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1101, -1, 0, 0 }));

			[Test]
			public void AddHalfImmediateHalfPosition() => TestUtility.AreEqual(new int[] { 1006, 0, 5, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1001, 0, 5, 0 }));
			[Test]
			public void AddHalfImmediateHalfPosition2() => TestUtility.AreEqual(new int[] { 106, 5, 0, 0 }, IntCodeCompiler.ComputeStep(new int[] { 0101, 5, 0, 0 }));


			[Test]
			public void MultiplyInImmediate() => TestUtility.AreEqual(new int[] { 18, 6, 3, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1102, 6, 3, 0 }));
			[Test]
			public void MultiplyInImmediate2() => TestUtility.AreEqual(new int[] { 0, 3, 0, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1102, 3, 0, 0 }));
			[Test]
			public void MultiplyInImmediate3() => TestUtility.AreEqual(new int[] { -6, -1, 6, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1102, -1, 6, 0 }));

			[Test]
			public void MultiplyHalfImmediateHalfPosition() => TestUtility.AreEqual(new int[] { 25, 2, 5, 0 }, IntCodeCompiler.ComputeStep(new int[] { 1002, 2, 5, 0 }));
			[Test]
			public void MultiplyHalfImmediateHalfPosition2() => TestUtility.AreEqual(new int[] { 510, 5, 0, 0 }, IntCodeCompiler.ComputeStep(new int[] { 0102, 5, 0, 0 }));

			[Test]
			public void Exemple1() => TestUtility.AreEqual(new int[] { 1002, 4, 3, 4, 99 }, IntCodeCompiler.ComputeStep(new int[] { 1002, 4, 3, 4, 33 }));
			[Test]
			public void Exemple2() => TestUtility.AreEqual(new int[] { 1101, 100, -1, 4, 99 }, IntCodeCompiler.ComputeStep(new int[] { 1101, 100, -1, 4, 0 }));
		}
	}
}
