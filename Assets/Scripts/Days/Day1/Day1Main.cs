using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Day1Main
{

	public static string Part1(string inputText)
	{
		var values = InputParser.ListOfInts(inputText);
		return values.Select(x => FuelRequirement(x)).Sum() + "";
	}

	public static string StartPart1ComputeShader(string inputText)
	{
		var values = InputParser.ListOfInts(inputText);
		var compute = AOCComputerShaders.Instance.Day1ComputeShader;
		var kernel = compute.FindKernel("Day1Main");

		var inputTexture = new Texture2D(1, values.Length);
		var outputTexture = new RenderTexture(1, values.Length, 24);
		outputTexture.filterMode = FilterMode.Point;
		outputTexture.useMipMap = false;
		outputTexture.antiAliasing = 1;
		outputTexture.enableRandomWrite = true;
		outputTexture.Create();
		AOCUI.Instance.Part1ComputeInput.texture = inputTexture;
		AOCUI.Instance.Part1ComputeOutput.texture = outputTexture;

		var colors = new Color32[values.Length];
		var max = values.Max();
		for (int i = 0; i < values.Length; i++)
			colors[i] = Color.Lerp(Color.black, Color.white, ((float)values[i]) / max);
		inputTexture.SetPixels32(colors);
		inputTexture.Apply();

		compute.SetTexture(kernel, "Input", inputTexture);
		compute.SetTexture(kernel, "Result", outputTexture);
		compute.Dispatch(kernel, 100, 100, 1);

		return values.Select(x => FuelRequirement(x)).Sum() + "";
	}

	public static int FuelRequirement(int mass)
	{
		return (int)(mass / 3f) - 2;
	}

	public static string Part2(string inputText)
	{
		var values = InputParser.ListOfInts(inputText);
		return values.Select(x => RecursiveFuelRequirement(x)).Sum() + "";
	}

	public static int RecursiveFuelRequirement(int mass)
	{
		var fuel = (int)(mass / 3f) - 2;
		if (fuel <= 0)
			return 0;
		else
			return fuel + RecursiveFuelRequirement(fuel);
	}
}
