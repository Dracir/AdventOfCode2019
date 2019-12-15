using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Day14Main
{

	public static long Part1(string inputText, long fuelCreated = 1)
	{
		var reactions = GetReactions(inputText.Replace("\r", ""));
		return Part1(reactions, fuelCreated);
	}

	public static long Part1(Dictionary<string, Reaction> reactions, long fuelCreated = 1)
	{
		var reactionUsed = new Dictionary<string, long>();
		reactionUsed.Add("ORE", 0);

		var leftOver = new Dictionary<string, long>();
		foreach (var item in reactions)
			leftOver.Add(item.Key, 0);
		leftOver.Add("ORE", 0);

		var neededToParse = new Queue<(string element, long amountNeeded)>();
		neededToParse.Enqueue(("FUEL", fuelCreated));

		while (neededToParse.Count != 0)
		{
			var elementToParse = neededToParse.Dequeue();
			var element = elementToParse.element;
			if (element == "ORE")
			{
				reactionUsed["ORE"] += elementToParse.amountNeeded;
				continue;
			};

			var reaction = reactions[element];
			var amountNeeded = elementToParse.amountNeeded;
			if (leftOver[element] >= amountNeeded)
			{
				leftOver[element] -= amountNeeded;
				amountNeeded = 0;
			}
			else
			{
				amountNeeded -= leftOver[element];
				leftOver[element] = 0;
			}

			var reactionTimes = amountNeeded / reaction.OutputAmount;
			if (amountNeeded % reaction.OutputAmount != 0)
				reactionTimes++;

			if (reactionTimes == 0)
				continue;

			leftOver[element] += reactionTimes * reaction.OutputAmount - amountNeeded;
			//Debug.Log($"{element} needed {amountNeeded} in {reactionTimes} reactions with {leftOver[element]} leftover. out {reaction.OutputAmount}");

			if (reactionUsed.ContainsKey(element))
				reactionUsed[element] += reactionTimes;
			else
				reactionUsed.Add(element, reactionTimes);

			foreach (var item in reaction.Ingredients)
				neededToParse.Enqueue((item.Key, reactionTimes * item.Value));
		}

		//PrintReactions(reactionUsed);

		return reactionUsed.Where(x => x.Key == "ORE").Sum(x => x.Value);
	}

	public static void PrintReactions(Dictionary<string, long> reactionsUsed)
	{
		var str = "";
		foreach (var item in reactionsUsed)
			str += $"{item.Key} {item.Value}\n";

		Debug.Log(str);
	}

	private static Dictionary<string, Reaction> GetReactions(string input)
	{
		var reactions = new Dictionary<string, Reaction>();

		foreach (var item in input.Split('\n'))
		{
			var ingredients = new Dictionary<string, long>();
			var splitted = item.Split('=');
			var outputPart = splitted[1].Split(' ');
			var outputAmount = long.Parse(outputPart[1]);
			var outputType = outputPart[2];

			foreach (var ingredient in splitted[0].Split(','))
			{
				var ingredientSplitted = ingredient.Trim().Split(' ');
				var ingredientAmount = long.Parse(ingredientSplitted[0]);
				var ingredientType = ingredientSplitted[1];
				ingredients.Add(ingredientType, ingredientAmount);
			}
			reactions.Add(outputType, new Reaction(outputAmount, ingredients));
		}

		return reactions;
	}

	public struct Reaction
	{
		public long OutputAmount;
		public Dictionary<string, long> Ingredients;

		public Reaction(long outputAmount, Dictionary<string, long> ingredients)
		{
			OutputAmount = outputAmount;
			Ingredients = ingredients;
		}
	}

	public static long Part2(string inputText)
	{
		var reactions = GetReactions(inputText.Replace("\r", ""));
		long maxOre = 1000000000000;
		long oreNeeded = 0;
		long fuelBase = 0;
		for (int power = 9; power > 0; power--)
		{
			long currentDigitPower = (long)System.Numerics.BigInteger.Pow(10, power - 1);
			bool found = false;
			for (int digit = 0; digit < 10; digit++)
			{
				long fuel = fuelBase + currentDigitPower * digit;
				oreNeeded = Part1(reactions, fuel);
				Debug.Log($"{fuel} => {oreNeeded}");
				if (oreNeeded >= maxOre)
				{
					found = true;
					fuelBase += currentDigitPower * (digit - 1);
					Debug.Log("New fuelBase : " + fuelBase);
					break;
				}
			}
			if (!found)
			{
				fuelBase += currentDigitPower * 9;
				Debug.Log("New fuelBase : " + fuelBase);
			}

		}

		return fuelBase;
	}
}