using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCodeToBasic
{
	public static void Convert(IntCodeProgram program)
	{
		var compiler = new IntCodeCompiler();
		string output = "";
		int pointer = 0;
		while (pointer < program.Memory.Length)
		{
			var instruction = program.Memory[pointer];
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
		AOCExecutor.ActionForMain.Enqueue(() => AOCInput.WriteToFile("D5Part1HumainCode.txt", output));
	}

	public static void ConvertWhileRunning(IntCodeCompiler intCodeCompiler, IntCodeProgram program)
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
		AOCExecutor.ActionForMain.Enqueue(() => AOCInput.WriteToFile("D5Part2HumainCode.txt", output));

	}

	private static string InstructionToCode(IntCodeProgram program, int instruction, int opcode, int pointer)
	{
		var output = "";
		var aValue = "Error";
		var a = 0;
		if (pointer + 1 < program.Memory.Length)
		{
			a = program.Memory[pointer + 1];
			aValue = IntCodeProgram.InstructionParameterAInPositionMode(instruction) ? $"[{a}]" : $"{a}";
		}
		var bValue = "Error";
		var b = 0;
		if (pointer + 2 < program.Memory.Length)
		{
			b = program.Memory[pointer + 2];
			bValue = IntCodeProgram.InstructionParameterBInPositionMode(instruction) ? $"[{b}]" : $"{b}";
		}
		var cValue = "Error";
		var c = 0;
		if (pointer + 3 < program.Memory.Length)
		{
			c = program.Memory[pointer + 3];
			cValue = IntCodeProgram.InstructionParameterCInPositionMode(instruction) ? $"[{c}]" : $"{c}";
		}

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

		return output;
	}
}
