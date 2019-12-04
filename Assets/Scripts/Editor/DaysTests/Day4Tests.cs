using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
	public class Day4Tests
	{
		[Test]
		public void Part1_Exemple1()
		{
			Assert.AreEqual(true, Day4Main.IsValidPassword(111111));
		}
		[Test]
		public void Part1_Exemple2()
		{
			Assert.AreEqual(false, Day4Main.IsValidPassword(223450));
		}
		[Test]
		public void Part1_Exemple3()
		{
			Assert.AreEqual(false, Day4Main.IsValidPassword(123789));
		}
		[Test]
		public void Part2_Exemple1()
		{
			Assert.AreEqual(true, Day4Main.IsValidPasswordPart2(112233));
		}
		[Test]
		public void Part2_Exemple2()
		{
			Assert.AreEqual(false, Day4Main.IsValidPasswordPart2(123444));
		}
		[Test]
		public void Part2_Exemple3()
		{
			Assert.AreEqual(true, Day4Main.IsValidPasswordPart2(111122));
		}

		[Test]
		public void Part2_CustomExempleDoubleAndTrio()
		{
			Assert.AreEqual(true, Day4Main.IsValidPasswordPart2(112333));
		}
	}
}
