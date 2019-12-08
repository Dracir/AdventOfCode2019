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
		var outputNodes = new List<TreeNode>();
		outputNodes.Add(new TreeNode("COM"));

		var nodeDick = new Dictionary<string, TreeNode>();
		var links = input.Split(lineSeparator).Select(x =>
		{
			var split = x.Split(linkSeparator);
			return (split[0], split[1]);
		}).ToList();

		Debug.Log(string.Join(",", links.Select(x => x.Item1 + " ) " + x.Item2).ToArray()));
		foreach (var link in links)
		{
			var node = new TreeNode(link.Item2);
			outputNodes.Add(node);
			nodeDick.Add(link.Item2, node);
		}

		foreach (var item in nodeDick)
			Debug.Log($"Key: {item.Key}, Value: {item.Value}");

		Debug.Log(string.Join(",", outputNodes.Select(x => x.Name).ToArray()));

		foreach (var link in links)
		{
			var parentId = link.Item1;
			if (nodeDick.ContainsKey(parentId))
			{
				Debug.Log($"Unknown item {parentId}");
				continue;
			}
			else
				Debug.Log($"trouvé item{parentId}");

			Debug.Log(string.Join(",", nodeDick.Select(x => x.Key).ToArray()));
			var parent = nodeDick[link.Item1];
			var child = nodeDick[link.Item2];
			child.Parent = parent;
			parent.Childrend.Add(child);
		}

		/*

		foreach (var link in remainingNodesToParse)
			links.Add(link.Item2, link.Item1);

		int a = links.Count;

		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Nodes : {a}."));

		int changes = 1;
		while (links.Count != 0 && changes > 0)
		{
			var stuff = outputNodes.Keys.ToArray();
			int nbOutputs = outputNodes.Count();
			var has4FT = outputNodes.ContainsKey("4FT");
			var nbneed4FT = links.Where(x => x.Value == "4FT").Count();
			AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"nbOutputs {nbOutputs} " + string.Join(",", stuff) + " - " + nbneed4FT));
			AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"needs 4ft: {links.Where(x => x.Value == "4FT").Count()}  exists {has4FT}."));
			changes = 0;
			foreach (var link in links.Where(x => outputNodes.ContainsKey(x.Value)).ToArray())
			{
				changes++;
				AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Adding {link.Key}."));
				TreeNode parentNode = outputNodes[link.Value];
				TreeNode childNode = new TreeNode(link.Key);

				childNode.Parent = parentNode;
				parentNode.Childrend.Add(childNode);
				outputNodes.Add(link.Key, childNode);
				var containnew = outputNodes.ContainsKey(link.Key);
				AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"contain new key {link.Key} {containnew}."));
				links.Remove(link.Key);
			}
			int count = links.Count;
			int change = changes;
			AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Remaining Nodes : {count} with {change} changes."));
		}


		AOCExecutor.ActionForMain.Enqueue(() => Debug.Log($"Remaining Nodes : {links.Count}"));*/


		var firstNode = outputNodes.First(x => x.Name == "COM");

		return new Tree(null, firstNode);
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