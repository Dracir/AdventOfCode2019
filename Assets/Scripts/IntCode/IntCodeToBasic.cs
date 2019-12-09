using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCodeToBasic
{
	public static void Convert(IntCodeProgram program, string filename)
	{
		var compiler = new IntCodeCompiler();
		string output = "";
		long pointer = 0;
		while (pointer < program.MemoryLength)
		{
			var instruction = program[pointer];
			var opcode = instruction % 100;
			if (opcode == 99)
			{
				output += "Halt";
				break;
			}
			else if (opcode > compiler.Instructions.Length || compiler.Instructions[opcode] == null)
			{
				output += "! Missing Instruction !\n";
				break;
			}
			else
			{
				output += InstructionToCode(program, instruction, opcode, pointer);
				pointer += compiler.InstructionsSkips[opcode];
			}
		}
		if (Application.isPlaying)
			AOCExecutor.ActionForMain.Enqueue(() => AOCInput.WriteToFile(filename, output));
		else
			AOCInput.WriteToFile(filename, output);
	}

	public static void ConvertWhileRunning(long[] intcode, string filename, bool useExecutorActionForMain) 
		=> ConvertWhileRunning(new IntCodeCompiler(), new IntCodeProgram(intcode, 0), filename, useExecutorActionForMain);
		
	public static void ConvertWhileRunning(IntCodeCompiler intCodeCompiler, IntCodeProgram program, string filename, bool useExecutorActionForMain)
	{
		string output = "";
		int maxSteps = 100;
		while (!program.IsDone || maxSteps-- > 0)
		{
			var instruction = program.OpCodeAtPointer;
			var opcode = instruction % 100;
			if (opcode == 99)
			{
				output += "Halt";
				break;
			}
			else if (opcode > intCodeCompiler.Instructions.Length || intCodeCompiler.Instructions[opcode] == null)
			{
				output += $"! Missing Instruction {opcode}!\n";
				break;
			}
			else
			{
				output += InstructionToCode(program, instruction, opcode, program.Pointer);
				program = intCodeCompiler.ComputeStep(program);
			}

		}

		if (useExecutorActionForMain)
			AOCExecutor.ActionForMain.Enqueue(() => AOCInput.WriteToFile(filename, output));
		else
			AOCInput.WriteToFile(filename, output);

	}

	private static string InstructionToCode(IntCodeProgram program, long instruction, long opcode, long pointer)
	{
		var output = "";

		long a = program.ParameterA;
		var aValue = GetParamValue(program, instruction, a, 0);

		long b = program.ParameterA;
		var bValue = GetParamValue(program, instruction, b, 1);

		long c = program.ParameterA;
		var cValue = GetParamValue(program, instruction, c, 2);

		if (opcode == 1)
			return $"[{c}] = {aValue} + {bValue}\n";
		else if (opcode == 2)
			return $"[{c}] = {aValue} * {bValue}\n";
		else if (opcode == 3)
			return $"[{a}] = Input()\n";
		else if (opcode == 4)
			return $"Output += [{a}]\n";
		else if (opcode == 5)
			return $"IP = ({aValue} != 0) ? {bValue} : IP + 3\n";
		else if (opcode == 6)
			return $"IP = ({aValue} == 0) ? {bValue} : IP + 3\n";
		else if (opcode == 7)
			return $"[{c}] = ({aValue} < {bValue}) ? 1 : 0\n";
		else if (opcode == 8)
			return $"[{c}] = ({aValue} == {bValue}) ? 1 : 0\n";
		else if (opcode == 9)
			return $"RelativeBase = {program.RelativeBaseOffset} + {aValue}\n";

		return output;
	}

	private static string GetParamValue(IntCodeProgram program, long instruction, long value, int param)
	{
		var mode = IntCodeProgram.OcodeParameterMode(instruction, param);
		switch (mode)
		{
			case ParameterMode.Position: return $"[{value}]";
			case ParameterMode.Immediate: return $"{value}";
			case ParameterMode.Relative: return $"[{value} + {program.RelativeBaseOffset}]";
			default: return "";
		}

	}
}
