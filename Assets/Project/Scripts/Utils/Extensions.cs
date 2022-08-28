using UnityEngine;

public static class Extensions
{
	public static class InputExtensions
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
	}
}