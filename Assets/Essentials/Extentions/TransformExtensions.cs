using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class TransformExtensions
{

	public static Transform[] GetChildren(this Transform parent, bool recursive = false)
	{
		var children = new List<Transform>(parent.childCount);

		for (int i = 0; i < parent.childCount; i++)
		{
			var child = parent.GetChild(i);
			children.Add(child);

			if (recursive && child.childCount > 0)
				children.AddRange(child.GetChildren(recursive));
		}

		return children.ToArray();
	}

	/// <summary>
	/// Destroys only and all Children got from "GetChildren()". Doesnt include self.
	/// </summary>
	public static void DestroyChildren(this Transform parent)
	{
		var children = parent.GetChildren();

		for (int i = 0; i < children.Length; i++)
			children[i].gameObject.Destroy();
	}


	/// <summary>
	/// Count all children and children of childrens
	/// </summary>
	public static int GetChildrenCountRecursive(this Transform parent)
	{
		if (parent.childCount == 0)
			return 0;

		int counter = 0;

		for (int i = 0; i < parent.childCount; i++)
			counter += 1 + parent.GetChild(i).GetChildrenCountRecursive();

		return counter;
	}
}
