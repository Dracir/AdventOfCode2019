using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static InputParser;
using System;

public class Day6Main
{

	public static int Part1(string inputText)
	{
		var tree = InputParser.ReadTree(inputText, '\n', ')');
		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log("Done Parsing tree"));

		var dictionaryNbOrbits = new Dictionary<string, int>();
		dictionaryNbOrbits.Add(tree.Root.Name, 0);

		AddChildToNbOrbits(tree.Root, dictionaryNbOrbits);

		return dictionaryNbOrbits.Sum(x => x.Value);
	}

	private static void AddChildToNbOrbits(TreeNode root, Dictionary<string, int> dictionaryNbOrbits)
	{
		int childNbOrbits = dictionaryNbOrbits[root.Name] + 1;
		foreach (var child in root.Childrend)
		{
			dictionaryNbOrbits.Add(child.Name, childNbOrbits);
			AddChildToNbOrbits(child, dictionaryNbOrbits);
		}
	}

	public static int Part2(string inputText)
	{
		return 2;
	}
}
