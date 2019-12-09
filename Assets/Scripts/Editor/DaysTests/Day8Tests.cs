using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day8Tests
	{
		[Test]
		public void Exemple1_Has2Layers()
		{
			var layers = Day8Main.InputToImageLayers(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 }, 3, 2);

			Assert.AreEqual(2, layers.Length, "Has 2 layers");
		}

		[Test]
		public void Exemple1_Pixels()
		{
			var layers = Day8Main.InputToImageLayers(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 }, 3, 2);

			Assert.AreEqual(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } }, layers[0].ImagePixels);
			Assert.AreEqual(new int[,] { { 7, 8, 9 }, { 0, 1, 2 } }, layers[1].ImagePixels);
		}

		[Test]
		public void Part2_Exemple1()
		{
			var layers = Day8Main.InputToImageLayers(new int[] { 0, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 2, 0, 0, 0, 0 }, 2, 2);
			var image = Day8Main.GetImage(layers);

			Assert.AreEqual(new int[,] { { 0, 1 }, { 1, 0 } }, image);
		}

	}
}
