using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class InputParser
{
	public static int[] ListOfInts(string input) => input.Split('\n').Select(x => int.Parse(x)).ToArray();
	public static int[] ListOfInts(string input, char separator) => input.Split(separator).Select(x => int.Parse(x)).ToArray();

	/* a range in the format of "number-number" for exemple 130254-678275*/
	public static Vector2Int ParseRange(string input)
	{
		var split = input.Split('-');
		return new Vector2Int(Int32.Parse(split[0]), Int32.Parse(split[1]));
	}

	public static Tree ReadTree(string input, char lineSeparator, char linkSeparator)
	{
		Dictionary<string, TreeNode> outputNodes = new Dictionary<string, TreeNode>();
		outputNodes.Add("COM", new TreeNode("COM"));

		Dictionary<string, string> links = new Dictionary<string, string>();
		var remainingNodesToParse = input.Split(lineSeparator).Select(x =>
		{
			var split = x.Split(linkSeparator);
			return (split[0], split[1]);
		})
		.ToList();

		foreach (var link in remainingNodesToParse)
			links.Add(link.Item2, link.Item1);

		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Nodes : {links.Count}."));

		int changes = 1;
		while (links.Count != 0 && changes > 0)
		{
			changes = 0;
			foreach (var link in links.Where(x => outputNodes.ContainsKey(x.Value)).ToArray())
			{
				changes++;
				TreeNode parentNode = outputNodes[link.Value];
				TreeNode childNode = new TreeNode(link.Key);

				childNode.Parent = parentNode;
				parentNode.Childrend.Add(childNode);
				outputNodes.Add(link.Key, childNode);
				links.Remove(link.Key);
			}
			AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Remaining Nodes : {links.Count} with {changes} changes."));
		}


		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Remaining Nodes : {links.Count}"));
		var firstNode = outputNodes["COM"];

		return new Tree(outputNodes, firstNode);
	}


	public struct Tree
	{
		public Dictionary<string, TreeNode> Nodes;
		public TreeNode Root;

		public Tree(Dictionary<string, TreeNode> nodes, TreeNode root)
		{
			Nodes = nodes;
			Root = root;
		}
	}

	public class TreeNode
	{
		public string Name;
		public TreeNode Parent;
		public List<TreeNode> Childrend = new List<TreeNode>();

		public TreeNode(string name)
		{
			this.Name = name;
		}
	}
}