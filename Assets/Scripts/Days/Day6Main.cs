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
		var tree = InputParser.ReadTree(inputText.Replace("\r", ""), '\n', ')');
		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log("Done Parsing tree"));

		var dictionaryNbOrbits = new Dictionary<string, int>();
		dictionaryNbOrbits.Add(tree.Root.Name, 0);

		AddChildToNbOrbits(tree.Root, dictionaryNbOrbits);

		return dictionaryNbOrbits.Sum(x => x.Value);
	}

	private static void AddChildToNbOrbits(TreeNode root, Dictionary<string, int> dictionaryNbOrbits)
	{
		int childNbOrbits = dictionaryNbOrbits[root.Name] + 1;
		foreach (var child in root.Children)
		{
			dictionaryNbOrbits.Add(child.Name, childNbOrbits);
			AddChildToNbOrbits(child, dictionaryNbOrbits);
		}
	}

	public static int Part2(string inputText)
	{
		var tree = InputParser.ReadTree(inputText.Replace("\r", ""), '\n', ')');

		var you = Find(tree.Root, "YOU");
		var san = Find(tree.Root, "SAN");

		var myParents = new Dictionary<string, int>();
		InputParser.TreeNode parent = you;
		int distance = 0;
		while (parent.Parent != null)
		{
			parent = parent.Parent;
			myParents.Add(parent.Name, ++distance);
		}

		parent = san;
		distance = 0;
		while (!myParents.ContainsKey(parent.Name))
		{
			parent = parent.Parent;
			distance++;
		}


		return distance + myParents[parent.Name] - 2;
	}

	private static InputParser.TreeNode Find(InputParser.TreeNode node, string v)
	{
		if (node.Children.Any(x => x.Name == v))
			return node.Children.First(x => x.Name == v);
		else
			foreach (var child in node.Children)
			{
				var result = Find(child, v);
				if (result != null)
					return result;
			}
		return null;
	}
}
