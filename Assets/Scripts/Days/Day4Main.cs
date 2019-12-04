using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day4Main
{

	public static string Part1(string inputText)
	{
		var range = InputParser.ParseRange(inputText);

		int validPword = 0;
		for (int i = range.x; i <= range.y; i++)
			if (IsValidPassword(i)) validPword++;

		return validPword + "";
	}

	public static bool IsValidPassword(int v)
	{
		var digits = GetIntArray(v);
		if (digits.Length != 6) return false;
		var hadDouble = false;
		for (int i = 1; i < 6; i++)
		{
			if (digits[i] < digits[i - 1])
				return false;
			if (digits[i] == digits[i - 1])
				hadDouble = true;
		}
		return hadDouble;
	}

	static int[] GetIntArray(int num)
	{
		List<int> listOfInts = new List<int>();
		while (num > 0)
		{
			listOfInts.Add(num % 10);
			num = num / 10;
		}
		listOfInts.Reverse();
		return listOfInts.ToArray();
	}


	public static string Part2(string inputText)
	{
		var range = InputParser.ParseRange(inputText);

		int validPword = 0;
		for (int i = range.x; i <= range.y; i++)
			if (IsValidPasswordPart2(i)) validPword++;

		return validPword + "";
	}

	public static bool IsValidPasswordPart2(int v)
	{
		if (!IsValidPassword(v)) return false;
		var digits = GetIntArray(v);
		bool hasLargeGroup = false;
		bool hasDouble = false;
		for (int i = 0; i <= 9; i++)
		{
			int nbRepeat = 0;
			for (int j = 0; j < 6; j++)
				if (digits[j] == i)
					nbRepeat++;
			if (nbRepeat >= 3)
				hasLargeGroup = true;

			if (nbRepeat % 2 == 0 && nbRepeat == 2)
				hasDouble = true;
		}
		if(hasLargeGroup && !hasDouble) return false;

		return hasDouble;
	}
}
