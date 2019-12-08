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

			private static string Day5Input = "3,225,1,225,6,6,1100,1,238,225,104,0,2,136,183,224,101,-5304,224,224,4,224,1002,223,8,223,1001,224,6,224,1,224,223,223,1101,72,47,225,1101,59,55,225,1101,46,75,225,1101,49,15,224,101,-64,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,102,9,210,224,1001,224,-270,224,4,224,1002,223,8,223,1001,224,2,224,1,223,224,223,101,14,35,224,101,-86,224,224,4,224,1002,223,8,223,101,4,224,224,1,224,223,223,1102,40,74,224,1001,224,-2960,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,1101,10,78,225,1001,39,90,224,1001,224,-149,224,4,224,102,8,223,223,1001,224,4,224,1,223,224,223,1002,217,50,224,1001,224,-1650,224,4,224,1002,223,8,223,1001,224,7,224,1,224,223,223,1102,68,8,225,1,43,214,224,1001,224,-126,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1102,88,30,225,1102,18,80,225,1102,33,28,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,108,677,677,224,102,2,223,223,1005,224,329,1001,223,1,223,1107,677,226,224,102,2,223,223,1006,224,344,1001,223,1,223,108,226,226,224,102,2,223,223,1005,224,359,1001,223,1,223,1108,677,226,224,102,2,223,223,1006,224,374,101,1,223,223,108,677,226,224,102,2,223,223,1006,224,389,1001,223,1,223,107,226,226,224,102,2,223,223,1005,224,404,1001,223,1,223,8,226,226,224,102,2,223,223,1006,224,419,101,1,223,223,1107,677,677,224,102,2,223,223,1006,224,434,1001,223,1,223,1107,226,677,224,1002,223,2,223,1006,224,449,101,1,223,223,7,677,677,224,1002,223,2,223,1006,224,464,1001,223,1,223,1108,226,677,224,1002,223,2,223,1005,224,479,1001,223,1,223,8,677,226,224,1002,223,2,223,1005,224,494,101,1,223,223,7,226,677,224,102,2,223,223,1005,224,509,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,524,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,539,1001,223,1,223,1007,677,677,224,102,2,223,223,1005,224,554,101,1,223,223,107,226,677,224,1002,223,2,223,1005,224,569,1001,223,1,223,1108,677,677,224,1002,223,2,223,1006,224,584,1001,223,1,223,1008,226,226,224,1002,223,2,223,1005,224,599,101,1,223,223,1008,677,677,224,102,2,223,223,1005,224,614,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,629,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,644,101,1,223,223,1007,226,677,224,1002,223,2,223,1005,224,659,1001,223,1,223,1007,226,226,224,102,2,223,223,1005,224,674,101,1,223,223,4,223,99,226";
			[Test]
			public void Part1Answer()
			{
				var intcode = InputParser.ListOfInts(Day5Input, ',');

				var compiler = new IntCodeCompiler(1);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual("30000000013978427", compiler.OutputValue);

			}
			[Test]
			public void Part2Answer()
			{
				var intcode = InputParser.ListOfInts(Day5Input, ',');

				var compiler = new IntCodeCompiler(5);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual("11189491", compiler.OutputValue);

			}
		}


		public class Day7
		{
			private static string Day7Input = "3,8,1001,8,10,8,105,1,0,0,21,38,47,64,85,106,187,268,349,430,99999,3,9,1002,9,4,9,1001,9,4,9,1002,9,4,9,4,9,99,3,9,1002,9,4,9,4,9,99,3,9,1001,9,3,9,102,5,9,9,1001,9,5,9,4,9,99,3,9,101,3,9,9,102,5,9,9,1001,9,4,9,102,4,9,9,4,9,99,3,9,1002,9,3,9,101,2,9,9,102,4,9,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";

			[Test]
			public void Part1Answer()
			{
				Assert.AreEqual(366376, Day7Main.Part1(Day7Input));
			}

			[Test]
			public void Part2Answer()
			{
				Assert.AreEqual(21596786, Day7Main.Part2(Day7Input));
			}
		}
	}
}
