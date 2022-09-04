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

	public static void Clear(this short[] array)
	{
		for (int i = 0; i < array.Length; i++) array[i] = 0;
	}
}