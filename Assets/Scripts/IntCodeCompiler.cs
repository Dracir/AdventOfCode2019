using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCodeCompiler
{

	public Func<IntCodeProgram, IntCodeProgram>[] Instructions = new Func<IntCodeProgram, IntCodeProgram>[99];

	public IntCodeCompiler()
	{
		Instructions[1] = Add;
		Instructions[2] = Multiply;
	}

	public static int[] Compute(int[] memory, int startingPointer = 0)
	{
		var program = new IntCodeProgram(memory, startingPointer);
		var compiler = new IntCodeCompiler();
		return compiler.Compute(program).Memory;
	}

	public static int[] ComputeStep(int[] memory, int startingPointer = 0)
	{
		var program = new IntCodeProgram(memory, startingPointer);
		var compiler = new IntCodeCompiler();
		return compiler.ComputeStep(program).Memory;
	}

	private IntCodeProgram Add(IntCodeProgram program)
	{
		var memory = program.Memory;
		memory[program.ParameterC] = program.ValueAtParameterA + program.ValueAtParameterB;
		return new IntCodeProgram(memory, program.Pointer + 4);
	}

	private IntCodeProgram Multiply(IntCodeProgram program)
	{
		var memory = program.Memory;
		memory[program.ParameterC] = program.ValueAtParameterA * program.ValueAtParameterB;
		return new IntCodeProgram(memory, program.Pointer + 4);
	}


	public IntCodeProgram Compute(IntCodeProgram program)
	{
		while (!program.IsDone)
			program = ComputeStep(program);
		return program;
	}

	public IntCodeProgram ComputeStep(IntCodeProgram program)
	{
		if (program.IsDone)
			return program;
		var instruction = program.OpCodeAtPointer;
		if (Instructions[instruction] == null)
		{
			Debug.LogError("Missing instruction " + instruction);
			return program;
		}
		else
			return Instructions[instruction](program);
	}

}
