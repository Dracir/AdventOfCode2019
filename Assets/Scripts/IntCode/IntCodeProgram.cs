using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public struct IntCodeProgram
{
	public int[] Memory;
	public int Pointer;

	public int Noun => Memory[1];
	public int Verb => Memory[2];
	public bool IsDone => Memory[Pointer] == 99;

	public int OpCodeAtPointer => Memory[Pointer];
	public int ParameterAPosition => Memory[Pointer + 1];
	public int ParameterBPosition => Memory[Pointer + 2];
	public int ParameterCPosition => Memory[Pointer + 3];
	public int ParameterDPosition => Memory[Pointer + 4];

	public int ParameterAImmediate => Memory[Memory[Pointer + 1]];
	public int ParameterBImmediate => Memory[Memory[Pointer + 2]];
	public int ParameterCImmediate => Memory[Memory[Pointer + 3]];
	public int ParameterDImmediate => Memory[Memory[Pointer + 4]];

	public int ParameterAValue => IsParameterAInPositionMode ? ParameterAImmediate : ParameterAPosition;
	public int ParameterBValue => IsParameterBInPositionMode ? ParameterBImmediate : ParameterBPosition;
	public int ParameterCValue => IsParameterCInPositionMode ? ParameterCImmediate : ParameterCPosition;
	public int ParameterDValue => IsParameterDInPositionMode ? ParameterDImmediate : ParameterDPosition;

	public bool IsParameterAInPositionMode => InstructionParameterAInPositionMode(Memory[Pointer + 0]);
	public bool IsParameterBInPositionMode => InstructionParameterBInPositionMode(Memory[Pointer + 0]);
	public bool IsParameterCInPositionMode => InstructionParameterCInPositionMode(Memory[Pointer + 0]);
	public bool IsParameterDInPositionMode => InstructionParameterDInPositionMode(Memory[Pointer + 0]);

	public static bool InstructionParameterAInPositionMode(int instruction) => !new Regex(@"^.*1..$").IsMatch(instruction.ToString());
	public static bool InstructionParameterBInPositionMode(int instruction) => !new Regex(@"^.*1...$").IsMatch(instruction.ToString());
	public static bool InstructionParameterCInPositionMode(int instruction) => !new Regex(@"^.*1....$").IsMatch(instruction.ToString());
	public static bool InstructionParameterDInPositionMode(int instruction) => !new Regex(@"^.*1.....$").IsMatch(instruction.ToString());

	public IntCodeProgram(int[] memory, int pointer)
	{
		Memory = memory;
		Pointer = pointer;
	}

}
