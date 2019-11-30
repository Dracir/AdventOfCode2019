using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DaysCreator
{

	private static string MainFileText = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DayXMain
{

	public static string Part1(string inputText)
	{
		return ""1"";
	}

	public static string Part2(string inputText)
	{
		return ""2"";
	}
}
";


	[MenuItem("AdventOfCode/MakeAventOfCodeDays", false, 999)]
	static void MakeDays()
	{
		MakeDaysScript();
		//MakeDaysInputFile();
	}

	private static void MakeDaysInputFile()
	{
		var resourceFolderPath = Path.Combine(Application.dataPath, "Resources");
		Directory.CreateDirectory(resourceFolderPath);
		var inputFolderPath = Path.Combine(resourceFolderPath, "Inputs");
		Directory.CreateDirectory(inputFolderPath);

		for (int day = 1; day <= 25; day++)
		{
			var path = Path.Combine(inputFolderPath, $"Day{day}Input.txt");
			File.WriteAllText(path, "");
		}

	}

	private static void MakeDaysScript()
	{
		var scriptFolderPath = Path.Combine(Application.dataPath, "Scripts");
		Directory.CreateDirectory(scriptFolderPath);
		var scriptDaysPath = Path.Combine(scriptFolderPath, "Days");
		Directory.CreateDirectory(scriptDaysPath);
		var basePath = Application.dataPath;


		for (int day = 1; day <= 25; day++)
		{
			MakeDayScript(day, scriptDaysPath);
		}
	}

	private static void MakeDayScript(int day, string scriptDaysPath)
	{
		var dayPath = Path.Combine(scriptDaysPath, $"Day{day}");
		Directory.CreateDirectory(dayPath);
		var mainPath = Path.Combine(dayPath, $"Day{day}Main.cs");
		var txt = MainFileText.Replace("DayXMain",$"Day{day}Main");
		File.WriteAllText(mainPath, txt);

	}
}
