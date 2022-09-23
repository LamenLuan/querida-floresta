using System;
using UnityEngine;

public static class Extensions
{
	public static bool KeyboardDown()
	{
		if (Input.anyKeyDown)
		{
			for (int i = 0; i < 3; i++)
				if (Input.GetMouseButtonDown(i)) return false;

			return true;
		}

		return false;
	}

	public static void Clear(this int[] array)
	{
		for (int i = 0; i < array.Length; i++) array[i] = 0;
	}

	public static void Clear(this double[] array)
	{
		for (int i = 0; i < array.Length; i++) array[i] = 0;
	}

	public static void Validate(this int[] array)
	{
		for (int i = 0; i < array.Length; i++) if (array[i] < 0) array[i] = 0;
	}

	public static void Validate(this double[] array)
	{
		for (int i = 0; i < array.Length; i++) if (array[i] < 0) array[i] = 0;
	}

	public static double SecondsPassed(this DateTime dateTime)
		=> (DateTime.Now - dateTime).TotalSeconds;
}