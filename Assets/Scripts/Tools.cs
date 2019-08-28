using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
	public static void SetLayer(this GameObject gameObject, int layer)
	{
		gameObject.layer = layer;
		
		foreach(Transform child in gameObject.transform)
		{
			child.gameObject.SetLayer(layer);
		}
	}
}
