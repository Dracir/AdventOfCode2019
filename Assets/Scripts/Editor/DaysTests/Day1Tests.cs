using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day1Tests
	{
		public class Part1
		{

			[Test]
			public void AMassOf12_Gives2()
			{
				Assert.AreEqual(2, Day1Main.FuelRequirement(12));
			}
			[Test]
			public void AMassOf14_Gives2()
			{
				Assert.AreEqual(2, Day1Main.FuelRequirement(14));
			}
			[Test]
			public void AMassOf1969_Gives654()
			{
				Assert.AreEqual(654, Day1Main.FuelRequirement(1969));
			}
			[Test]
			public void AMassOf100756_Gives33583()
			{
				Assert.AreEqual(33583, Day1Main.FuelRequirement(100756));
			}
		}

		public class Part2
		{

			[Test]
			public void AMassOf12_Gives2()
			{
				Assert.AreEqual(2, Day1Main.RecursiveFuelRequirement(12));
			}
			[Test]
			public void AMassOf14_Gives2()
			{
				Assert.AreEqual(2, Day1Main.RecursiveFuelRequirement(14));
			}
			[Test]
			public void AMassOf1969_Gives966()
			{
				Assert.AreEqual(966, Day1Main.RecursiveFuelRequirement(1969));
			}
			[Test]
			public void AMassOf100756_Gives50346()
			{
				Assert.AreEqual(50346, Day1Main.RecursiveFuelRequirement(100756));
			}
		}
	}
}
