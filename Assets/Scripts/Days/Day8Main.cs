using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class Day8Main
{

	public static int Part1(string inputText)
	{
		var pixels = InputParser.ListOfDigitNoSeparator(inputText);
		var layers = InputToImageLayers(pixels, 25, 6);
		int lowerIndex = 0;
		int fewest0 = int.MaxValue;

		for (int i = 0; i < layers.Length; i++)
		{
			var nb0 = layers[i].ImagePixels.Cast<int>().Count(x => x == 0);
			if (nb0 < fewest0)
			{
				fewest0 = nb0;
				lowerIndex = i;
			}
		}

		var nb1 = layers[lowerIndex].ImagePixels.Cast<int>().Count(x => x == 1);
		var nb2 = layers[lowerIndex].ImagePixels.Cast<int>().Count(x => x == 2);
		return nb1 * nb2;
	}

	public static ImageLayer[] InputToImageLayers(int[] pixels, int width, int height)
	{
		var images = new List<ImageLayer>();
		var pixelPerImage = (width * height);
		var nbImages = pixels.Length / pixelPerImage;
		int pixel = 0;
		for (int i = 0; i < nbImages; i++)
		{
			var imagePixels = new int[height, width];
			for (int pi = 0; pi < pixelPerImage; pi++)
				imagePixels[pi / width, pi % width] = pixels[pixel++];
			images.Add(new ImageLayer(imagePixels));
		}
		return images.ToArray();
	}

	public struct ImageLayer
	{
		public int[,] ImagePixels;

		public ImageLayer(int[,] imagePixels)
		{
			this.ImagePixels = imagePixels;
		}
	}

	public static int Part2(string inputText)
	{
		var pixels = InputParser.ListOfDigitNoSeparator(inputText);
		var layers = InputToImageLayers(pixels, 25, 6);
		var image = GetImage(layers);

		var str = "";
		for (int y = 0; y < 6; y++)
		{
			for (int x = 0; x < 25; x++)
			{
				var value = image[y, x];
				if (value == 0)
					str += '.';
				else if (value == 1)
					str += '#';
				else
					str += ' ';
			}
			str += '\n';
		}

		AOCExecutor.ActionForMain.Enqueue(() => ImageToTexturee(image, 25, 6, AOCUI.Instance.Part1ComputeOutput));
		Debug.Log(str);

		return 2;
	}

	private static void ImageToTexturee(int[,] image, int w, int h, RawImage target)
	{
		var texture = new Texture2D(w, h);

		var colors = new Color32[w * h];

		int i = 0;
		for (int y = 5; y >=0; y--)
		{
			for (int x = 0; x < 25; x++)
			{
				var value = image[y, x];
				if (value == 0)
					colors[i++] = Color.black;
				else if (value == 1)
					colors[i++] = Color.white;
				else
					colors[i++] = Color.green;
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.SetPixels32(colors);
		texture.Apply();
		target.texture = texture;
	}

	public static int[,] GetImage(ImageLayer[] layers)
	{
		var w = layers[0].ImagePixels.GetLength(1);
		var h = layers[0].ImagePixels.GetLength(0);
		var nbLayers = layers.Count();
		var result = new int[h, w];
		for (int x = 0; x < w; x++)
		{
			for (int y = 0; y < h; y++)
			{
				int i = -1;
				while (i++ < nbLayers && layers[i].ImagePixels[y, x] == 2) ;
				if (i < nbLayers)
					result[y, x] = layers[i].ImagePixels[y, x];
				else
					result[y, x] = 2;
			}
		}
		return result;
	}
}
