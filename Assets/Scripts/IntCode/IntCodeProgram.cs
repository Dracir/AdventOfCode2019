using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct IntCodeProgram
{
	public List<long> Memory;
	public long Pointer;
	public long RelativeBaseOffset;

	public long this[long key]
	{
		get
		{
			if (key >= MemoryLength)
				ResizeTo(key + 1);
			return Memory[(int)key];
		}
		set
		{
			if (key >= MemoryLength)
				ResizeTo(key + 1);
			Memory[(int)key] = value;
		}
	}
	public int MemoryLength => Memory.Count;

	private void ResizeTo(long v)
	{
		for (int i = MemoryLength; i < v; i++)
			Memory.Add(0);
	}

	public static implicit operator long[](IntCodeProgram program) => program.Memory.ToArray();

	public long Noun => this[1];
	public long Verb => this[2];
	public bool IsDone => this[Pointer] == 99;

	public long OpCodeAtPointer => this[Pointer];
	public long ParameterA => this[Pointer + 1];
	public long ParameterB => this[Pointer + 2];
	public long ParameterC => this[Pointer + 3];

	public long ParameterAPointer => ParameterPointer(OpCodeAtPointer, 0);
	public long ParameterBPointer => ParameterPointer(OpCodeAtPointer, 1);
	public long ParameterCPointer => ParameterPointer(OpCodeAtPointer, 2);

	public long ParameterAValue => ParameterValue(OpCodeAtPointer, 0);
	public long ParameterBValue => ParameterValue(OpCodeAtPointer, 1);
	public long ParameterCValue => ParameterValue(OpCodeAtPointer, 2);

	public long ParameterPointer(long instruction, int parameter)
	{
		var mode = OcodeParameterMode(instruction, parameter);
		switch (mode)
		{
			case ParameterMode.Position:
				return this[Pointer + parameter + 1];
			case ParameterMode.Immediate:
				Debug.Log("Pointer in immediate Mode !!!" + instruction);
				return 0;
			case ParameterMode.Relative:
				return RelativeBaseOffset + this[Pointer + parameter + 1];
			default: return 0;
		}
	}

	public long ParameterValue(long instruction, int parameter)
	{
		var mode = OcodeParameterMode(instruction, parameter);
		switch (mode)
		{
			case ParameterMode.Position:
				return this[this[Pointer + parameter + 1]];
			case ParameterMode.Immediate:
				return this[Pointer + parameter + 1];
			case ParameterMode.Relative:
				return this[RelativeBaseOffset + this[Pointer + parameter + 1]];
			default: return 0;
		}
	}

	public static ParameterMode OcodeParameterMode(long instruction, int parameterIndex)
	{
		var param = instruction / ((long)Math.Pow(10, parameterIndex + 2)) % 10;
		if (param == 0)
			return ParameterMode.Position;
		else if (param == 1)
			return ParameterMode.Immediate;
		else if (param == 2)
			return ParameterMode.Relative;
		return ParameterMode.Immediate;
	}

	public static bool InstructionParameterAInPositionMode(long instruction) => OcodeParameterMode(instruction, 0) == ParameterMode.Position;
	public static bool InstructionParameterBInPositionMode(long instruction) => OcodeParameterMode(instruction, 1) == ParameterMode.Position;
	public static bool InstructionParameterCInPositionMode(long instruction) => OcodeParameterMode(instruction, 2) == ParameterMode.Position;

	public IntCodeProgram(long[] memory, long pointer, long relativeBaseOffset = 0) : this(new List<long>(memory), pointer, relativeBaseOffset) { }
	public IntCodeProgram(List<long> memory, long pointer, long relativeBaseOffset = 0)
	{
		Memory = memory;
		Pointer = pointer;
		RelativeBaseOffset = relativeBaseOffset;
	}
}


public enum ParameterMode { Position, Immediate, Relative }
