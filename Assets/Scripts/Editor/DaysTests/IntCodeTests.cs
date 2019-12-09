using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;
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
				var input = new long[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new long[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple12()
			{
				var input = new long[] { 1, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new long[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input, 4));
			}

			[Test]
			public void Exemple13()
			{
				var input = new long[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				var expected = new long[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input, 8));
			}

			[Test]
			public void Exemple2()
			{
				var input = new long[] { 1, 0, 0, 0, 99 };
				var expected = new long[] { 2, 0, 0, 0, 99 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple3()
			{
				var input = new long[] { 2, 3, 0, 3, 99 };
				var expected = new long[] { 2, 3, 0, 6, 99 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple4()
			{
				var input = new long[] { 2, 4, 4, 5, 99, 0 };
				var expected = new long[] { 2, 4, 4, 5, 99, 9801 };
				TestUtility.AreEqual(expected, IntCodeCompiler.ComputeStep(input));
			}

			[Test]
			public void Exemple5()
			{
				var input = new long[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
				var expected = new long[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
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
			public void AddInImmediate() => TestUtility.AreEqual(new long[] { 3, 0, 3, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1101, 0, 3, 0 }));
			[Test]
			public void AddInImmediate2() => TestUtility.AreEqual(new long[] { 3, 3, 0, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1101, 3, 0, 0 }));
			[Test]
			public void AddInImmediate3() => TestUtility.AreEqual(new long[] { -1, -1, 0, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1101, -1, 0, 0 }));

			[Test]
			public void AddHalfImmediateHalfPosition() => TestUtility.AreEqual(new long[] { 1006, 0, 5, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1001, 0, 5, 0 }));
			[Test]
			public void AddHalfImmediateHalfPosition2() => TestUtility.AreEqual(new long[] { 106, 5, 0, 0 }, IntCodeCompiler.ComputeStep(new long[] { 0101, 5, 0, 0 }));


			[Test]
			public void MultiplyInImmediate() => TestUtility.AreEqual(new long[] { 18, 6, 3, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1102, 6, 3, 0 }));
			[Test]
			public void MultiplyInImmediate2() => TestUtility.AreEqual(new long[] { 0, 3, 0, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1102, 3, 0, 0 }));
			[Test]
			public void MultiplyInImmediate3() => TestUtility.AreEqual(new long[] { -6, -1, 6, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1102, -1, 6, 0 }));

			[Test]
			public void MultiplyHalfImmediateHalfPosition() => TestUtility.AreEqual(new long[] { 25, 2, 5, 0 }, IntCodeCompiler.ComputeStep(new long[] { 1002, 2, 5, 0 }));
			[Test]
			public void MultiplyHalfImmediateHalfPosition2() => TestUtility.AreEqual(new long[] { 510, 5, 0, 0 }, IntCodeCompiler.ComputeStep(new long[] { 0102, 5, 0, 0 }));

			[Test]
			public void Exemple1() => TestUtility.AreEqual(new long[] { 1002, 4, 3, 4, 99 }, IntCodeCompiler.ComputeStep(new long[] { 1002, 4, 3, 4, 33 }));
			[Test]
			public void Exemple2() => TestUtility.AreEqual(new long[] { 1101, 100, -1, 4, 99 }, IntCodeCompiler.ComputeStep(new long[] { 1101, 100, -1, 4, 0 }));

			private static string Day5Input = "3,225,1,225,6,6,1100,1,238,225,104,0,2,136,183,224,101,-5304,224,224,4,224,1002,223,8,223,1001,224,6,224,1,224,223,223,1101,72,47,225,1101,59,55,225,1101,46,75,225,1101,49,15,224,101,-64,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,102,9,210,224,1001,224,-270,224,4,224,1002,223,8,223,1001,224,2,224,1,223,224,223,101,14,35,224,101,-86,224,224,4,224,1002,223,8,223,101,4,224,224,1,224,223,223,1102,40,74,224,1001,224,-2960,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,1101,10,78,225,1001,39,90,224,1001,224,-149,224,4,224,102,8,223,223,1001,224,4,224,1,223,224,223,1002,217,50,224,1001,224,-1650,224,4,224,1002,223,8,223,1001,224,7,224,1,224,223,223,1102,68,8,225,1,43,214,224,1001,224,-126,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1102,88,30,225,1102,18,80,225,1102,33,28,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,108,677,677,224,102,2,223,223,1005,224,329,1001,223,1,223,1107,677,226,224,102,2,223,223,1006,224,344,1001,223,1,223,108,226,226,224,102,2,223,223,1005,224,359,1001,223,1,223,1108,677,226,224,102,2,223,223,1006,224,374,101,1,223,223,108,677,226,224,102,2,223,223,1006,224,389,1001,223,1,223,107,226,226,224,102,2,223,223,1005,224,404,1001,223,1,223,8,226,226,224,102,2,223,223,1006,224,419,101,1,223,223,1107,677,677,224,102,2,223,223,1006,224,434,1001,223,1,223,1107,226,677,224,1002,223,2,223,1006,224,449,101,1,223,223,7,677,677,224,1002,223,2,223,1006,224,464,1001,223,1,223,1108,226,677,224,1002,223,2,223,1005,224,479,1001,223,1,223,8,677,226,224,1002,223,2,223,1005,224,494,101,1,223,223,7,226,677,224,102,2,223,223,1005,224,509,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,524,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,539,1001,223,1,223,1007,677,677,224,102,2,223,223,1005,224,554,101,1,223,223,107,226,677,224,1002,223,2,223,1005,224,569,1001,223,1,223,1108,677,677,224,1002,223,2,223,1006,224,584,1001,223,1,223,1008,226,226,224,1002,223,2,223,1005,224,599,101,1,223,223,1008,677,677,224,102,2,223,223,1005,224,614,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,629,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,644,101,1,223,223,1007,226,677,224,1002,223,2,223,1005,224,659,1001,223,1,223,1007,226,226,224,102,2,223,223,1005,224,674,101,1,223,223,4,223,99,226";
			[Test]
			public void Part1Answer()
			{
				var intcode = InputParser.ListOfLongs(Day5Input, ',');

				var compiler = new IntCodeCompiler(1);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual("30000000013978427", compiler.OutputConcat);

			}
			[Test]
			public void Part2Answer()
			{
				var intcode = InputParser.ListOfLongs(Day5Input, ',');

				var compiler = new IntCodeCompiler(5);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual("11189491", compiler.OutputConcat);

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

		public class OtherTests
		{
			[Test]
			public void Part1_RelativeExample()
			{
				var compiler = new IntCodeCompiler();
				var program = compiler.Compute(new IntCodeProgram(new long[] { 104, 1, 104, 2, 104, 3, 104, 4, 99 }, 0, 0));
				Assert.AreEqual(false, compiler.Paused, "Should Not be paused");
				Assert.AreEqual(1234, compiler.OutputValue, "" + compiler.OutputAasListIntStr);
			}
		}
		public class Day9
		{
			private static string Day9Input = "1102,34463338,34463338,63,1007,63,34463338,63,1005,63,53,1102,1,3,1000,109,988,209,12,9,1000,209,6,209,3,203,0,1008,1000,1,63,1005,63,65,1008,1000,2,63,1005,63,904,1008,1000,0,63,1005,63,58,4,25,104,0,99,4,0,104,0,99,4,17,104,0,99,0,0,1102,533,1,1024,1102,260,1,1023,1101,33,0,1016,1102,37,1,1017,1102,1,36,1009,1101,0,35,1011,1101,0,27,1004,1101,0,0,1020,1101,242,0,1029,1101,0,31,1018,1101,0,38,1007,1101,0,29,1015,1102,1,23,1006,1101,25,0,1002,1102,1,39,1008,1101,0,20,1001,1102,1,34,1012,1102,370,1,1027,1101,30,0,1010,1102,24,1,1014,1101,21,0,1000,1101,22,0,1003,1102,1,26,1005,1101,0,267,1022,1101,1,0,1021,1101,28,0,1013,1101,0,32,1019,1101,251,0,1028,1101,377,0,1026,1102,1,524,1025,109,4,2102,1,-4,63,1008,63,21,63,1005,63,203,4,187,1105,1,207,1001,64,1,64,1002,64,2,64,109,6,1201,-1,0,63,1008,63,36,63,1005,63,229,4,213,1105,1,233,1001,64,1,64,1002,64,2,64,109,18,2106,0,0,4,239,1001,64,1,64,1106,0,251,1002,64,2,64,109,-4,2105,1,-1,1001,64,1,64,1105,1,269,4,257,1002,64,2,64,109,-6,1205,3,287,4,275,1001,64,1,64,1106,0,287,1002,64,2,64,109,-19,1202,9,1,63,1008,63,41,63,1005,63,307,1105,1,313,4,293,1001,64,1,64,1002,64,2,64,109,8,2108,23,-1,63,1005,63,331,4,319,1106,0,335,1001,64,1,64,1002,64,2,64,109,-3,21101,40,0,10,1008,1014,40,63,1005,63,361,4,341,1001,64,1,64,1106,0,361,1002,64,2,64,109,28,2106,0,-5,1001,64,1,64,1106,0,379,4,367,1002,64,2,64,109,-30,1208,7,36,63,1005,63,401,4,385,1001,64,1,64,1105,1,401,1002,64,2,64,109,-1,2101,0,6,63,1008,63,38,63,1005,63,427,4,407,1001,64,1,64,1105,1,427,1002,64,2,64,109,7,1207,-3,27,63,1005,63,445,4,433,1106,0,449,1001,64,1,64,1002,64,2,64,109,8,21107,41,40,0,1005,1016,465,1106,0,471,4,455,1001,64,1,64,1002,64,2,64,109,6,21107,42,43,-6,1005,1016,489,4,477,1105,1,493,1001,64,1,64,1002,64,2,64,109,-26,1208,8,28,63,1005,63,513,1001,64,1,64,1105,1,515,4,499,1002,64,2,64,109,29,2105,1,-1,4,521,1001,64,1,64,1105,1,533,1002,64,2,64,109,-16,1201,-4,0,63,1008,63,23,63,1005,63,553,1105,1,559,4,539,1001,64,1,64,1002,64,2,64,109,4,21101,43,0,-3,1008,1010,41,63,1005,63,579,1106,0,585,4,565,1001,64,1,64,1002,64,2,64,109,-8,1207,-3,24,63,1005,63,605,1001,64,1,64,1106,0,607,4,591,1002,64,2,64,109,1,2102,1,-2,63,1008,63,25,63,1005,63,627,1106,0,633,4,613,1001,64,1,64,1002,64,2,64,109,4,2108,25,-7,63,1005,63,653,1001,64,1,64,1106,0,655,4,639,1002,64,2,64,109,16,21102,44,1,-8,1008,1018,44,63,1005,63,681,4,661,1001,64,1,64,1106,0,681,1002,64,2,64,109,-32,1202,9,1,63,1008,63,22,63,1005,63,703,4,687,1105,1,707,1001,64,1,64,1002,64,2,64,109,1,2107,26,9,63,1005,63,725,4,713,1105,1,729,1001,64,1,64,1002,64,2,64,109,21,1206,5,745,1001,64,1,64,1106,0,747,4,735,1002,64,2,64,109,3,1205,1,763,1001,64,1,64,1106,0,765,4,753,1002,64,2,64,109,-18,2101,0,5,63,1008,63,24,63,1005,63,785,1105,1,791,4,771,1001,64,1,64,1002,64,2,64,109,6,21102,45,1,4,1008,1011,48,63,1005,63,811,1106,0,817,4,797,1001,64,1,64,1002,64,2,64,109,5,21108,46,46,1,1005,1013,835,4,823,1106,0,839,1001,64,1,64,1002,64,2,64,109,-5,21108,47,45,8,1005,1015,855,1105,1,861,4,845,1001,64,1,64,1002,64,2,64,109,9,1206,4,875,4,867,1105,1,879,1001,64,1,64,1002,64,2,64,109,-7,2107,23,-6,63,1005,63,895,1106,0,901,4,885,1001,64,1,64,4,64,99,21101,27,0,1,21101,915,0,0,1106,0,922,21201,1,51547,1,204,1,99,109,3,1207,-2,3,63,1005,63,964,21201,-2,-1,1,21101,942,0,0,1106,0,922,22102,1,1,-1,21201,-2,-3,1,21102,1,957,0,1106,0,922,22201,1,-1,-2,1106,0,968,21202,-2,1,-2,109,-3,2105,1,0";

			[Test]
			public void ParameterMode_ParameterA_InPosition() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(001, 0));
			[Test]
			public void ParameterMode_ParameterA_InPosition2() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(120004, 0));
			[Test]
			public void ParameterMode_ParameterA_InImmediate() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(101, 0));
			[Test]
			public void ParameterMode_ParameterA_InImmediate2() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(120104, 0));
			[Test]
			public void ParameterMode_ParameterA_InRelative() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(201, 0));
			[Test]
			public void ParameterMode_ParameterA_InRelative2() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(120204, 0));

			[Test]
			public void ParameterMode_ParameterB_InPosition() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(0001, 1));
			[Test]
			public void ParameterMode_ParameterB_InPosition2() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(120104, 1));
			[Test]
			public void ParameterMode_ParameterB_InImmediate() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(1101, 1));
			[Test]
			public void ParameterMode_ParameterB_InImmediate2() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(101204, 1));
			[Test]
			public void ParameterMode_ParameterB_InRelative() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(2001, 1));
			[Test]
			public void ParameterMode_ParameterB_InRelative2() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(22104, 1));

			[Test]
			public void ParameterMode_ParameterC_InPosition() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(00001, 2));
			[Test]
			public void ParameterMode_ParameterC_InPosition2() => Assert.AreEqual(ParameterMode.Position, IntCodeProgram.OcodeParameterMode(02104, 2));
			[Test]
			public void ParameterMode_ParameterC_InImmediate() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(12001, 2));
			[Test]
			public void ParameterMode_ParameterC_InImmediate2() => Assert.AreEqual(ParameterMode.Immediate, IntCodeProgram.OcodeParameterMode(11204, 2));
			[Test]
			public void ParameterMode_ParameterC_InRelative() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(20001, 2));
			[Test]
			public void ParameterMode_ParameterC_InRelative2() => Assert.AreEqual(ParameterMode.Relative, IntCodeProgram.OcodeParameterMode(20104, 2));

			[Test]
			public void Part1_RelativeExample()
			{
				var compiler = new IntCodeCompiler();
				var program = compiler.ComputeStep(new IntCodeProgram(new long[] { 109, 19 }, 0, 2000));
				Assert.AreEqual(2019, program.RelativeBaseOffset);
			}

			[Test]
			public void Part1_RelativeExampleComplete()
			{
				var compiler = new IntCodeCompiler();
				var program = compiler.Compute(new IntCodeProgram(new long[] { 109, 1, 204, 1, 99 }, 0, 0));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be set to 1");
				Assert.AreEqual(204, compiler.OutputValue);
			}

			[Test]
			public void Part1_RelativeMultipleTime()
			{
				var compiler = new IntCodeCompiler();
				var program = compiler.Compute(new IntCodeProgram(new long[] { 109, 1, 109, 1, 109, 1, 109, 1, 99 }, 0, 0));
				Assert.AreEqual(4, program.RelativeBaseOffset, "The relative Offset should be set to 1");
			}

			[Test]
			public void Part1_KeepRelativeBase()
			{
				var compiler = new IntCodeCompiler(new long[] { 1 });
				var program = compiler.Compute(new IntCodeProgram(new long[] { 1101, 2, 3, 3, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an Addition");

				program = compiler.Compute(new IntCodeProgram(new long[] { 1102, 2, 3, 3, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an Multiply");

				program = compiler.Compute(new IntCodeProgram(new long[] { 3, 0, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an Input");

				program = compiler.Compute(new IntCodeProgram(new long[] { 4, 0, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an ouput");

				program = compiler.Compute(new IntCodeProgram(new long[] { 1105, 1, 3, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an JumpIfTrue");

				program = compiler.Compute(new IntCodeProgram(new long[] { 1106, 1, 3, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an JumpIfFalse");

				program = compiler.Compute(new IntCodeProgram(new long[] { 1107, 1, 3, 0, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an LessThen");

				program = compiler.Compute(new IntCodeProgram(new long[] { 1108, 1, 3, 0, 99 }, 0, 1));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be stay to 1 after an Equal");
			}

			[Test]
			public void Part1_Relative2()
			{
				var compiler = new IntCodeCompiler();
				var program = compiler.Compute(new IntCodeProgram(new long[] { 204, 0, 109, -1, 204, 5, 99 }, 0, 2));
				Assert.AreEqual(1, program.RelativeBaseOffset, "The relative Offset should be set to 1");
				Assert.AreEqual(10999, compiler.OutputValue);
			}

			[Test]
			public void Part1_OutsideMemory()
			{
				var compiler = new IntCodeCompiler(new long[] { 69 });
				var program = compiler.Compute(new IntCodeProgram(new long[] { 03, 5, 04, 5, 99 }, 0, 2));
				Assert.AreEqual(2, program.RelativeBaseOffset, "The relative Offset should be set to 0");
				TestUtility.AreEqual(new long[] { 03, 5, 04, 5, 99, 69 }, program.Memory.ToArray());
				Assert.AreEqual(69, compiler.OutputValue);
			}

			[Test]
			public void Part1_QuineSelfReplicating()
			{
				var intcode = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
				var compiler = new IntCodeCompiler();
				compiler.Compute(new IntCodeProgram(intcode.ToArray(), 0));
				IntCodeToBasic.ConvertWhileRunning(intcode.ToArray(), "Day9_Part1_QuineSelfReplicating.txt", false);
				TestUtility.AreEqual(intcode, compiler.OutputValues.ToArray());
			}

			[Test]
			public void Part1_16DigitNumber()
			{
				var intcode = new long[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
				var compiler = new IntCodeCompiler();
				compiler.Compute(new IntCodeProgram(intcode, 0));
				Assert.AreEqual(34915192L * 34915192L, compiler.OutputValue);
			}

			[Test]
			public void Part1_LargestNumber()
			{
				var intcode = new long[] { 104, 1125899906842624, 99 };
				var compiler = new IntCodeCompiler();
				compiler.Compute(new IntCodeProgram(intcode, 0));
				Assert.AreEqual(1125899906842624, compiler.OutputValue);
			}


			[Test]
			public void Part1()
			{

				var intcode = InputParser.ListOfLongs(Day9Input, ',');

				IntCodeToBasic.ConvertWhileRunning(new IntCodeCompiler(1), new IntCodeProgram(intcode.ToArray(), 0), "D9Part1_HumainCode.txt", false);

				var compiler = new IntCodeCompiler(1);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual(3533056970, compiler.OutputValue);
			}

			[Test]
			public void Part2()
			{

				var intcode = InputParser.ListOfLongs(Day9Input, ',');

				//Trop long !!
				//IntCodeToBasic.ConvertWhileRunning(new IntCodeCompiler(2), new IntCodeProgram(intcode.ToArray(), 0), "D9Part2_HumainCode.txt", false);

				var compiler = new IntCodeCompiler(2);
				compiler.Compute(new IntCodeProgram(intcode, 0));

				Assert.AreEqual(72852, compiler.OutputValue);
			}
		}
	}
}
