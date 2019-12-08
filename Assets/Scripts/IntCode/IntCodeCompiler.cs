using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IntCodeCompiler
{

	public Func<IntCodeProgram, IntCodeProgram>[] Instructions = new Func<IntCodeProgram, IntCodeProgram>[100];
	public int[] InstructionsSkips = new int[] { 1, 4, 4, 2, 2, 3, 3, 4, 4 };

	public int[] InputValue;
	private int InputPointer = -1;

	public string OutputValue = "";
	public bool PauseOnOutput;
	public bool Paused;


	public IntCodeCompiler(int inputValue, bool pauseOnOutput = false) : this(new int[] { inputValue }, pauseOnOutput)
	{ }

	public IntCodeCompiler(int[] inputValue = null, bool pauseOnOutput = false)
	{
		PauseOnOutput = pauseOnOutput;
		if (inputValue != null)
			InputValue = inputValue;
		InputPointer = -1;

		Instructions[1] = Add;
		Instructions[2] = Multiply;
		Instructions[3] = Input;
		Instructions[4] = Output;
		Instructions[5] = JumpIfTrue;
		Instructions[6] = JumpIfFalse;
		Instructions[7] = LessThan;
		Instructions[8] = Equals;
	}


	public void Clear()
	{
		OutputValue = "";
	}

	public void SetInputs(int[] inputs)
	{
		InputValue = inputs.ToArray();
		InputPointer = -1;
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
		int input = NextInput();
		program.Memory[program.ParameterAPosition] = input;
		return new IntCodeProgram(program.Memory, program.Pointer + 2);
	}

	private int NextInput()
	{
		if (InputPointer < InputValue.Count())
			InputPointer++;
		var input = InputValue[InputPointer];
		return input;
	}

	private IntCodeProgram Output(IntCodeProgram program)
	{
		OutputValue += program.ParameterAValue + "";
		if (PauseOnOutput)
			Paused = true;
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
		Paused = false;
		while (!program.IsDone && !Paused)
			program = ComputeStep(program);
		return program;
	}

	public IntCodeProgram ComputeStep(IntCodeProgram program)
	{
		Paused = false;
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
