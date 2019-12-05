using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCodeCompiler
{

	public Func<IntCodeProgram, IntCodeProgram>[] Instructions = new Func<IntCodeProgram, IntCodeProgram>[100];
	public int[] InstructionsSkips = new int[] { 1, 4, 4, 2, 2, 3, 3, 4, 4 };

	public int InputValue = 0;

	public string OutputValue = "";

	public IntCodeCompiler(int inputValue = 0)
	{
		InputValue = inputValue;

		Instructions[1] = Add;
		Instructions[2] = Multiply;
		Instructions[3] = Input;
		Instructions[4] = Output;
		Instructions[5] = JumpIfTrue;
		Instructions[6] = JumpIfFalse;
		Instructions[7] = LessThan;
		Instructions[8] = Equals;
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
		memory[program.ParameterCPosition] = program.ParameterAValue + program.ParameterBValue;
		return new IntCodeProgram(memory, program.Pointer + 4);
	}

	private IntCodeProgram Multiply(IntCodeProgram program)
	{
		var memory = program.Memory;
		memory[program.ParameterCPosition] = program.ParameterAValue * program.ParameterBValue;
		return new IntCodeProgram(memory, program.Pointer + 4);
	}

	private IntCodeProgram Input(IntCodeProgram program)
	{
		program.Memory[program.ParameterAPosition] = InputValue;
		return new IntCodeProgram(program.Memory, program.Pointer + 2);
	}

	private IntCodeProgram Output(IntCodeProgram program)
	{
		OutputValue += program.Memory[program.ParameterAPosition] + "";
		return new IntCodeProgram(program.Memory, program.Pointer + 2);
	}

	private IntCodeProgram JumpIfTrue(IntCodeProgram program)
	{
		if (program.ParameterAValue != 0)
			return new IntCodeProgram(program.Memory, program.ParameterBValue);
		else
			return new IntCodeProgram(program.Memory, program.Pointer + 3);
	}

	private IntCodeProgram JumpIfFalse(IntCodeProgram program)
	{
		if (program.ParameterAValue == 0)
			return new IntCodeProgram(program.Memory, program.ParameterBValue);
		else
			return new IntCodeProgram(program.Memory, program.Pointer + 3);
	}

	private IntCodeProgram LessThan(IntCodeProgram program)
	{
		var result = (program.ParameterAValue < program.ParameterBValue) ? 1 : 0;
		var memory = program.Memory;
		memory[program.ParameterCPosition] = result;
		return new IntCodeProgram(memory, program.Pointer + 4);
	}

	private IntCodeProgram Equals(IntCodeProgram program)
	{
		var result = (program.ParameterAValue == program.ParameterBValue) ? 1 : 0;
		var memory = program.Memory;
		memory[program.ParameterCPosition] = result;
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
		var instruction = program.OpCodeAtPointer % 100;

		if (Instructions[instruction] == null)
		{
			Debug.LogError("Missing instruction " + instruction);
			return program;
		}
		else
			return Instructions[instruction](program);
	}

}
