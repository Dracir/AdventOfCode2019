using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IntCodeCompiler
{

	public Func<IntCodeProgram, IntCodeProgram>[] Instructions = new Func<IntCodeProgram, IntCodeProgram>[100];
	public long[] InstructionsSkips = new long[] { 1, 4, 4, 2, 2, 3, 3, 4, 4, 2 };

	public long[] InputValue;
	private long InputPointer = -1;

	public List<long> OutputValues = new List<long>();
	public long OutputValue => OutputValues.Count == 0 ? 0 : long.Parse(OutputConcat);
	public string OutputConcat => string.Join("", OutputValues.Select(x => x.ToString()));
	public string OutputAasListIntStr => string.Join(",", OutputValues.Select(x => x.ToString()));

	public bool PauseOnOutput;
	public bool Paused;


	public IntCodeCompiler(long inputValue, bool pauseOnOutput = false) : this(new long[] { inputValue }, pauseOnOutput)
	{ }

	public IntCodeCompiler(long[] inputValue = null, bool pauseOnOutput = false)
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
		Instructions[9] = AjustRelativeBase;
	}


	public void Clear()
	{
		OutputValues.Clear();
	}

	public void SetInputs(long[] inputs)
	{
		InputValue = inputs.ToArray();
		InputPointer = -1;
	}

	public static long[] Compute(long[] memory, long startingPointer = 0)
	{
		var program = new IntCodeProgram(memory, startingPointer);
		var compiler = new IntCodeCompiler();
		return compiler.Compute(program);
	}

	public static long[] ComputeStep(long[] memory, long startingPointer = 0)
	{
		var program = new IntCodeProgram(memory, startingPointer);
		var compiler = new IntCodeCompiler();
		return compiler.ComputeStep(program);
	}


	private IntCodeProgram Add(IntCodeProgram program)
	{
		program[program.ParameterCPointer] = program.ParameterAValue + program.ParameterBValue;
		return new IntCodeProgram(program, program.Pointer + 4, program.RelativeBaseOffset);
	}

	private IntCodeProgram Multiply(IntCodeProgram program)
	{
		program[program.ParameterCPointer] = program.ParameterAValue * program.ParameterBValue;
		return new IntCodeProgram(program, program.Pointer + 4, program.RelativeBaseOffset);
	}

	private IntCodeProgram Input(IntCodeProgram program)
	{
		var input = NextInput();
		program[program.ParameterAPointer] = input;
		return new IntCodeProgram(program.Memory, program.Pointer + 2, program.RelativeBaseOffset);
	}

	private long NextInput()
	{
		if (InputValue.Count() == 0)
			Debug.LogError("No Input ERROR");
		else if (InputPointer < InputValue.Count())
			InputPointer++;

		var input = InputValue[InputPointer];
		return input;
	}

	private IntCodeProgram Output(IntCodeProgram program)
	{
		OutputValues.Add(program.ParameterAValue);
		Paused = PauseOnOutput;
		return new IntCodeProgram(program, program.Pointer + 2, program.RelativeBaseOffset);
	}

	private IntCodeProgram JumpIfTrue(IntCodeProgram program)
	{
		if (program.ParameterAValue != 0)
			return new IntCodeProgram(program, program.ParameterBValue, program.RelativeBaseOffset);
		else
			return new IntCodeProgram(program, program.Pointer + 3, program.RelativeBaseOffset);
	}

	private IntCodeProgram JumpIfFalse(IntCodeProgram program)
	{
		if (program.ParameterAValue == 0)
			return new IntCodeProgram(program, program.ParameterBValue, program.RelativeBaseOffset);
		else
			return new IntCodeProgram(program, program.Pointer + 3, program.RelativeBaseOffset);
	}

	private IntCodeProgram LessThan(IntCodeProgram program)
	{
		var result = (program.ParameterAValue < program.ParameterBValue) ? 1 : 0;
		program[program.ParameterCPointer] = result;
		return new IntCodeProgram(program, program.Pointer + 4, program.RelativeBaseOffset);
	}

	private IntCodeProgram Equals(IntCodeProgram program)
	{
		var result = (program.ParameterAValue == program.ParameterBValue) ? 1 : 0;
		program[program.ParameterCPointer] = result;
		return new IntCodeProgram(program, program.Pointer + 4, program.RelativeBaseOffset);
	}

	private IntCodeProgram AjustRelativeBase(IntCodeProgram program)
	{
		var newRelativeBaseOffset = program.RelativeBaseOffset + program.ParameterAValue;
		return new IntCodeProgram(program, program.Pointer + 2, newRelativeBaseOffset);
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
