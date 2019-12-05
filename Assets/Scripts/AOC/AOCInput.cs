using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AOCInput
{

	private static string CachedInputFolderPath = "";
	private static string CachedOuputFolderPath = "";

	private static string GetPathFor(int day)
	{
		if (string.IsNullOrEmpty(CachedInputFolderPath))
			CachedInputFolderPath = Path.Combine(Path.Combine(Application.dataPath, "Resources"), "Inputs");

		return Path.Combine(CachedInputFolderPath, $"Day{day}Input.txt");
	}

	public static string GetInput(int day)
	{
		return File.ReadAllText(GetPathFor(day));
	}

	public static void WriteToFile(string fileName, string fileContent)
	{
		if (string.IsNullOrEmpty(CachedOuputFolderPath))
			CachedOuputFolderPath = Path.Combine(Path.Combine(Application.dataPath, "Resources"), "Outputs");
		File.WriteAllText(Path.Combine(CachedOuputFolderPath, fileName), fileContent);
	}
}
