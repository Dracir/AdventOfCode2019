using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IntCodeProgram
{
	public int[] Memory;
	public int Pointer;

	public int Noun { get { return Memory[1]; } }
	public int Verb { get { return Memory[2]; } }
	public bool IsDone { get { return Memory[Pointer] == 99; } }

	public int OpCodeAtPointer { get { return Memory[Pointer]; } }
	public int ParameterA { get { return Memory[Pointer + 1]; } }
	public int ParameterB { get { return Memory[Pointer + 2]; } }
	public int ParameterC { get { return Memory[Pointer + 3]; } }
	public int ParameterD { get { return Memory[Pointer + 4]; } }

	public int ValueAtParameterA { get { return Memory[Memory[Pointer + 1]]; } }
	public int ValueAtParameterB { get { return Memory[Memory[Pointer + 2]]; } }
	public int ValueAtParameterC { get { return Memory[Memory[Pointer + 3]]; } }
	public int ValueAtParameterD { get { return Memory[Memory[Pointer + 4]]; } }

	public IntCodeProgram(int[] memory, int pointer)
	{
		Memory = memory;
		Pointer = pointer;
	}
}
