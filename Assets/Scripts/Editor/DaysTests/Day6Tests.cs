using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Day6Tests
    {
        [Test]
        public void Exemple1()
        {
			var input = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L";
			Assert.AreEqual(42,Day6Main.Part1(input));
        }
		
		[Test]
        public void SingleNode()
        {
			var input = "COM)B";
			Assert.AreEqual(1,Day6Main.Part1(input));
        }
		
		[Test]
        public void SingleLineNode()
        {
			var input = "COM)A\nA)B";
			Assert.AreEqual(3,Day6Main.Part1(input));
        }

		[Test]
        public void DualChilds()
        {
			var input = "COM)A\nCOM)B";
			Assert.AreEqual(2,Day6Main.Part1(input));
        }

    }
}
