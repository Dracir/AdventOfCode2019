using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AOCInput
{

	private static string CachedFolderPath = "";

	private static string GetPathFor(int day)
	{
		if (string.IsNullOrEmpty(CachedFolderPath))
			CachedFolderPath = Path.Combine(Path.Combine(Application.dataPath, "Resources"), "Inputs");

		return Path.Combine(CachedFolderPath, $"Day{day}Input.txt");
	}


	public static string GetInput(int day)
	{
		return File.ReadAllText(GetPathFor(day));
	}
}
