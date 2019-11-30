using System;

public static class RandomExtensions
{
	public static float Range(this Random random, float min, float max)
	{
        return min + ((float)random.NextDouble()) * (max-min);
	}
}
