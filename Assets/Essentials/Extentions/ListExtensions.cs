using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public static class ListExtensions {

	/// <summary>
	/// A Fisher-Yates shuffle with a set Random
	/// </summary>
	public static void Shuffle<T>(this IList<T> list, System.Random random)
	{
		int n = list.Count;
		
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			var value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	/// <summary>
	/// A Fisher-Yates shuffle
	/// </summary>
	public static void Shuffle<T>(this IList<T> list)
	{
		list.Shuffle(new System.Random(Random.Range(0,10000000)));
	}

	public static IList<T> WhereDistance<T>(this IList<T> list, Vector3 position, int min, int max) where T : MonoBehaviour
	{
		return list.Where( 
			i => 
			{
				var d = Vector3.Distance(i.transform.position,position);
				return d > min && d < max;
		 	}
		).ToList();
	}


	public static IList<T> WhereIn<T>(this IList<T> list, Collider2D collider) where T : MonoBehaviour{
		return list.Where( 
			i => { return collider.bounds.Contains(i.transform.position);}
		).ToList();
	}
	public static IList<T> WhereIn<T>(this IList<T> list, Collider collider) where T : MonoBehaviour{
		return list.Where( 
			i => { return collider.bounds.Contains(i.transform.position);}
		).ToList();
	}
	public static IList<T> WhereIn<T>(this IList<T> list, Bounds bounds) where T : MonoBehaviour{
		return list.Where( 
			i => { return bounds.Contains(i.transform.position);}
		).ToList();
	}
}
