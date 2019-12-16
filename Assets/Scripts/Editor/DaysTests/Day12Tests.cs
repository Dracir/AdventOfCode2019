using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day12Tests
	{
		[Test]
		public void Part1_Exemple1_Step1()
		{
			var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-1, 0, 2)   ,Vector3Int.zero),
				(new Vector3Int(2, -10, -7) ,Vector3Int.zero),
				(new Vector3Int(4, -8, 8)   ,Vector3Int.zero),
				(new Vector3Int(3, 5, -1)   ,Vector3Int.zero)
			};
			var expected = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(2, -1, 1)   ,new Vector3Int(3,-1,-1)),
				(new Vector3Int(3, -7, -4)  ,new Vector3Int(1,3,3)),
				(new Vector3Int(1, -7, 5)   ,new Vector3Int(-3,1,-3)),
				(new Vector3Int(2,  2, 0)   ,new Vector3Int(-1,-3,1))
			};

			Assert.AreEqual(expected, Day12Main.GravityStep(input));
		}

		[Test]
		public void Part1_Exemple1_Step10()
		{
			var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-1, 0, 2)   ,Vector3Int.zero),
				(new Vector3Int(2, -10, -7) ,Vector3Int.zero),
				(new Vector3Int(4, -8, 8)   ,Vector3Int.zero),
				(new Vector3Int(3, 5, -1)   ,Vector3Int.zero)
			};
			var expected = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(2, 1, -3)   ,new Vector3Int(-3,-2,1)),
				(new Vector3Int(1, -8, 0)  ,new Vector3Int(-1,1,3)),
				(new Vector3Int(3, -6, 1)   ,new Vector3Int(3,2,-3)),
				(new Vector3Int(2,  0, 4)   ,new Vector3Int(1,-1,-1))
			};

			for (int i = 0; i < 10; i++)
				input = Day12Main.GravityStep(input);

			Assert.AreEqual(expected, input);
		}

		[Test]
		public void Part1_Exemple1_TotalEnergy()
		{
			var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-1, 0, 2)   ,Vector3Int.zero),
				(new Vector3Int(2, -10, -7) ,Vector3Int.zero),
				(new Vector3Int(4, -8, 8)   ,Vector3Int.zero),
				(new Vector3Int(3, 5, -1)   ,Vector3Int.zero)
			};
			var expected = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(2, 1, -3)   ,new Vector3Int(-3,-2,1)),
				(new Vector3Int(1, -8, 0)  ,new Vector3Int(-1,1,3)),
				(new Vector3Int(3, -6, 1)   ,new Vector3Int(3,2,-3)),
				(new Vector3Int(2,  0, 4)   ,new Vector3Int(1,-1,-1))
			};

			for (int i = 0; i < 10; i++)
				input = Day12Main.GravityStep(input);

			var pot = Day12Main.TotalEnergy(input);

			Assert.AreEqual(179, pot);
		}

		[Test]
		public void Part2_Exemple1()
		{
			var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-1, 0, 2)   ,Vector3Int.zero),
				(new Vector3Int(2, -10, -7) ,Vector3Int.zero),
				(new Vector3Int(4, -8, 8)   ,Vector3Int.zero),
				(new Vector3Int(3, 5, -1)   ,Vector3Int.zero)
			};

			Assert.AreEqual(2772, Day12Main.Part2(input));
		}

		[Test]
		public void Part2_Exemple2()
		{
			var input = new (Vector3Int, Vector3Int)[] {
				(new Vector3Int(-8, -10, 0)   ,Vector3Int.zero),
				(new Vector3Int(5, 5, 10) ,Vector3Int.zero),
				(new Vector3Int(2, -7,3)   ,Vector3Int.zero),
				(new Vector3Int(9, -8, -3)   ,Vector3Int.zero)
			};

			Assert.AreEqual(4686774924, Day12Main.Part2(input));
		}
	}
}
